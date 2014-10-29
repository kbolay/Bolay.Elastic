using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.GeoPoint
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-geo-point-type.html
    /// </summary>
    [JsonConverter(typeof(GeoPointPropertySerializer))]
    public class GeoPointProperty : DocumentPropertyBase
    {
        internal const bool _INDEX_LAT_LON_DEFAULT = false;
        internal const bool _INDEX_GEO_HASH_DEFAULT = false;
        internal const int _GEO_HASH_PRECISION_DEFAULT = 12;
        internal const bool _INDEX_GEO_HASH_PREFIX_DEFAULT = false;
        internal const bool _VALIDATE_DEFAULT = false;
        internal const bool _VALIDATE_LAT_DEFAULT = false;
        internal const bool _VALIDATE_LON_DEFAULT = false;
        internal const bool _NORMALIZE_DEFAULT = true;
        internal const bool _NORMALIZE_LAT_DEFAULT = false;
        internal const bool _NORMALIZE_LON_DEFAULT = false;
        internal const string _FIELD_DATA_FORMAT_DEFAULT = "compressed";
        internal static DistanceValue _COMPRESSION_PRECISION_DEFAULT = new DistanceValue(1, DistanceUnitEnum.Centimeter);

        /// <summary>
        /// Gets or sets whether to index the .lat and .lon as fields. 
        /// Defaults to false.
        /// </summary>
        public bool IndexLatLon { get; set; }

        /// <summary>
        /// Gets or sets whether to index the .geohash as a field. 
        /// Defaults to false.
        /// </summary>
        public bool IndexGeoHash { get; set; }

        /// <summary>
        /// Gets or sets whether the prefixes of the provided geohash are indexed. If set to true, the full geohash is also indexed.
        /// Defaults to false.
        /// </summary>
        public bool IndexGeoHashPrefix { get; set; }

        /// <summary>
        /// Gets or sets the geohash precision.
        /// Defaults to 12.
        /// </summary>
        public int GeoHashPrecision { get; set; }

        /// <summary>
        /// Gets or sets whether to reject geo points with invalid latitude or longitude.
        /// Note: Validation only works when Normalize is false.
        /// Defaults to false.
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// Gets or sets whether to reject geo points with an invalid latitude.
        /// Defaults to false.
        /// </summary>
        public bool ValidateLatitude { get; set; }

        /// <summary>
        /// Gets or sets whether to reject geo points with an invalid longitude.
        /// Defaults to false.
        /// </summary>
        public bool ValidateLongitude { get; set; }

        /// <summary>
        /// Gets or sets whether to normalize latitude and longitude.
        /// Defaults to true.
        /// </summary>
        public bool Normalize { get; set; }
        
        /// <summary>
        /// Gets or sets whether to normalize latitude.
        /// Defaults to false.
        /// </summary>
        public bool NormalizeLatitude { get; set; }

        /// <summary>
        /// Gets or sets whether to normalize longitude.
        /// Defaults to false.
        /// </summary>
        public bool NormalizeLongitude { get; set; }

        /// <summary>
        /// Gets or sets the amount of lossy compression to use.
        /// Defaults to 1cm.
        /// </summary>
        public DistanceValue CompressionPrecision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public GeoPointProperty(string name) : base(name, PropertyTypeEnum.GeoPoint)
        {
            IndexLatLon = _INDEX_LAT_LON_DEFAULT;
            IndexGeoHash = _INDEX_GEO_HASH_DEFAULT;
            IndexGeoHashPrefix = _INDEX_GEO_HASH_PREFIX_DEFAULT;
            GeoHashPrecision = _GEO_HASH_PRECISION_DEFAULT;
            Validate = _VALIDATE_DEFAULT;
            ValidateLatitude = _VALIDATE_LAT_DEFAULT;
            ValidateLongitude = _VALIDATE_LON_DEFAULT;
            Normalize = _NORMALIZE_DEFAULT;
            NormalizeLatitude = _NORMALIZE_LAT_DEFAULT;
            NormalizeLongitude = _NORMALIZE_LON_DEFAULT;
            CompressionPrecision = _COMPRESSION_PRECISION_DEFAULT;
        }
    }
}
