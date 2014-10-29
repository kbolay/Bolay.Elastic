using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public class ScoreFunctionSerializer : JsonConverter
    {
        private const string _FILTER = "filter";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> functionDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            ScoreFunctionEnum functionType = ScoreFunctionEnum.BoostFactor;
            functionType = ScoreFunctionEnum.Find(functionDict.First().Key);

            if (functionType == null)
                throw new Exception("Unable to determine score function type.");

            ScoreFunctionBase scoreFunction = JsonConvert.DeserializeObject(functionDict.First().Value.ToString(), functionType.ImplementationType) as ScoreFunctionBase;

            return scoreFunction;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if (!(value is ScoreFunctionBase))
            //    throw new Exception("This serializer requiers a ScoreFunctionBase.");

            //Dictionary<string, object> functionDict = new Dictionary<string, object>();
            //ScoreFunctionBase functionBase = value as ScoreFunctionBase;
            //if (functionBase.Filter != null)
            //{
            //    Dictionary<string, object> filterDict = new Dictionary<string, object>();
            //    filterDict.Add(_FILTER, functionBase.Filter);
            //    functionDict.Add(functionBase.)
            //}

            throw new NotImplementedException();
        }
    }
}
