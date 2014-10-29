using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Bolay.Elastic.QueryDSL.Queries.Prefix;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Bolay.Elastic.QueryDSL.Queries.Regex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-multi-term-query.html
    /// </summary>
    [JsonConverter(typeof(SpanMultiTermSerializer))]
    public class SpanMultiTermQuery : SpanQueryBase
    {
        /// <summary>
        /// The query defining what to search for.
        /// </summary>
        public IQuery Match { get; private set; }

        public SpanMultiTermQuery(IQuery match)
        {
            if (match == null)
                throw new ArgumentNullException("match", "SpanMultiTermQuery requires a query.");

            if (!(match is FuzzyQueryBase || match is PrefixQuery || match is RangeQueryBase || match is RegexQuery))
                throw new ArgumentException("Match value is not one of the accepted query types.", "match");

            Match = match;
        }

        public SpanMultiTermQuery(FuzzyQueryBase fuzzyQuery)
        {
            if (fuzzyQuery == null)
                throw new ArgumentNullException("fuzzyQuery", "SpanMultiTermQuery expects a child of FuzzyQueryBase in this constructor.");

            Match = fuzzyQuery;
        }
        public SpanMultiTermQuery(PrefixQuery prefixQuery)
        {
            if (prefixQuery == null)
                throw new ArgumentNullException("prefixQuery", "SpanMultiTermQuery expects a PrefixQuery in this constructor.");

            Match = prefixQuery;
        }
        public SpanMultiTermQuery(RangeQueryBase rangeQuery)
        {
            if (rangeQuery == null)
                throw new ArgumentNullException("rangeQuery", "SpanMultiTermQuery expects a child of RangeQueryBase in this constructor.");

            Match = rangeQuery;
        }
        public SpanMultiTermQuery(RegexQuery regexQuery)
        {
            if (regexQuery == null)
                throw new ArgumentNullException("regexQuery", "SpanMultiTermQuery expects a RegexQuery in this constructor.");

            Match = regexQuery;
        }
    }
}
