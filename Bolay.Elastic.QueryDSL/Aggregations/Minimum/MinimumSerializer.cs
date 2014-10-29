using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Minimum
{
    public class MinimumSerializer : JsonConverter
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
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Minimum.ToString()));

            return MetricAggregationBase.Deserialize<MinimumAggregate>(wholeDict.First().Key, fieldDict);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MinimumAggregate))
                throw new SerializeTypeException<MinimumAggregate>();

            MinimumAggregate agg = value as MinimumAggregate;

            Dictionary<string, object> fieldDict = agg.Serialize();

            Dictionary<string, object> minDict = new Dictionary<string, object>();
            minDict.Add(AggregationTypeEnum.Minimum.ToString(), fieldDict);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(agg.Name, minDict);

            serializer.Serialize(writer, aggDict);
        }
    }
}
