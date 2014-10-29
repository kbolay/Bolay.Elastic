using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Indices
{
    [JsonConverter(typeof(IndicesSerializer))]
    public class IndicesQuery : QueryBase
    {
        /// <summary>
        /// The indices to run the matching query against.
        /// </summary>
        public IEnumerable<string> Indices { get; private set; }

        /// <summary>
        /// The query to run against the specified indices.
        /// </summary>
        public IQuery MatchingQuery { get; private set; }

        /// <summary>
        /// The type of query to run on indexes that don't match.
        /// If this is set to none or all, the NonMatchingQuery doesn't need to be supplied.
        /// </summary>
        public NonMatchingTypeEnum NonMatchingQueryType { get; private set; }

        /// <summary>
        /// The query to run against any indices not specified.
        /// </summary>
        public IQuery NonMatchingQuery { get; private set; }

        /// <summary>
        /// Create an indices query.
        /// </summary>
        /// <param name="indices">The indices to run the query on.</param>
        /// <param name="query">The query to run on the specified indices.</param>
        /// <param name="nonMatchingQuery">The query to run on the indices that are not specified.</param>
        public IndicesQuery(IEnumerable<string> indices, IQuery matchingQuery, IQuery nonMatchingQuery)
            :this(indices, matchingQuery)
        {
            if (nonMatchingQuery == null)
                throw new ArgumentNullException("nonMatchingQuery", "IndicesQuery expects a nonMatchingQuery.");

            NonMatchingQuery = nonMatchingQuery;
        }

        /// <summary>
        /// Creates an indices query.
        /// </summary>
        /// <param name="indices">The indices to run the query on.</param>
        /// <param name="query">The query to run on the specified indices.</param>
        /// <param name="nonMatchingQueryType">The type of query to run against non specified indices. Options are none or all.</param>
        public IndicesQuery(IEnumerable<string> indices, IQuery matchingQuery, NonMatchingTypeEnum nonMatchingQueryType)
            :this(indices, matchingQuery)
        {
            if (nonMatchingQueryType == null)
                throw new ArgumentNullException("nonMatchingQueryType", "IndicesQuery expects a nonMatchingQueryType.");

            NonMatchingQueryType = nonMatchingQueryType;
        }

        internal IndicesQuery(IEnumerable<string> indices, IQuery matchingQuery)
        {
            if (indices == null || indices.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("indices", "IndicesQuery requires at least one index to be specified.");
            if (matchingQuery == null)
                throw new ArgumentNullException("matchingQuery", "IndicesQuery requires a query.");

            Indices = indices.Where(x => !string.IsNullOrWhiteSpace(x));
            MatchingQuery = matchingQuery;
        }
    }
}
