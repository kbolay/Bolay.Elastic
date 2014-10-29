using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-first-query.html
    /// </summary>
    [JsonConverter(typeof(SpanFirstSerializer))]
    public class SpanFirstQuery : SpanQueryBase
    {
        /// <summary>
        /// Any other span query.
        /// </summary>
        public SpanQueryBase Match { get; private set; }

        /// <summary>
        /// Set the maximum end position allowed for any match to the internal span query.
        /// </summary>
        public Int64 End { get; private set; }

        public SpanFirstQuery(SpanQueryBase match, Int64 end)
        {
            if (match == null)
                throw new ArgumentNullException("match", "SpanFirstQuery requires a match span query.");
            if (match is SpanFirstQuery)
                throw new ArgumentException("SpanFirstQuery requires the match query to be a different type of match query.", "match");
            if (end <= 0)
                throw new ArgumentOutOfRangeException("end", "SpanFirstQuery requires the end value to be greater than zero.");

            Match = match;
            End = end;
        }
    }
}
