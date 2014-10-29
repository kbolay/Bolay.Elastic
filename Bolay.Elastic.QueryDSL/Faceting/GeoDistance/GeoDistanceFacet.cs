using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.GeoDistance
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-geo-distance-facet.html
    /// </summary>
    [JsonConverter(typeof(GeoDistanceFacetSerializer))]
    public class GeoDistanceFacet : IFacet
    {
        /// <summary>
        /// Gets the name of the facet.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets or sets the geo_point field to get calculate distance with.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the center point to calculate distance from.
        /// </summary>
        public CoordinatePoint CenterPoint { get; private set; }

        /// <summary>
        /// Gets the grouping of distances for the facet.
        /// </summary>
        public IEnumerable<DistanceBucket> RangeBuckets { get; set; }

        /// <summary>
        /// Gets or sets the value field.
        /// </summary>
        public string ValueField { get; set; }

        /// <summary>
        /// Gets or sets the value script.
        /// </summary>
        public string ValueScript { get; set; }

        /// <summary>
        /// Gets or sets the script parameters.
        /// </summary>
        public IEnumerable<ScriptParameter> ScriptParameters { get; set; }

        /// <summary>
        /// Gets or sets the scripting language used in any script property values.
        /// </summary>
        public string ScriptLanguage { get; set; }

        /// <summary>
        /// Creates a geo distance facet.
        /// </summary>
        /// <param name="facetName">Sets the facet name.</param>
        /// <param name="field">Sets the geo_point field that will be used to calculate distance.</param>
        /// <param name="centerPoint">Sets the central point to calculate distance with.</param>
        /// <param name="rangeBuckets">Sets the distance range buckets.</param>
        public GeoDistanceFacet(string facetName, string field, CoordinatePoint centerPoint, IEnumerable<DistanceBucket> rangeBuckets)
        {
            if (string.IsNullOrWhiteSpace(facetName))
                throw new ArgumentNullException("facetName", "GeoDistanceFacet requires a facet name.");
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoDistanceFacet requires a field.");
            if (centerPoint == null)
                throw new ArgumentNullException("centerPoint", "GeoDistanceFacet requires a center point.");
            if (rangeBuckets == null || rangeBuckets.All(x => x == null))
                throw new ArgumentNullException("rangeBuckets", "GeoDistanceFacet requires at least one distance range bucket.");

            FacetName = facetName;
            Field = field;
            CenterPoint = centerPoint;
            RangeBuckets = rangeBuckets.Where(x => x != null);
        }
    }
}
