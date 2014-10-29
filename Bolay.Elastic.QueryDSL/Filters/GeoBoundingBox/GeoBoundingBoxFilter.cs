using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoBoundingBox
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-bounding-box-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoBoundingBoxSerializer))]
    public class GeoBoundingBoxFilter : FilterBase
    {
        /// <summary>
        /// The geo_point field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The top left coordinates for the box.
        /// </summary>
        public CoordinatePoint TopLeft { get; private set; }

        /// <summary>
        /// The bottom right coordinates for the box.
        /// </summary>
        public CoordinatePoint BottomRight { get; private set; }

        /// <summary>
        /// The execution type for the filter.
        /// Defaults to memory.
        /// </summary>
        public ExecutionTypeEnum Type { get; set; }

        internal GeoBoundingBoxFilter()
        {
            Type = GeoBoundingBoxSerializer._TYPE_DEFAULT;
            Cache = GeoBoundingBoxSerializer._CACHED_DEFAULT;
        }

        /// <summary>
        /// Create a geo_bounding_box filter using coordinate points.
        /// </summary>
        /// <param name="topLeft">The coordinate point for the top left of the bounding box.</param>
        /// <param name="bottomRight">The coordinate point for the bottom right of the bounding box.</param>
        public GeoBoundingBoxFilter(string field, CoordinatePoint topLeft, CoordinatePoint bottomRight) 
            : this()
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoBoundingBoxFilter requires a field.");
            if (topLeft == null)
                throw new ArgumentNullException("topLeft", "GeoBoundingBoxFilter requires a top left coordinate point for this constructor.");
            if (bottomRight == null)
                throw new ArgumentNullException("bottomRight", "GeoBoundingBoxFilter requies a bottom right coordinate point for this constructor.");

            Field = field;
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}
