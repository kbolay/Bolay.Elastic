using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Sum
{
    public class SumSerializer : JsonConverter
    {
        private const string _FIELD = "field";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Sum.ToString()));

            return MetricAggregationBase.Deserialize<SumAggregate>(wholeDict.First().Key, fieldDict);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SumAggregate))
                throw new SerializeTypeException<SumAggregate>();

            SumAggregate agg = value as SumAggregate;

            Dictionary<string, object> fieldDict = agg.Serialize();

            Dictionary<string, object> maxDict = new Dictionary<string, object>();
            maxDict.Add(AggregationTypeEnum.Sum.ToString(), fieldDict);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(agg.Name, maxDict);

            serializer.Serialize(writer, aggDict);
        }
    }
}
