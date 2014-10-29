using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.IndexBoosts
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-index-boost.html
    /// </summary>
    [JsonConverter(typeof(IndicesBoostSerializer))]
    public class IndicesBoost : ISearchPiece
    {
        public IEnumerable<IndexBoost> BoostedIndices { get; private set; }

        public IndicesBoost(IEnumerable<IndexBoost> boostedIndices)
        {
            if (boostedIndices == null || boostedIndices.All(x => x == null))
                throw new ArgumentNullException("boostedIndices", "IndicesBoost requires at least one IndexBoost.");

            BoostedIndices = boostedIndices.Where(x => x != null);
        }
    }
}
