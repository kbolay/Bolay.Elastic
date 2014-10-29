using Bolay.Elastic.Coordinates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoPolygon
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-polygon-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoPolygonSerializer))]
    public class GeoPolygonFilter : FilterBase
    {
        /// <summary>
        /// Gets the geo_point field this filter will be performed against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Get the points forming a polygon to search with.
        /// </summary>
        public IEnumerable<CoordinatePoint> PolygonPoints { get; private set; }

        /// <summary>
        /// Create a geo_polygon filter.
        /// </summary>
        /// <param name="field">The geo_point field in the documents.</param>
        /// <param name="polygonPoints">The coordinates of the polygon.</param>
        public GeoPolygonFilter(string field, IEnumerable<CoordinatePoint> polygonPoints)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoPolygonFilter requires a field.");
            if (polygonPoints == null || polygonPoints.All(x => x == null) || polygonPoints.Count(x => x != null) < 3)
                throw new ArgumentNullException("polygonPoints", "GeoPolygonFilter requires at least three coordinates points to create the polygon.");

            Field = field;
            PolygonPoints = polygonPoints.Where(x => x != null);
            Cache = GeoPolygonSerializer._CACHE_DEFAULT;
        }
    }
}
