using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range
{
    public class RangeSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _KEYED = "keyed";
        private const string _RANGES = "ranges";
        private const string _GREATER_THAN = "gt";
        private const string _LESS_THAN = "lt";
        private const string _GREATER_THAN_OR_EQUAL_TO = "gte";
        private const string _LESS_THAN_OR_EQUAL_TO = "lte";
        private const string _RANGE_KEY = "key";

        internal const bool _KEYED_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Range.ToString()));

            string aggName = wholeDict.First().Key;
            string field = fieldDict.GetStringOrDefault(_FIELD);
            Script script = fieldDict.DeserializeObject<Script>();

            List<RangeBucket> buckets = new List<RangeBucket>();
            List<Dictionary<string, object>> bucketDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(fieldDict.GetString(_RANGES));
            foreach (Dictionary<string, object> bucketDict in bucketDictList)
            {
                RangeBucket bucket = DeserializeRangeBucket(bucketDict);
                buckets.Add(bucket);
            }

            RangeAggregate agg = null;
            if (!string.IsNullOrWhiteSpace(field) && script != null)
                agg = new RangeAggregate(aggName, field, script, buckets);
            else if (!string.IsNullOrWhiteSpace(field))
                agg = new RangeAggregate(aggName, field, buckets);
            else if (script != null)
                agg = new RangeAggregate(aggName, script, buckets);
            else
                throw new RequiredPropertyMissingException(_FIELD + "/" + Script.SCRIPT);

            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RangeAggregate))
                throw new SerializeTypeException<RangeAggregate>();

            RangeAggregate agg = value as RangeAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            agg.Script.Serialize(fieldDict);
            fieldDict.AddObject(_KEYED, agg.AreRangesKeyed, _KEYED_DEFAULT);
            fieldDict.AddObject(_RANGES, agg.Ranges.Select(x => SerializeRangeBucket(x)));
            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Range.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }

        private Dictionary<string, object> SerializeRangeBucket(RangeBucket bucket)
        {
            Dictionary<string, object> bucketDict = new Dictionary<string, object>();
            bucketDict.AddObject(_RANGE_KEY, bucket.Key);
            bucketDict.AddObject(_GREATER_THAN, bucket.GreaterThan);
            bucketDict.AddObject(_LESS_THAN, bucket.LessThan);
            bucketDict.AddObject(_GREATER_THAN_OR_EQUAL_TO, bucket.GreaterThanOrEqualTo);
            bucketDict.AddObject(_LESS_THAN_OR_EQUAL_TO, bucket.LessThanOrEqualTo);

            return bucketDict;
        }

        private RangeBucket DeserializeRangeBucket(Dictionary<string, object> bucketDict)
        {
            RangeBucket bucket = new RangeBucket();
            if (bucketDict.ContainsKey(_GREATER_THAN))
                bucket.GreaterThan = bucketDict[_GREATER_THAN];
            if (bucketDict.ContainsKey(_LESS_THAN))
                bucket.LessThan = bucketDict[_LESS_THAN];
            if (bucketDict.ContainsKey(_GREATER_THAN_OR_EQUAL_TO))
                bucket.GreaterThanOrEqualTo = bucketDict[_GREATER_THAN_OR_EQUAL_TO];
            if (bucketDict.ContainsKey(_LESS_THAN_OR_EQUAL_TO))
                bucket.LessThanOrEqualTo = bucketDict[_LESS_THAN_OR_EQUAL_TO];

            bucket.Key = bucketDict.GetStringOrDefault(_RANGE_KEY);

            return bucket;
        }
    }
}
