using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoHashCell
{
    public class GeoHashCellSerializer : JsonConverter
    {
        private const string _PRECISION = "precision";
        private const string _NEIGHBORS = "neighbors";

        internal const bool _CACHE_DEFAULT = false;

        private static List<string> _KnownFields = new List<string>()
        {
            _PRECISION,
            _NEIGHBORS,
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
            if (fieldDict.ContainsKey(FilterTypeEnum.GeoHashCell.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("GeoPointProperty");

            GeoHashCellFilter filter = null;
            CoordinatePoint geoHash = CoordinatePointSerializer.DeserializeCoordinatePoint(fieldDict.GetString(fieldKvp.Key));
            bool useNeighbors = fieldDict.GetBool(_NEIGHBORS);

            // try to create the geo hash cell filter with standard precision, if that fails use distance precision
            try
            {
                filter = new GeoHashCellFilter(
                                    fieldKvp.Key, 
                                    geoHash, 
                                    fieldDict.GetInt32(_PRECISION), 
                                    useNeighbors);
            }
            catch(ParsingException<Int32> ex)
            {
                filter = new GeoHashCellFilter(
                                    fieldKvp.Key,
                                    geoHash,
                                    new DistanceValue(fieldDict.GetString(_PRECISION)),
                                    useNeighbors);
            }

            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoHashCellFilter))
                throw new SerializeTypeException<GeoHashCellFilter>();

            GeoHashCellFilter filter = value as GeoHashCellFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, filter.GeoHash);
            if (filter.GeoHashPrecision.HasValue)
                fieldDict.Add(_PRECISION, filter.GeoHashPrecision.Value);
            else
                fieldDict.Add(_PRECISION, filter.DistancePrecision);
            fieldDict.Add(_NEIGHBORS, filter.AllowNeighbors);

            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            
            Dictionary<string, object> filterDict = new Dictionary<string,object>();
            filterDict.Add(FilterTypeEnum.GeoHashCell.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
