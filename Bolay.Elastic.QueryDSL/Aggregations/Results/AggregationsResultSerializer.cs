using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class AggregationsResultSerializer : JsonConverter
    {
        private const string _UNIT = "unit";
        private const string _TO = "to";
        private const string _FROM = "from";
        private const string _TO_AS_STRING = "to_as_string";
        private const string _FROM_AS_STRING = "from_as_string";
        private const string _KEY = "key";
        private const string _KEY_AS_STRING = "key_as_string";
        private const string _DOCUMENT_COUNT = "doc_count";
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";
        private const string _AVERAGE = "avg";
        private const string _COUNT = "count";
        private const string _SUM = "sum";
        private const string _SUM_OF_SQUARES = "sum_of_squares";
        private const string _STANDARD_DEVIATION = "std_deviation";
        private const string _VARIANCE = "variance";
        private const string _VALUE = "value";
        private const string _BUCKETS = "buckets";

        private static List<string> _KnownFields = new List<string>()
        {
            _UNIT,
            _TO,
            _FROM,
            _TO_AS_STRING,
            _FROM_AS_STRING,
            _KEY,
            _KEY_AS_STRING,
            _DOCUMENT_COUNT,
            _MINIMUM,
            _MAXIMUM,
            _AVERAGE,
            _COUNT,
            _SUM,
            _SUM_OF_SQUARES,
            _STANDARD_DEVIATION,
            _VARIANCE,
            _VALUE,
            _BUCKETS
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> aggsDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            IEnumerable<IAggregationResult> aggregationResults = DeserializeSubAggregations(aggsDict);
            AggregationsResult result = new AggregationsResult()
            {
                Aggregations = aggregationResults
            };

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public MultiBucketAggregation DeserializeBuckets(string bucketsJson)
        {
            MultiBucketAggregation agg = null;
            List<Dictionary<string, object>> bucketDictList = null;
            try
            {
                bucketDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(bucketsJson);
            }
            catch { }

            Dictionary<string, object> keyedBucketsDict = null;
            try
            {
                keyedBucketsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(bucketsJson);
            }
            catch { }

            if (bucketDictList != null)
            {
                agg = DeserializeBuckets(bucketDictList);
            }
            else if (keyedBucketsDict != null)
            {
                agg = DeserializeKeyedBuckets(keyedBucketsDict);
            }

            return agg;
        }

        public MultiBucketAggregation DeserializeBuckets(List<Dictionary<string, object>> bucketDictList)
        {
            MultiBucketAggregation agg = new MultiBucketAggregation();
            List<BucketAggregation> buckets = new List<BucketAggregation>();
            foreach (Dictionary<string, object> bucketDict in bucketDictList)
            {
                buckets.Add(DeserializeBucket(bucketDict));
            }

            agg.Buckets = buckets;
            return agg;
        }
        public MultiBucketAggregation DeserializeKeyedBuckets(Dictionary<string, object> bucketsDict)
        {
            List<BucketAggregation> buckets = new List<BucketAggregation>();
            foreach (KeyValuePair<string, object> bucketKvp in bucketsDict)
            {
                BucketAggregation bucket = DeserializeBucket(JsonConvert.DeserializeObject<Dictionary<string, object>>(bucketKvp.Value.ToString()));

                bucket.Key = bucketKvp.Key;

                buckets.Add(bucket);
            }

            MultiBucketAggregation agg = new MultiBucketAggregation();
            agg.Buckets = buckets;

            return agg;
        }

        public BucketAggregation DeserializeBucket(Dictionary<string, object> bucketDict)
        {
            BucketAggregation bucket = null;
            if (bucketDict.ContainsKey(_UNIT))
            {
                DistanceUnitEnum unitType = DistanceUnitEnum.Centimeter;
                bucket = new DistanceAggregation() 
                { 
                    Unit = DistanceUnitEnum.Find(bucketDict.GetString(_UNIT)),
                    To = bucketDict.GetDoubleOrNull(_TO),
                    From = bucketDict.GetDoubleOrNull(_FROM),
                    DocumentCount = bucketDict.GetInt32(_DOCUMENT_COUNT)
                };
            }
            else if (bucketDict.ContainsKey(_TO) || bucketDict.ContainsKey(_FROM))
            {
                bucket = new RangeAggregation()
                {
                    To = bucketDict.GetDoubleOrNull(_TO),
                    From = bucketDict.GetDoubleOrNull(_FROM),
                    ToAsString = bucketDict.GetStringOrDefault(_TO_AS_STRING),
                    FromAsString = bucketDict.GetStringOrDefault(_FROM_AS_STRING),
                    DocumentCount = bucketDict.GetInt32(_DOCUMENT_COUNT)
                };
            }
            else
            {
                bucket = new BucketAggregation()
                {
                    KeyAsString = bucketDict.GetStringOrDefault(_KEY_AS_STRING),
                    DocumentCount = bucketDict.GetInt32(_DOCUMENT_COUNT)
                };
            }

            if (bucketDict.ContainsKey(_KEY))
                bucket.Key = bucketDict[_KEY];

            bucket.AggregationResults = DeserializeSubAggregations(bucketDict);

            return bucket;
        }

        public IEnumerable<IAggregationResult> DeserializeSubAggregations(Dictionary<string, object> aggsDict)
        {
            List<IAggregationResult> aggregationResults = new List<IAggregationResult>();
            foreach (KeyValuePair<string, object> aggKvp in aggsDict.Where(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase)))
            {
                Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggKvp.Value.ToString());

                if (aggDict.ContainsKey(_BUCKETS))
                {
                    MultiBucketAggregation agg = DeserializeBuckets(aggDict.GetString(_BUCKETS));
                    agg.Name = aggKvp.Key;
                    aggregationResults.Add(agg);
                    continue;
                }
                else if (aggDict.ContainsKey(_DOCUMENT_COUNT))
                {
                    BucketAggregation agg = new BucketAggregation()
                    {
                        Name = aggKvp.Key,
                        DocumentCount = aggDict.GetInt32(_DOCUMENT_COUNT),
                        AggregationResults = DeserializeSubAggregations(aggDict)
                    };
                    aggregationResults.Add(agg);
                    continue;
                }
                else if (aggDict.ContainsKey(_STANDARD_DEVIATION))
                {
                    ExtendedStatisticsAggregation agg = new ExtendedStatisticsAggregation()
                    {
                        Name = aggKvp.Key,
                        Average = aggDict.GetDouble(_AVERAGE),
                        Count = aggDict.GetInt32(_COUNT),
                        Maximum = aggDict.GetDouble(_MAXIMUM),
                        Minimum = aggDict.GetDouble(_MINIMUM),
                        StandardDeviation = aggDict.GetDouble(_STANDARD_DEVIATION),
                        Sum = aggDict.GetDouble(_SUM),
                        SumOfSquares = aggDict.GetDouble(_SUM_OF_SQUARES),
                        Variance = aggDict.GetDouble(_VARIANCE)
                    };

                    aggregationResults.Add(agg);
                    continue;
                }
                else if (aggDict.ContainsKey(_SUM))
                {
                    StatisticsAggregation agg = new StatisticsAggregation()
                    {
                        Name = aggKvp.Key,
                        Average = aggDict.GetDouble(_AVERAGE),
                        Count = aggDict.GetInt32(_COUNT),
                        Maximum = aggDict.GetDouble(_MAXIMUM),
                        Minimum = aggDict.GetDouble(_MINIMUM),
                        Sum = aggDict.GetDouble(_SUM)
                    };

                    aggregationResults.Add(agg);
                    continue;
                }
                else if (aggDict.ContainsKey(_VALUE))
                {
                    SingleValueAggregation agg = new SingleValueAggregation()
                    {
                        Name = aggKvp.Key,
                        Value = aggDict[_VALUE]
                    };

                    aggregationResults.Add(agg);
                    continue;
                }
                else // PERCENTILES
                {
                    PercentilesAggregation agg = new PercentilesAggregation(aggKvp.Key, aggDict);
                }
            }

            return aggregationResults;
        }
    }
}
