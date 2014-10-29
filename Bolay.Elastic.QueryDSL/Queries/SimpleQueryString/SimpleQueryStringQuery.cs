using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.SimpleQueryString
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-simple-query-string-query.html
    /// </summary>
    [JsonConverter(typeof(SimpleQueryStringSerializer))]
    public class SimpleQueryStringQuery : QueryBase
    {
        /// <summary>
        /// The fields to search against.
        /// Defaults to _all in ES.
        /// </summary>
        public IEnumerable<string> Fields { get; private set; }

        /// <summary>
        /// The values to search for.
        /// </summary>
        public string Query { get; private set; }

        /// <summary>
        /// The default operator for items inside the query.
        /// Defaults to or.
        /// </summary>
        public OperatorEnum DefaultOperator { get; set; }

        /// <summary>
        /// Set an anaylyzer to analyze the text.
        /// Defaults to the fields mapped analyzers.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Determine which parsing features to activate for the query.
        /// Defaults to ALL.
        /// </summary>
        public IEnumerable<ParsingFeatureEnum> ParsingFeatureFlags { get; set; }

        internal SimpleQueryStringQuery()
        {
            DefaultOperator = SimpleQueryStringSerializer._DEFAULT_OPERATOR_DEFAULT;
            ParsingFeatureFlags = SimpleQueryStringSerializer._FLAGS_DEFAULT;
        }

        public SimpleQueryStringQuery(string query)
            : this()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "SimpleQueryStringQuery requires a query.");

            Query = query;
        }

        public SimpleQueryStringQuery(IEnumerable<string> fields, string query)
            : this(query)
        {
            if (fields == null || fields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("fields", "SimpleQueryStringQuery expects at least one field in this constructor.");

            Fields = fields.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
