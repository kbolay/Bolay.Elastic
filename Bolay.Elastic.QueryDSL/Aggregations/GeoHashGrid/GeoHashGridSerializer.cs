using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.GeoHashGrid
{
    public class GeoHashGridSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _PRECISION = "precision";
        private const string _SIZE = "size";
        private const string _SHARD_SIZE = "shard_size";

        internal const int _SIZE_DEFAULT = 10000;
        internal const int _PRECISION_DEFAULT = 5;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.GeoHashGrid.ToString()));

            GeoHashGridAggregate agg = new GeoHashGridAggregate(
                wholeDict.First().Key,
                fieldDict.GetString(_FIELD),
                fieldDict.GetInt32(_PRECISION, _PRECISION_DEFAULT));

            agg.Size = fieldDict.GetInt32(_SIZE, _SIZE_DEFAULT);
            agg.ShardSize = fieldDict.GetInt32(_SHARD_SIZE, agg.Size);

            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoHashGridAggregate))
                throw new SerializeTypeException<GeoHashGridAggregate>();

            GeoHashGridAggregate agg = value as GeoHashGridAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            fieldDict.AddObject(_PRECISION, agg.Precision, _PRECISION_DEFAULT);
            fieldDict.AddObject(_SIZE, agg.Size, _SIZE_DEFAULT);
            fieldDict.AddObject(_SHARD_SIZE, agg.ShardSize, agg.Size);

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.GeoHashGrid.ToString(), fieldDict);

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
