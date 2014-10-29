using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Bool
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-bool-filter.html
    /// </summary>
    [JsonConverter(typeof(BoolSerializer))]
    public class BoolFilter : FilterBase
    {
        /// <summary>
        /// Filters that must match the document.
        /// </summary>
        public IEnumerable<IFilter> MustFilters { get; set; }

        /// <summary>
        /// Filters that must not match the document.
        /// </summary>
        public IEnumerable<IFilter> MustNotFilters { get; set; }

        /// <summary>
        /// Filters that are optional to match the the document.
        /// </summary>
        public IEnumerable<IFilter> ShouldFilters { get; set; }

        /// <summary>
        /// Look to ES and Lucene documentation for information on this property.
        /// </summary>
        public bool DisableCoords { get; set; }

        /// <summary>
        /// Describe the number or percentage of should filters that are required.
        /// </summary>
        public MinimumShouldMatchBase MinimumShouldMatch { get; set; }

        internal BoolFilter()
        {
            Cache = BoolSerializer._CACHE_DEFAULT;
            MinimumShouldMatch = BoolSerializer._MINIMUM_SHOULD_MATCH_DEFAULT;
            DisableCoords = BoolSerializer._DISABLE_COORDS_DEFAULT;
        }

        /// <summary>
        /// Create a bool filter.
        /// </summary>
        /// <param name="mustFilter">The resulting documents must match these filters.</param>
        /// <param name="mustNotFilters">The resulting documents cannot match these filters.</param>
        /// <param name="shouldFilters">The resulting document may or may not match these filters.</param>
        public BoolFilter(IEnumerable<IFilter> mustFilter, IEnumerable<IFilter> mustNotFilters, IEnumerable<IFilter> shouldFilters)
            : this()
        {
            if ((mustFilter == null || !mustFilter.Any()) &&
                (mustNotFilters == null || !mustNotFilters.Any()) &&
                (shouldFilters == null || !shouldFilters.Any()))
            {
                throw new ArgumentNullException("filters", "BoolFilter requires at least one filter be provided.");
            }

            MustFilters = mustFilter;
            ShouldFilters = shouldFilters;
            MustNotFilters = mustNotFilters;
        } 
    }
}
