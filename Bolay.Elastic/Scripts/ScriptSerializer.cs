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
        public const string _SCRIPT = "script";
        internal const string _SCRIPT_LANGUAGE = "lang";
        internal const string _PARAMETERS = "params";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public static void Serialize(Script script, Dictionary<string, object> fieldDict, string scriptKey = _SCRIPT)
        {
            if (script == null || string.IsNullOrWhiteSpace(script.ScriptText))
                return;

            fieldDict.AddObject(_SCRIPT_LANGUAGE, script.Language);
            fieldDict.AddObject(scriptKey, script.ScriptText);
            if(script.Parameters != null && script.Parameters.Any(x => x != null))
                fieldDict.AddObject(_PARAMETERS, new ScriptParameterCollection(script.Parameters));
        }

        public static Script Deserialize(Dictionary<string, object> fieldDict, string scriptKey = _SCRIPT)
        {
            if (!fieldDict.ContainsKey(scriptKey))
                return null;

            Script script = new Script(fieldDict.GetString(scriptKey));
            script.Language = fieldDict.GetStringOrDefault(_SCRIPT_LANGUAGE);

            if (fieldDict.ContainsKey(_PARAMETERS))
                script.Parameters = JsonConvert.DeserializeObject<ScriptParameterCollection>(fieldDict.GetString(_PARAMETERS));

            return script;
        }
    }
}
