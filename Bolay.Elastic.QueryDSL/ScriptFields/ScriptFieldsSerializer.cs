using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.ScriptFields
{
    public class ScriptFieldsSerializer : JsonConverter
    {
        private const string _PARAMETERS = "params";
        private const string _SCRIPT = "script";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SearchPieceTypeEnum.ScriptField.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<ScriptField> scriptFields = new List<ScriptField>();
            foreach (KeyValuePair<string, object> scriptKvp in fieldDict)
            {
                Dictionary<string, object> scriptFieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(scriptKvp.Value.ToString());
                
                scriptFields.Add(new ScriptField(scriptKvp.Key, ScriptSerializer.Deserialize(scriptFieldDict)));                
            }

            return new ScriptFieldRequest(scriptFields);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ScriptFieldRequest))
                throw new SerializeTypeException<ScriptFieldRequest>();

            ScriptFieldRequest scriptFields = value as ScriptFieldRequest;

            Dictionary<string, object> scriptFieldDict = new Dictionary<string, object>();
            foreach (ScriptField scriptField in scriptFields.Fields)
            {
                Dictionary<string, object> scriptDict = new Dictionary<string, object>();
                ScriptSerializer.Serialize(scriptField.Script, scriptDict);
                scriptFieldDict.Add(scriptField.Field, scriptDict);
            }

            Dictionary<string, object> scriptsDict = new Dictionary<string, object>();
            scriptsDict.Add(SearchPieceTypeEnum.ScriptField.ToString(), scriptFieldDict);

            serializer.Serialize(writer, scriptsDict);
        }
    }
}
