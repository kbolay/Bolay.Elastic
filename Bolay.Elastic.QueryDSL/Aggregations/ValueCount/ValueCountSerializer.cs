using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.ValueCount
{
    public class ValueCountSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.ValueCount.ToString()));

            return MetricAggregationBase.Deserialize<ValueCountAggregate>(wholeDict.First().Key, fieldDict);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ValueCountAggregate))
                throw new SerializeTypeException<ValueCountAggregate>();

            ValueCountAggregate agg = value as ValueCountAggregate;

            Dictionary<string, object> fieldDict = agg.Serialize();

            Dictionary<string, object> maxDict = new Dictionary<string, object>();
            maxDict.Add(AggregationTypeEnum.ValueCount.ToString(), fieldDict);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(agg.Name, maxDict);

            serializer.Serialize(writer, aggDict);
        }
    }
}
