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
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-distance-range-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoDistanceRangeSerializer))]
    public class GeoDistanceRangeFilter : FilterBase
    {
        private DistanceValue _GreaterThan { get; set; }
        private DistanceValue _LessThan { get; set; }
        private DistanceValue _GreaterThanOrEqualTo { get; set; }
        private DistanceValue _LessThanOrEqualTo { get; set; }

        /// <summary>
        /// The geo point field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The central point to search from.
        /// </summary>
        public CoordinatePoint CenterPoint { get; private set; }

        /// <summary>
        /// Matching documents must have a geo_point greater than this distance from the center point.
        /// </summary>
        public DistanceValue GreaterThan 
        {
            get
            {
                return _GreaterThan;
            }
            set
            {
                if (value != null && _GreaterThanOrEqualTo != null)
                    throw new ConflictingPropertiesException(new List<string>() { "GreaterThan", "GreaterThanOrEqualTo" });

                _GreaterThan = value;
            }
        }

        /// <summary>
        /// Matching documents must have a geo_point less than this distance from the center point.
        /// </summary>
        public DistanceValue LessThan
        {
            get
            {
                return _LessThan;
            }
            set
            {
                if (value != null && _LessThanOrEqualTo != null)
                    throw new ConflictingPropertiesException(new List<string>() { "LessThan", "LessThanOrEqualTo" });

                _LessThan = value;
            }
        }

        /// <summary>
        /// Matching documents must have a geo_point greater than or equal to this distance from the center point.
        /// </summary>
        public DistanceValue GreaterThanOrEqualTo
        {
            get
            {
                return _GreaterThanOrEqualTo;
            }
            set
            {
                if (value != null && _GreaterThan != null)
                    throw new ConflictingPropertiesException(new List<string>() { "GreaterThan", "GreaterThanOrEqualTo" });

                _GreaterThanOrEqualTo = value;
            }
        }

        /// <summary>
        /// Matching documents must have a geo_point less than or equal to this distance from the center point.
        /// </summary>
        public DistanceValue LessThanOrEqualTo
        {
            get
            {
                return _LessThanOrEqualTo;
            }
            set
            {
                if (value != null && _LessThan != null)
                    throw new ConflictingPropertiesException(new List<string>() { "LessThan", "LessThanOrEqualTo" });

                _LessThanOrEqualTo = value;
            }
        }

        /// <summary>
        /// Create a geo_distance_range filter.
        /// </summary>
        /// <param name="field">The geo_point field in the document.</param>
        /// <param name="point">The central point to search from.</param>
        public GeoDistanceRangeFilter(string field, CoordinatePoint centerPoint)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoDistanceRangeFilter requires a field.");
            if (centerPoint == null)
                throw new ArgumentNullException("centerPoint", "GeoDistanceRangeFilter requires a central point.");

            Field = field;
            CenterPoint = centerPoint;
            Cache = GeoDistanceRangeSerializer._CACHE_DEFAULT;
        }
    }
}
