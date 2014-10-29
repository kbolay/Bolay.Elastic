using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoDistance
{
    public class GeoDistanceSerializer : JsonConverter
    {
        private const string _DISTANCE = "distance";
        private const string _DISTANCE_TYPE = "distance_type";
        private const string _OPTIMIZE_BBOX = "optimize_bbox";

        private static List<string> _KnownFields = new List<string>() { _DISTANCE, _DISTANCE_TYPE, _OPTIMIZE_BBOX, FilterSerializer._CACHE, FilterSerializer._CACHE_KEY };

        internal const bool _CACHE_DEFAULT = false;
        internal static DistanceComputeTypeEnum _DISTANCE_TYPE_DEFAULT = DistanceComputeTypeEnum.SloppyArc;
        internal static BoundingBoxOptimizeEnum _OPTIMIZE_BBOX_DEFAULT = BoundingBoxOptimizeEnum.Memory;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.GeoDistance.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            DistanceValue distance = new DistanceValue(fieldDict.GetString(_DISTANCE));
            KeyValuePair<string, object> fieldKvp = fieldDict.First(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("GeoPointProperty");

            CoordinatePoint point = CoordinatePointSerializer.DeserializeCoordinatePoint(fieldKvp.Value.ToString());

            GeoDistanceFilter filter = new GeoDistanceFilter(fieldKvp.Key, distance, point);

            filter.DistanceComputeMethod = DistanceComputeTypeEnum.Find(fieldDict.GetString(_DISTANCE_TYPE, _DISTANCE_TYPE_DEFAULT.ToString()));
            filter.OptimizeBoundingBox = BoundingBoxOptimizeEnum.Find(fieldDict.GetString(_OPTIMIZE_BBOX, _OPTIMIZE_BBOX_DEFAULT.ToString()));

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoDistanceFilter))
                throw new SerializeTypeException<GeoDistanceFilter>();

            GeoDistanceFilter filter = value as GeoDistanceFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_DISTANCE, filter.Distance);
            fieldDict.Add(filter.Field, filter.CenterPoint);

            fieldDict.AddObject(_DISTANCE_TYPE, filter.DistanceComputeMethod.ToString(), _DISTANCE_TYPE_DEFAULT.ToString());
            fieldDict.AddObject(_OPTIMIZE_BBOX, filter.OptimizeBoundingBox.ToString(), _OPTIMIZE_BBOX_DEFAULT.ToString());

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.GeoDistance.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
