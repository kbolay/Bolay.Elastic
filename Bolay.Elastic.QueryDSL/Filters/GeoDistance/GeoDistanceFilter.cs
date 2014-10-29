using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoDistance
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-distance-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoDistanceSerializer))]
    public class GeoDistanceFilter : FilterBase
    {
        /// <summary>
        /// The geo point field in the documents to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The length of the radius.
        /// </summary>
        public DistanceValue Distance { get; private set; }

        /// <summary>
        /// The center point to search from.
        /// </summary>
        public CoordinatePoint CenterPoint { get; private set; }

        /// <summary>
        /// The method used to compute the distance.
        /// Defaults to sloppy_arc.
        /// </summary>
        public DistanceComputeTypeEnum DistanceComputeMethod { get; set; }

        /// <summary>
        /// Determines if the distance query is optimized by using a bounding box.
        /// Defaults to memory.
        /// </summary>
        public BoundingBoxOptimizeEnum OptimizeBoundingBox { get; set; }

        /// <summary>
        /// Create a geo_distance filter that searches from a central coordinate point.
        /// </summary>
        /// <param name="field">The document geo_point field.</param>
        /// <param name="distance">The length of the radius.</param>
        /// <param name="centerPoint">The central latitude and longitude of the search.</param>
        public GeoDistanceFilter(string field, DistanceValue distance, CoordinatePoint centerPoint)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoDistanceFilter requires a field.");
            if (distance == null)
                throw new ArgumentNullException("distance", "GeoDistanceFilter requires a distance.");
            if(centerPoint == null)
                throw new ArgumentNullException("centerPoint", "GeoDistanceFilter requires a central coordinate point.");

            Field = field;
            Distance = distance;
            CenterPoint = centerPoint;
            DistanceComputeMethod = GeoDistanceSerializer._DISTANCE_TYPE_DEFAULT;
            OptimizeBoundingBox = GeoDistanceSerializer._OPTIMIZE_BBOX_DEFAULT;
            Cache = GeoDistanceSerializer._CACHE_DEFAULT;
        }
    }
}
