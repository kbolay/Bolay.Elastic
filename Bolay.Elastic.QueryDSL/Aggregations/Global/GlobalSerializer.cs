using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Global
{
    public class GlobalSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> globalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Global.ToString()));

            IEnumerable<IAggregation> subAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            if (subAggregations == null || !subAggregations.Any())
                throw new RequiredPropertyMissingException(BucketAggregationBase._SUB_AGGREGATIONS + "/" + BucketAggregationBase._SUB_AGGREGATIONS_ABBR);

            return new GlobalAggregate(wholeDict.First().Key, subAggregations);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GlobalAggregate))
                throw new SerializeTypeException<GlobalAggregate>();

            GlobalAggregate agg = value as GlobalAggregate;

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Global.ToString(), new Dictionary<string, object>());
            
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
