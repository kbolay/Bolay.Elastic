using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.IPv4
{   
    public class IPv4RangeSerializer : JsonConverter
    {
        private const string _TO = "to";
        private const string _FROM = "from";
        private const string _RANGES = "ranges";
        private const string _FIELD = "field";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.IpRange.ToString()));

            string aggName = wholeDict.First().Key;
            string field = fieldDict.GetString(_FIELD);

            List<IpRangeBucket> buckets = new List<IpRangeBucket>();
            List<Dictionary<string, object>> bucketDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(fieldDict.GetString(_RANGES));
            foreach (Dictionary<string, object> bucketDict in bucketDictList)
            {
                IpRangeBucket bucket = DeserializeRangeBucket(bucketDict);
                buckets.Add(bucket);
            }

            IPv4RangeAggregate agg = new IPv4RangeAggregate(aggName, field, buckets);
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IPv4RangeAggregate))
                throw new SerializeTypeException<IPv4RangeAggregate>();

            IPv4RangeAggregate agg = value as IPv4RangeAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            fieldDict.AddObject(_RANGES, agg.Ranges.Select(x => SerializeRangeBucket(x)));
            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.IpRange.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }

        private Dictionary<string, object> SerializeRangeBucket(IpRangeBucket bucket)
        {
            Dictionary<string, object> bucketDict = new Dictionary<string, object>();
            bucketDict.AddObject(_TO, bucket.To);
            bucketDict.AddObject(_FROM, bucket.From);

            return bucketDict;
        }

        private IpRangeBucket DeserializeRangeBucket(Dictionary<string, object> bucketDict)
        {
            IpRangeBucket bucket = new IpRangeBucket();
            if (bucketDict.ContainsKey(_TO))
                bucket.To = bucketDict.GetString(_TO);
            if (bucketDict.ContainsKey(_FROM))
                bucket.From = bucketDict.GetString(_FROM);

            return bucket;
        }
    }
}
