using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-span-term-query.html
    /// </summary>
    [JsonConverter(typeof(SpanTermSerializer))]
    public class SpanTermQuery : SpanQueryBase
    {
        /// <summary>
        /// The field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The value to search for.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The boost for the query.
        /// </summary>
        public Double Boost { get; set; }

        public SpanTermQuery(string field, string value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "SpanTermQuery requires field.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "SpanTermQuery requires value.");

            Field = field;
            Value = value;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
