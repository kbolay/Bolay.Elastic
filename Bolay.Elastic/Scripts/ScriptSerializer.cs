using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Scripts
{
    public class ScriptSerializer : JsonConverter
    {       
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            Script script = null;
            if (fieldDict.ContainsKey(Script.SCRIPT))
            {
                script = new Script(fieldDict.GetString(Script.SCRIPT));
                script.Language = fieldDict.GetStringOrDefault(Script.LANGUAGE);

                if (fieldDict.ContainsKey(Script.PARAMETERS))
                    script.Parameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(fieldDict.GetString(Script.PARAMETERS));
            }

            return script;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Script))
                throw new SerializeTypeException<Script>();

            Script script = value as Script;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(Script.LANGUAGE, script.Language);
            fieldDict.AddObject(Script.SCRIPT, script.ScriptText);
            if (script.Parameters != null && script.Parameters.Any())
            {
                fieldDict.AddObject(Script.PARAMETERS, new ScriptParameterCollection(script.Parameters));
            }            

            serializer.Serialize(writer, fieldDict);
        }
    }
}
