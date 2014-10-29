using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoDistanceRange
{
    public class GeoDistanceRangeSerializer : JsonConverter
    {
        private const string _GREATER_THAN = "gt";
        private const string _GREATER_THAN_OR_EQUAL_TO = "gte";
        private const string _LESS_THAN = "lt";
        private const string _LESS_THAN_OR_EQUAL_TO = "lte";
        private const string _TO = "to";
        private const string _FROM = "from";
        private const string _INCLUDE_UPPER = "include_upper";
        private const string _INCLUDE_LOWER = "include_lower";

        internal const bool _CACHE_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            _GREATER_THAN,
            _LESS_THAN,
            _GREATER_THAN_OR_EQUAL_TO,
            _LESS_THAN,
            _TO,
            _FROM,
            _INCLUDE_LOWER,
            _INCLUDE_UPPER,
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY
        };

        private static List<string> _GreaterFields = new List<string>()
        {
            _GREATER_THAN,
            _GREATER_THAN_OR_EQUAL_TO,
            _FROM
        };

        private static List<string> _LesserFields = new List<string>()
        {
            _LESS_THAN,
            _LESS_THAN_OR_EQUAL_TO,
            _TO
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.GeoDistanceRange.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            if(fieldDict.Count(x => _GreaterFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase)) > 1)
                throw new ConflictingPropertiesException(_GreaterFields);
            if(fieldDict.Count(x => _LesserFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase)) > 1)
                throw new ConflictingPropertiesException(_LesserFields);

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("GeoPointProperty");

            CoordinatePoint point = CoordinatePointSerializer.DeserializeCoordinatePoint(fieldKvp.Value.ToString());

            GeoDistanceRangeFilter filter = new GeoDistanceRangeFilter(fieldKvp.Key, point);
            if (fieldDict.ContainsKey(_GREATER_THAN))
            {
                filter.GreaterThan = new DistanceValue(fieldDict.GetString(_GREATER_THAN));
            }
            
            if (fieldDict.ContainsKey(_GREATER_THAN_OR_EQUAL_TO))
            {
                filter.GreaterThanOrEqualTo = new DistanceValue(fieldDict.GetString(_GREATER_THAN_OR_EQUAL_TO));
            }

            if (fieldDict.ContainsKey(_FROM))
            {
                if (fieldDict.ContainsKey(_INCLUDE_LOWER) && fieldDict.GetBool(_INCLUDE_LOWER))
                    filter.GreaterThanOrEqualTo = new DistanceValue(fieldDict.GetString(_FROM));
                else
                    filter.GreaterThan = new DistanceValue(fieldDict.GetString(_FROM));
            }

            if (fieldDict.ContainsKey(_LESS_THAN))
            {
                filter.LessThan = new DistanceValue(fieldDict.GetString(_LESS_THAN));
            }

            if (fieldDict.ContainsKey(_LESS_THAN_OR_EQUAL_TO))
            {
                filter.LessThanOrEqualTo = new DistanceValue(fieldDict.GetString(_LESS_THAN_OR_EQUAL_TO));
            }

            if (fieldDict.ContainsKey(_TO))
            {
                if (fieldDict.ContainsKey(_INCLUDE_UPPER) && fieldDict.GetBool(_INCLUDE_UPPER))
                    filter.LessThanOrEqualTo = new DistanceValue(fieldDict.GetString(_TO));
                else
                    filter.LessThan = new DistanceValue(fieldDict.GetString(_TO));
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoDistanceRangeFilter))
                throw new SerializeTypeException<GeoDistanceRangeFilter>();

            GeoDistanceRangeFilter filter = value as GeoDistanceRangeFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_GREATER_THAN, filter.GreaterThan);
            fieldDict.AddObject(_GREATER_THAN_OR_EQUAL_TO, filter.GreaterThanOrEqualTo);
            fieldDict.AddObject(_LESS_THAN, filter.LessThan);
            fieldDict.AddObject(_LESS_THAN_OR_EQUAL_TO, filter.LessThanOrEqualTo);
            fieldDict.Add(filter.Field, filter.CenterPoint);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.GeoDistanceRange.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
