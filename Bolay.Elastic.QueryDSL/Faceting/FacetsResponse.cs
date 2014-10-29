using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    /// <summary>
    /// The results of a facets request.
    /// </summary>
    [JsonConverter(typeof(FacetResponseSerializer))]
    public class FacetsResponse
    {
        /// <summary>
        /// Gets the results of the facet requests.
        /// </summary>
        public IEnumerable<IFacetResponse> FacetResults { get; private set; }

        internal FacetsResponse(IEnumerable<IFacetResponse> facetResults)
        {
            FacetResults = facetResults;
        }
    }
}
