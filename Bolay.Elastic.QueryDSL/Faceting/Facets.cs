using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets.html
    /// </summary>
    [JsonConverter(typeof(FacetSerializer))]
    public class Facets : ISearchPiece
    {
        public IEnumerable<IFacet> FacetGenerators { get; private set; }

        public Facets(IEnumerable<IFacet> facetGenerators)
        {
            if (facetGenerators == null || facetGenerators.All(x => x == null))
                throw new ArgumentNullException("facetGenerators", "Facets requires at least one facet generator.");

            FacetGenerators = facetGenerators.Where(x => x != null);
        }
    }
}
