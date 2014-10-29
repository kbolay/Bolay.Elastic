using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Filter
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-filter-facet.html
    /// </summary>
    [JsonConverter(typeof(FilterFacetSerializer))]
    public class FilterFacet : IFacet
    {
        /// <summary>
        /// Gets the facet name.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets the filter of the facet.
        /// </summary>
        public IFilter Filter { get; private set; }

        /// <summary>
        /// Creates a filter facet using a filter.
        /// </summary>
        /// <param name="facetName">Sets the filter name.</param>
        /// <param name="filter">Sets the filter to use for the facet.</param>
        public FilterFacet(string facetName, IFilter filter)
        {
            if (string.IsNullOrEmpty(facetName))
                throw new ArgumentNullException("facetName", "FilterFacet requires a facet name.");
            if (filter == null)
                throw new ArgumentNullException("filter", "FilterFacet requires a filter.");

            FacetName = facetName;
            Filter = filter;
        }
    }
}
