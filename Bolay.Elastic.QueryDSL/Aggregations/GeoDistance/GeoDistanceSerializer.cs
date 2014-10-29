using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.GeoDistance
{
    public class GeoDistanceSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _ORIGIN = "origin";
        private const string _UNIT = "unit";
        private const string _DISTANCE_TYPE = "distance_type";
        private const string _RANGES = "ranges";
        private const string _TO = "to";
        private const string _FROM = "from";

        internal static DistanceUnitEnum _UNIT_DEFAULT = DistanceUnitEnum.Kilometer;
        internal static DistanceComputeTypeEnum _DISTANCE_TYPE_DEFAULT = DistanceComputeTypeEnum.SloppyArc;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.GeoDistance.ToString()));

            List<DistanceRangeBucket> ranges = new List<DistanceRangeBucket>();
            List<Dictionary<string, object>> bucketDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(fieldDict.GetString(_RANGES));
            foreach (Dictionary<string, object> bucketDict in bucketDictList)
            {
                ranges.Add(DeserializeRangeBucket(bucketDict));
            }

            GeoDistanceAggregate agg = new GeoDistanceAggregate(
                wholeDict.First().Key,
                fieldDict.GetString(_FIELD),
                CoordinatePointSerializer.DeserializeCoordinatePoint(fieldDict.GetString(_ORIGIN)),
                ranges);

            agg.Unit = DistanceUnitEnum.Find(fieldDict.GetString(_UNIT, _UNIT_DEFAULT.ToString()));
            agg.DistanceComputeType = DistanceComputeTypeEnum.Find(fieldDict.GetString(_DISTANCE_TYPE, _DISTANCE_TYPE_DEFAULT.ToString()));

            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoDistanceAggregate))
                throw new SerializeTypeException<GeoDistanceAggregate>();

            GeoDistanceAggregate agg = value as GeoDistanceAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            fieldDict.AddObject(_ORIGIN, agg.OriginPoint);
            fieldDict.AddObject(_UNIT, agg.Unit.ToString(), _UNIT_DEFAULT.ToString());
            fieldDict.AddObject(_DISTANCE_TYPE, agg.DistanceComputeType.ToString(), _DISTANCE_TYPE_DEFAULT.ToString());
            fieldDict.AddObject(_RANGES, agg.Ranges.Select(x => SerializeRangeBucket(x)));
            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.GeoDistance.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }

        private Dictionary<string, object> SerializeRangeBucket(DistanceRangeBucket bucket)
        {
            Dictionary<string, object> bucketDict = new Dictionary<string, object>();
            bucketDict.AddObject(_TO, bucket.To);
            bucketDict.AddObject(_FROM, bucket.From);

            return bucketDict;
        }

        private DistanceRangeBucket DeserializeRangeBucket(Dictionary<string, object> bucketDict)
        {
            DistanceRangeBucket bucket = new DistanceRangeBucket();
            if (bucketDict.ContainsKey(_TO))
                bucket.To = bucketDict.GetDouble(_TO);
            if (bucketDict.ContainsKey(_FROM))
                bucket.From = bucketDict.GetDouble(_FROM);

            return bucket;
        }
    }
}
