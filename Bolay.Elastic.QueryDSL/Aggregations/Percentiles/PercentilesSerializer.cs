using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Percentiles
{
    internal class PercentilesSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _PERCENTS = "percents";
        private const string _COMPRESSION = "compression";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> percentDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Percentiles.ToString()));

            PercentilesAggregate agg = null;
            if (percentDict.ContainsKey(_FIELD))
            {
                agg = new PercentilesAggregate(wholeDict.First().Key, percentDict.GetString(_FIELD));
            }
            else if (percentDict.ContainsKey(ScriptSerializer._SCRIPT))
            {
                Script script = ScriptSerializer.Deserialize(percentDict);
                agg = new PercentilesAggregate(wholeDict.First().Key, script);
            }
            else
            {
                throw new RequiredPropertyMissingException(_FIELD + "/" + ScriptSerializer._SCRIPT);
            }

            if (percentDict.ContainsKey(_PERCENTS))
            {
                agg.PercentBuckets = JsonConvert.DeserializeObject<IEnumerable<Double>>(percentDict.GetString(_PERCENTS));
            }

            agg.Compression = percentDict.GetInt32(_COMPRESSION, PercentilesAggregate._COMPRESSION_DEFAULT); 
            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PercentilesAggregate))
                throw new SerializeTypeException<PercentilesAggregate>();

            PercentilesAggregate agg = value as PercentilesAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            ScriptSerializer.Serialize(agg.Script, fieldDict);

            string defaultPercents = JsonConvert.SerializeObject(PercentilesAggregate._PERCENT_BUCKETS_DEFAULT);
            string actualPercents = JsonConvert.SerializeObject(agg.PercentBuckets);

            if (actualPercents != defaultPercents)
                fieldDict.AddObject(_PERCENTS, agg.PercentBuckets);

            fieldDict.AddObject(_COMPRESSION, agg.Compression, PercentilesAggregate._COMPRESSION_DEFAULT);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Percentiles.ToString(), fieldDict);

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
