using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public class ScriptScoreSerializer : JsonConverter
    {
        private const string _SCRIPT = "script";
        private const string _PARAMETERS = "params";
        private const string _LANGUAGE = "lang";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> scriptDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (scriptDict.ContainsKey(ScoreFunctionEnum.Script.ToString()))
                scriptDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(scriptDict.First().Value.ToString());

            ScriptScoreFunction scriptFunction = new ScriptScoreFunction(ScriptSerializer.Deserialize(scriptDict));

            return scriptFunction;                
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ScriptScoreFunction))
                throw new SerializeTypeException<ScriptScoreFunction>();

            ScriptScoreFunction scriptFunction = value as ScriptScoreFunction;
            Dictionary<string, object> scriptDict = new Dictionary<string, object>();
            ScriptSerializer.Serialize(scriptFunction.Script, scriptDict);
            
            serializer.Serialize(writer, scriptDict);
        }
    }
}
