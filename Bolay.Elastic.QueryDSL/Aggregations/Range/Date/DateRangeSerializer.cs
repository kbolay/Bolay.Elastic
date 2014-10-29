using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.Date
{
    public class DateRangeSerializer : JsonConverter
    {
        private const string _TO = "to";
        private const string _FROM = "from";
        private const string _RANGES = "ranges";
        private const string _FIELD = "field";
        private const string _FORMAT = "format";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.DateRange.ToString()));

            string aggName = wholeDict.First().Key;
            string field = fieldDict.GetString(_FIELD);

            List<DateRangeBucket> buckets = new List<DateRangeBucket>();
            List<Dictionary<string, object>> bucketDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(fieldDict.GetString(_RANGES));
            foreach (Dictionary<string, object> bucketDict in bucketDictList)
            {
                DateRangeBucket bucket = DeserializeRangeBucket(bucketDict);
                buckets.Add(bucket);
            }

            DateRangeAggregate agg = new DateRangeAggregate(aggName, field, buckets);
            agg.Format = fieldDict.GetStringOrDefault(_FORMAT);
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateRangeAggregate))
                throw new SerializeTypeException<DateRangeAggregate>();

            DateRangeAggregate agg = value as DateRangeAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            fieldDict.AddObject(_FORMAT, agg.Format);
            fieldDict.AddObject(_RANGES, agg.Ranges.Select(x => SerializeRangeBucket(x)));
            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.DateRange.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }

        private Dictionary<string, object> SerializeRangeBucket(DateRangeBucket bucket)
        {
            Dictionary<string, object> bucketDict = new Dictionary<string, object>();
            bucketDict.AddObject(_TO, bucket.To);
            bucketDict.AddObject(_FROM, bucket.From);

            return bucketDict;
        }

        private DateRangeBucket DeserializeRangeBucket(Dictionary<string, object> bucketDict)
        {
            DateRangeBucket bucket = new DateRangeBucket();
            if (bucketDict.ContainsKey(_TO))
                bucket.To = bucketDict[_TO];
            if (bucketDict.ContainsKey(_FROM))
                bucket.From = bucketDict[_FROM];

            return bucket;
        }
    }
}
