using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations
{
    public class AggregationsSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> aggsDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            List<IAggregation> aggGenerators = new List<IAggregation>();
            AggregationTypeEnum aggType = AggregationTypeEnum.DateHistogram;
            foreach (KeyValuePair<string, object> aggKvp in aggsDict)
            {
                Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggKvp.Value.ToString());
                KeyValuePair<string, object> aggTypeKvp = aggDict.First();

                // create a dictionary just for this one
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(aggKvp.Key, aggKvp.Value);
                Dictionary<string, object> aggTypeDict = new Dictionary<string, object>();
                aggTypeDict.Add("junk", internalDict);

                aggType = AggregationTypeEnum.Find(aggTypeKvp.Key);
                if (aggType == null)
                    throw new Exception(aggTypeKvp.Key + " is not a valid type of aggregation.");

                string facetTypeJsonStr = JsonConvert.SerializeObject(aggTypeDict.First().Value);
                aggGenerators.Add(JsonConvert.DeserializeObject(facetTypeJsonStr, aggType.ImplementationType) as IAggregation);
            }

            Aggregations aggs = new Aggregations(aggGenerators);

            return aggs;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Aggregations))
                throw new SerializeTypeException<Aggregations>();

            Aggregations aggs = value as Aggregations;

            Dictionary<string, object> aggsDict = new Dictionary<string, object>();

            foreach (IAggregation agg in aggs.Aggregators)
            {
                string aggJson = JsonConvert.SerializeObject(agg);
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggJson);
                aggsDict.Add(fieldDict.First().Key, fieldDict.First().Value);
            }

            serializer.Serialize(writer, aggsDict);
        }
    }
}
