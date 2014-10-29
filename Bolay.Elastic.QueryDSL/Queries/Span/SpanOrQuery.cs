using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-or-query.html
    /// </summary>
    [JsonConverter(typeof(SpanOrSerializer))]
    public class SpanOrQuery : SpanQueryBase
    {
        /// <summary>
        /// A collection of span queries.
        /// </summary>
        public IEnumerable<SpanQueryBase> Clauses { get; private set; }

        public SpanOrQuery(IEnumerable<SpanQueryBase> clauses)
        {
            if (clauses == null || clauses.All(x => x == null))
                throw new ArgumentNullException("clauses", "SpanOrQuery requires at least one span query.");

            Clauses = clauses;
        }
    }
}
