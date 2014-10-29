using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Query
{
    [JsonConverter(typeof(QueryFacetSerializer))]
    public class QueryFacet : IFacet
    {
        /// <summary>
        /// Gets the facet name.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets the query to use for the facet.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Create a query facet.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="query">Sets the query for the facet.</param>
        public QueryFacet(string facetName, IQuery query)
        {
            if (string.IsNullOrEmpty(facetName))
                throw new ArgumentNullException("facetName", "FilterFacet requires a facet name.");
            if (query == null)
                throw new ArgumentNullException("query", "QueryFacet requires a query.");

            FacetName = facetName;
            Query = query;
        }
    }
}
