using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Missing
{
    public class MissingSerializer : JsonConverter
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
            Dictionary<string, object> missingDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Missing.ToString()));

            MissingAggregate agg = new MissingAggregate(wholeDict.First().Key, missingDict.GetString(_FIELD));
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MissingAggregate))
                throw new SerializeTypeException<MissingAggregate>();

            MissingAggregate agg = value as MissingAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_FIELD, agg.Field);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Missing.ToString(), fieldDict);

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
