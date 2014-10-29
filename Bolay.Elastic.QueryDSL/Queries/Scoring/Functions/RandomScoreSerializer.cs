using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public class RandomScoreSerializer : JsonConverter
    {
        private const string _SEED = "seed";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> randomDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (randomDict.First().Key == ScoreFunctionEnum.Random.ToString())
            {
                randomDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(randomDict.First().Value.ToString());
            }

            Int64 seed = randomDict.GetInt64(_SEED);

            return new RandomScoreFunction(seed);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RandomScoreFunction))
                throw new SerializeTypeException<RandomScoreFunction>();

            RandomScoreFunction randomFunction = value as RandomScoreFunction;

            Dictionary<string, object> seedDict = new Dictionary<string, object>();
            seedDict.Add(_SEED, randomFunction.Seed);

            serializer.Serialize(writer, seedDict);
        }
    }
}
