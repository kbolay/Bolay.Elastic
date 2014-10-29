using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoHashCell
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geohash-cell-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoHashCellSerializer))]
    public class GeoHashCellFilter : FilterBase
    {
        /// <summary>
        /// Get the geo_point field to search against.
        /// This field must have a very specific mapping for the filter to work.
        /// </summary>
        public string Field { get; private set; }

        // TODO: test to see if filter using geohash using latitude and longitude.

        /// <summary>
        /// Gets the defined geo hash. This can be done with latitude and longitude or an actual geohash.
        /// </summary>
        public CoordinatePoint GeoHash { get; private set; }

        /// <summary>
        /// Gets the length of the geo hash prefix for the length. The larger the number the more exact the search.
        /// </summary>
        public int? GeoHashPrecision { get; private set; }

        /// <summary>
        /// The amount of distance allowed from the specified geo hash.
        /// </summary>
        public DistanceValue DistancePrecision { get; private set; }

        /// <summary>
        /// Get whether the neighbors of the defined geo hash will be accepted as potential matches.
        /// </summary>
        public bool AllowNeighbors { get; private set; }

        private GeoHashCellFilter(string field, CoordinatePoint geoHash, bool allowNeighbors)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException("field", "GeoHashCellFilter requires a geo_point field.");
            if (geoHash == null)
                throw new ArgumentNullException("geoHash", "GeoHashCellFilter requires a coordinate point defining a geo hash.");

            Field = field;
            GeoHash = geoHash;
            AllowNeighbors = allowNeighbors;
            Cache = GeoHashCellSerializer._CACHE_DEFAULT;
        }

        /// <summary>
        /// Create a geohash_cell filter.
        /// </summary>
        /// <param name="field">The geo_point field to search against.</param>
        /// <param name="geoHash">A coordinate point object defining the geo hash.</param>
        /// <param name="geoHashPrecision">The size of the geo hash prefix that will be considered for the search.</param>
        /// <param name="allowNeighbors">Allow the neighbors of the geo hash to be considered.</param>
        public GeoHashCellFilter(string field, CoordinatePoint geoHash, int geoHashPrecision, bool allowNeighbors)
            : this(field, geoHash, allowNeighbors)
        {
            if (geoHashPrecision <= 0)
                throw new ArgumentOutOfRangeException("geoHashPrecision", "GeoHashCellFilter requires the precision of the geo hash to be greater than zero.");

            GeoHashPrecision = geoHashPrecision;
        }

        /// <summary>
        /// Create a geohash_cell filter.
        /// </summary>
        /// <param name="field">The geo_point field to search against.</param>
        /// <param name="geoHash">A coordinate point object defining the geo hash.</param>
        /// <param name="distancePrecision">The allowed distance from the specified geo hash.</param>
        /// <param name="allowNeighbors">Allow the neighbors of the geo hash to be considered.</param>
        public GeoHashCellFilter(string field, CoordinatePoint geoHash, DistanceValue distancePrecision, bool allowNeighbors)
            : this(field, geoHash, allowNeighbors)
        {
            if (distancePrecision == null)
                throw new ArgumentNullException("distancePrecision", "GeoHashCellFilter requires a distance precision for this constructor.");

            DistancePrecision = distancePrecision;
        }
    }
}
