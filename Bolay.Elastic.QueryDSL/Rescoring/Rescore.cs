using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Rescoring
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-rescore.html
    /// </summary>
    [JsonConverter(typeof(RescoreSerializer))]
    public class Rescore : ISearchPiece
    {
        /// <summary>
        /// Gets the queries that will rescore the documents.
        /// </summary>
        public IEnumerable<RescoreQuery> RescoreQueries { get; private set; }

        /// <summary>
        /// Create a rescore request.
        /// </summary>
        /// <param name="rescoreQueries">The queries to run that will rescore the documents.</param>
        public Rescore(IEnumerable<RescoreQuery> rescoreQueries)
        {
            if (rescoreQueries == null || rescoreQueries.All(x => x == null))
                throw new ArgumentNullException("rescoreQueries", "RescoreRequest requires at least one rescore query.");

            RescoreQueries = rescoreQueries.Where(x => x != null);
        }
    }
}
