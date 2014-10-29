using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-near-query.html
    /// </summary>
    [JsonConverter(typeof(SpanNearSerializer))]
    public class SpanNearQuery : SpanQueryBase
    {
        /// <summary>
        /// A collection of span queries.
        /// </summary>
        public IEnumerable<SpanQueryBase> Clauses { get; private set; }

        /// <summary>
        /// The maximum number of intervening unmatched positions.
        /// </summary>
        public int? Slop { get; set; }

        /// <summary>
        /// Require that matches are in order.
        /// </summary>
        public bool RequireMatchesInOrder { get; set; }

        /// <summary>
        /// Collect the payloads of matches.
        /// </summary>
        public bool CollectPayloads { get; set; }

        /// <summary>
        /// Create a span_near query.
        /// </summary>
        /// <param name="clauses">The span queries used to find documents.</param>
        public SpanNearQuery(IEnumerable<SpanQueryBase> clauses)
        {
            if (clauses == null || clauses.All(x => x == null))
                throw new ArgumentNullException("clauses", "SpanNearQuery requires at least one span query for clauses.");

            Clauses = clauses;
        }

    }
}
