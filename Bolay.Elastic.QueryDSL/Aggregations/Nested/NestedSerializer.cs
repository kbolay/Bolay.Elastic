using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Nested
{
    public class NestedSerializer : JsonConverter
    {
        private const string _PATH = "path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> nestedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Nested.ToString()));

            NestedAggregate agg = new NestedAggregate(wholeDict.First().Key, nestedDict.GetString(_PATH));
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NestedAggregate))
                throw new SerializeTypeException<NestedAggregate>();

            NestedAggregate agg = value as NestedAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_PATH, agg.Path);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Nested.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }
    }
}
