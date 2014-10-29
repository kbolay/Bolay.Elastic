using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.GeoPoint
{
    internal class GeoPointPropertySerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _INDEX_LAT_LON = "lat_lon";
        private const string _INDEX_GEO_HASH = "geohash";
        private const string _INDEX_GEO_HASH_PREFIX = "geohash_prefix";
        private const string _GEO_HASH_PRECISION = "geohash_precision";
        private const string _VALIDATE = "validate";
        private const string _VALIDATE_LAT = "validate_lat";
        private const string _VALIDATE_LON = "validate_lon";
        private const string _NORMALIZE = "normalize";
        private const string _NORMALIZE_LAT = "normalize_lat";
        private const string _NORMALIZE_LON = "normalize_lon";
        private const string _FORMAT = "format";
        private const string _PRECISION = "precision";
        private const string _FIELD_DATA = "fielddata";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            GeoPointProperty prop = new GeoPointProperty(propDict.First().Key);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());
            DocumentPropertyBase.Deserialize(prop, fieldDict);
            prop.IndexLatLon = fieldDict.GetBool(_INDEX_LAT_LON, GeoPointProperty._INDEX_LAT_LON_DEFAULT);
            prop.IndexGeoHash = fieldDict.GetBool(_INDEX_GEO_HASH, GeoPointProperty._INDEX_GEO_HASH_DEFAULT);
            prop.IndexGeoHashPrefix = fieldDict.GetBool(_INDEX_GEO_HASH_PREFIX, GeoPointProperty._INDEX_GEO_HASH_PREFIX_DEFAULT);
            prop.GeoHashPrecision = fieldDict.GetInt32(_GEO_HASH_PRECISION, GeoPointProperty._GEO_HASH_PRECISION_DEFAULT);
            prop.Validate = fieldDict.GetBool(_VALIDATE, GeoPointProperty._VALIDATE_DEFAULT);
            prop.ValidateLatitude = fieldDict.GetBool(_VALIDATE_LAT, GeoPointProperty._VALIDATE_LAT_DEFAULT);
            prop.ValidateLongitude = fieldDict.GetBool(_VALIDATE_LON, GeoPointProperty._VALIDATE_LON_DEFAULT);
            prop.Normalize = fieldDict.GetBool(_NORMALIZE, GeoPointProperty._NORMALIZE_DEFAULT);
            prop.NormalizeLatitude = fieldDict.GetBool(_NORMALIZE_LAT, GeoPointProperty._NORMALIZE_LAT_DEFAULT);
            prop.NormalizeLongitude = fieldDict.GetBool(_NORMALIZE_LON, GeoPointProperty._NORMALIZE_LON_DEFAULT);

            if (fieldDict.ContainsKey(_FIELD_DATA))
            {
                Dictionary<string, object> fieldDataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_FIELD_DATA));

                if (fieldDataDict.ContainsKey(_PRECISION))
                    prop.CompressionPrecision = new Distance.DistanceValue(fieldDataDict.GetString(_PRECISION));
            }

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is GeoPointProperty))
                throw new SerializeTypeException<GeoPointProperty>();

            GeoPointProperty prop = value as GeoPointProperty;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            DocumentPropertyBase.Serialize(prop, fieldDict);
            fieldDict.AddObject(_INDEX_LAT_LON, prop.IndexLatLon, GeoPointProperty._INDEX_LAT_LON_DEFAULT);
            fieldDict.AddObject(_INDEX_GEO_HASH, prop.IndexGeoHash, GeoPointProperty._INDEX_GEO_HASH_DEFAULT);
            fieldDict.AddObject(_INDEX_GEO_HASH_PREFIX, prop.IndexGeoHashPrefix, GeoPointProperty._INDEX_GEO_HASH_PREFIX_DEFAULT);
            fieldDict.AddObject(_GEO_HASH_PRECISION, prop.GeoHashPrecision, GeoPointProperty._GEO_HASH_PRECISION_DEFAULT);
            fieldDict.AddObject(_VALIDATE, prop.Validate, GeoPointProperty._VALIDATE_DEFAULT);
            fieldDict.AddObject(_VALIDATE_LAT, prop.ValidateLatitude, GeoPointProperty._VALIDATE_LAT_DEFAULT);
            fieldDict.AddObject(_VALIDATE_LON, prop.ValidateLongitude, GeoPointProperty._VALIDATE_LON_DEFAULT);
            fieldDict.AddObject(_NORMALIZE, prop.Normalize, GeoPointProperty._NORMALIZE_DEFAULT);
            fieldDict.AddObject(_NORMALIZE_LAT, prop.NormalizeLatitude, GeoPointProperty._NORMALIZE_LAT_DEFAULT);
            fieldDict.AddObject(_NORMALIZE_LON, prop.NormalizeLongitude, GeoPointProperty._NORMALIZE_LON_DEFAULT);

            if (prop.CompressionPrecision != null && prop.CompressionPrecision.ToString() != GeoPointProperty._COMPRESSION_PRECISION_DEFAULT.ToString())
            {
                Dictionary<string, object> fieldDataDict = new Dictionary<string, object>();
                fieldDataDict.Add(_FORMAT, GeoPointProperty._FIELD_DATA_FORMAT_DEFAULT);
                fieldDataDict.Add(_PRECISION, prop.CompressionPrecision);

                fieldDict.Add(_FIELD_DATA, fieldDataDict);
            }

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
