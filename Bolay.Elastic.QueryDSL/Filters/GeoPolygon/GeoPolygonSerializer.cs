using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoPolygon
{
    public class GeoPolygonSerializer : JsonConverter
    {
        private const string _DELIMITER = ",";
        private static List<string> _DELIMITERS = new List<string>() { " , ", " ,", ", ", "," };

        internal const bool _CACHE_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.GeoPolygon.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("GeoPointProperty");

            IEnumerable<CoordinatePoint> points = CoordinatePointSerializer.DeserializeCollectionCoordinatePiont(fieldKvp.Value.ToString());

            GeoPolygonFilter filter = new GeoPolygonFilter(fieldKvp.Key, points);
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoPolygonFilter))
                throw new SerializeTypeException<GeoPolygonFilter>();

            GeoPolygonFilter filter = value as GeoPolygonFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (filter.PolygonPoints.All(x => string.IsNullOrWhiteSpace(x.GeoHash)))
            {
                fieldDict.Add(filter.Field, filter.PolygonPoints);
            }
            else
            {
                fieldDict.Add(filter.Field, BuildPolygonWithGeoHashes(filter.PolygonPoints));
            }

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.GeoPolygon.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }

        private List<string> BuildPolygonWithGeoHashes(IEnumerable<CoordinatePoint> points)
        {
            List<string> coordinates = new List<string>();
            foreach (CoordinatePoint point in points)
            {
                StringBuilder coordPoint = new StringBuilder();
                if (point.Latitude != default(Double) || point.Longitude != default(Double) || string.IsNullOrWhiteSpace(point.GeoHash))
                {
                    coordPoint.Append(point.Latitude);
                    coordPoint.Append(_DELIMITER);
                    coordPoint.Append(point.Longitude);
                }
                else
                {
                    coordPoint.Append(point.GeoHash);
                }

                coordinates.Add(coordPoint.ToString());
            }

            return coordinates;
        }
    }
}
