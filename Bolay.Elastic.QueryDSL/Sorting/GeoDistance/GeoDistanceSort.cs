using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.GeoDistance
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-sort.html#_geo_distance_sorting
    /// </summary>
    [JsonConverter(typeof(GeoDistanceSerializer))]
    public class GeoDistanceSort : ISortClause
    {
        /// <summary>
        /// Gets the geo_point field in the documents to determine distance with.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the center point to determine distance from.
        /// </summary>
        public CoordinatePoint CenterPoint { get; private set; }

        // TODO: Test whether unit is actually required.
        // TODO: If not required what is the default value?

        /// <summary>
        /// Gets the unit of distance to use when calculating the distance between the points.
        /// </summary>
        public DistanceUnitEnum Unit { get; private set; }

        /// <summary>
        /// Gets or sets the order to sort the documents based on the distance.
        /// Defaults to ascending.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the method of sorting to be used for arrays.
        /// </summary>
        public SortModeEnum SortMode { get; set; }

        /// <summary>
        /// Gets or sets the ability to reverse the sort.
        /// </summary>
        public bool Reverse { get; set; }

        public GeoDistanceSort(string field, CoordinatePoint centerPoint, DistanceUnitEnum unit)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoDistanceSort requires a field.");
            if (centerPoint == null)
                throw new ArgumentNullException("centerPoint", "GeoDistanceSort requires a center point.");
            if (unit == null)
                throw new ArgumentNullException("unit", "GeoDistanceSort requiers a unit of distance.");

            Field = field;
            CenterPoint = centerPoint;
            Unit = unit;
            SortOrder = SortClauseSerializer._ORDER_DEFAULT;
            Reverse = SortClauseSerializer._REVERSE_DEFAULT;
        }
    }
}
