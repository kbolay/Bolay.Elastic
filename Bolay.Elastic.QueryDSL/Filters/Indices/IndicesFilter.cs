using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Indices
{
    [JsonConverter(typeof(IndicesSerializer))]
    public class IndicesFilter : FilterBase
    {
        /// <summary>
        /// The indices to run the matching filter against.
        /// </summary>
        public IEnumerable<string> Indices { get; private set; }

        /// <summary>
        /// The filter to run against the specified indices.
        /// </summary>
        public IFilter MatchingFilter { get; private set; }

        /// <summary>
        /// The type of filter to run on indexes that don't match.
        /// If this is set to none or all, the NonMatchingFilter doesn't need to be supplied.
        /// </summary>
        public NonMatchingTypeEnum NonMatchingFilterType { get; private set; }

        /// <summary>
        /// The filter to run against any indices not specified.
        /// </summary>
        public IFilter NonMatchingFilter { get; private set; }

        /// <summary>
        /// Create an indices query.
        /// </summary>
        /// <param name="indices">The indices to run the filter on.</param>
        /// <param name="matchingFilter">The filter to run on the specified indices.</param>
        /// <param name="nonMatchingFilter">The filter to run on the indices that are not specified.</param>
        public IndicesFilter(IEnumerable<string> indices, IFilter matchingFilter, IFilter nonMatchingFilter)
            :this(indices, matchingFilter)
        {
            if (nonMatchingFilter == null)
                throw new ArgumentNullException("nonMatchingFilter", "IndicesFilter expects a nonMatchingFilter.");

            NonMatchingFilter = nonMatchingFilter;
        }

        /// <summary>
        /// Creates an indices query.
        /// </summary>
        /// <param name="indices">The indices to run the query on.</param>
        /// <param name="matchingFilter">The filter to run on the specified indices.</param>
        /// <param name="nonMatchingFilterType">The type of filter to run against non specified indices. Options are none or all.</param>
        public IndicesFilter(IEnumerable<string> indices, IFilter matchingFilter, NonMatchingTypeEnum nonMatchingFilterType)
            :this(indices, matchingFilter)
        {
            if (nonMatchingFilterType == null)
                throw new ArgumentNullException("nonMatchingFilterType", "IndicesFilter expects a nonMatchingFilterType.");

            NonMatchingFilterType = nonMatchingFilterType;
        }

        internal IndicesFilter(IEnumerable<string> indices, IFilter matchingFilter)
        {
            if (indices == null || indices.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("indices", "IndicesFilter requires at least one index to be specified.");
            if (matchingFilter == null)
                throw new ArgumentNullException("matchingFilter", "IndicesFilter requires a query.");

            Indices = indices.Where(x => !string.IsNullOrWhiteSpace(x));
            MatchingFilter = matchingFilter;
            Cache = IndicesSerializer._CACHE_DEFAULT;
        }
    }
}
