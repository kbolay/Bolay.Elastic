using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Range
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-range-facet.html
    /// </summary>
    [JsonConverter(typeof(RangeFacetSerializer))]
    public class RangeFacet : IFacet
    {
        /// <summary>
        /// Gets the name of the facet.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets the field.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the key field.
        /// </summary>
        public string KeyField { get; set; }

        /// <summary>
        /// Gets the value field.
        /// </summary>
        public string ValueField { get; set; }

        /// <summary>
        /// Gets the key script.
        /// </summary>
        public string KeyScript { get; set; }
        
        /// <summary>
        /// Gets the value script.
        /// </summary>
        public string ValueScript { get; set; }

        /// <summary>
        /// Gets or sets the script parameters.
        /// </summary>
        public IEnumerable<ScriptParameter> ScriptParameters { get; set; }

        /// <summary>
        /// Gets or sets the script language.
        /// </summary>
        public string ScriptLanguage { get; set; }

        /// <summary>
        /// Gets the ranges to determine value terms.
        /// </summary>
        public IEnumerable<RangeBucket> RangeBuckets { get; private set; }

        internal RangeFacet(string facetName)
        {
            if (string.IsNullOrWhiteSpace(facetName))
                throw new ArgumentNullException("facetName", "RangeFacet requires a facet name.");

            FacetName = facetName;
        }

        /// <summary>
        /// Creates a range facet, some combination of key/value field/script values are expected.
        /// </summary>
        /// <param name="facetName"></param>
        /// <param name="rangeBuckets"></param>
        public RangeFacet(string facetName, IEnumerable<RangeBucket> rangeBuckets)
            : this(facetName)
        {
            if (rangeBuckets == null || rangeBuckets.All(x => x == null))
                throw new ArgumentNullException("rangeBuckets", "RangeFacet requires at least one range bucket.");

            RangeBuckets = rangeBuckets.Where(x => x != null);
        }

        /// <summary>
        /// Create a range facet using a field name.
        /// </summary>
        /// <param name="facetName">Sets the facet name.</param>
        /// <param name="field">Sets the field name.</param>
        /// <param name="rangeBuckets">Sets the range buckets.</param>
        public RangeFacet(string facetName, string field, IEnumerable<RangeBucket> rangeBuckets)
            : this(facetName, rangeBuckets)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RangeFacet requires a field value.");

            Field = field;
        }
    }
}
