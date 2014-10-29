using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-not-query.html
    /// </summary>
    [JsonConverter(typeof(SpanNotSerializer))]
    public class SpanNotQuery : SpanQueryBase
    {
        /// <summary>
        /// A span query defining which documents to returns.
        /// </summary>
        public SpanQueryBase Include { get; private set; }

        /// <summary>
        /// A span query that filters out any matches from the include query.
        /// </summary>
        public SpanQueryBase Exclude { get; private set; }

        public SpanNotQuery(SpanQueryBase include, SpanQueryBase exclude)
        {
            if (include == null)
                throw new ArgumentNullException("include", "SpanNotQuery requires an include query.");
            if (exclude == null)
                throw new ArgumentNullException("exclude", "SpanNotQuery requires an exclude query.");

            Include = include;
            Exclude = exclude;
        }
    }
}
