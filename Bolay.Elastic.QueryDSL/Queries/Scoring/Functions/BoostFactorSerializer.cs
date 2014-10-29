using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public class BoostFactorSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> boostDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Double boostValue = boostDict.GetDouble(ScoreFunctionEnum.BoostFactor.ToString());

            return new BoostFactorFunction(boostValue);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is BoostFactorFunction))
                throw new SerializeTypeException<BoostFactorFunction>();

            BoostFactorFunction boostFactor = value as BoostFactorFunction;

            serializer.Serialize(writer, boostFactor.Boost);
        }
    }
}
