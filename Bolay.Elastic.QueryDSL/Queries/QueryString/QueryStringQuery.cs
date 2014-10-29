using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.QueryString
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-query-string-query.html
    /// </summary>
    [JsonConverter(typeof(QueryStringSerializer))]
    public class QueryStringQuery : QueryBase
    {
        public int NumberOfFields 
        {
            get 
            {
                if (Fields == null || Fields.All(x => string.IsNullOrWhiteSpace(x)))
                    return 0;

                return Fields.Count(x => !string.IsNullOrWhiteSpace(x));
            }
        }

        /// <summary>
        /// The fields to search against. If only one field is given
        /// then that will be used as the default_field.
        /// Defaults to _all inside of ES.
        /// Populate through the constructor.
        /// </summary>
        public IEnumerable<string> Fields { get; private set; }

        /// <summary>
        /// The values to search for.
        /// </summary>
        public string Query { get; private set; }

        /// <summary>
        /// The operator that separates the terms if no other operator is specified.
        /// Defaults to "or".
        /// </summary>
        public OperatorEnum DefaultOperator { get; set; }

        /// <summary>
        /// The analyzer used to on the values in the query.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Allow a wildcard, '*' or '?', to be the first character on a query term.
        /// Defaults to true.
        /// </summary>
        public bool AllowLeadingWildcard { get; set; }

        /// <summary>
        /// Automatically lowercase the query terms.
        /// Defaults to true.
        /// </summary>
        public bool LowerCaseExpandedTerms { get; set; }

        /// <summary>
        /// Enable the position increments.
        /// Defaults to true.
        /// </summary>
        public bool EnablePositionIncrements { get; set; }

        /// <summary>
        /// Control the number of terms a fuzzy query will expand to.
        /// Defaults to 50.
        /// </summary>
        public int FuzzyMaximumExpansions { get; set; }

        // TODO: Replace with real stuff that actually follows documentation.
        /// <summary>
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/common-options.html#fuzziness
        /// Defaults to "AUTO" inside of ES.
        /// </summary>
        public Double? Fuzziness { get; set; }

        /// <summary>
        /// Set the fuzzy query prefix length.
        /// Default to 0.
        /// </summary>
        public int FuzzyPrefixLength { get; set; }

        /// <summary>
        /// Set the slop match for the phrases in the query. Zero means that exact phrase matches are required.
        /// Default to 0.
        /// </summary>
        public int PhraseSlop { get; set; }

        /// <summary>
        /// Boost value for the query.
        /// Defaults to 1.0.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// Analyze wildcards in the query.
        /// Defaults to false.
        /// </summary>
        public bool AnalyzeWildcard { get; set; }

        /// <summary>
        /// Defaults to false.
        /// </summary>
        public bool AutoGeneratePhraseQueries { get; set; }

        /// <summary>
        /// The number of should clauses from generated queries that must match.
        /// Defaults to 1.
        /// </summary>
        public MinimumShouldMatchBase MinimumShouldMatch { get; set; }

        /// <summary>
        /// Request that ES be lenient and ignore format based failures.
        /// Default to false.
        /// </summary>
        public bool IsLenient { get; set; }

        /// <summary>
        /// If set to true ES will use a dis_max query internally.
        /// If set to false ES will use a bool query internally.
        /// This value is only used when multiple fields are specified in the Fields property.
        /// Defaults to true.
        /// </summary>
        public bool UseDisMax { get; set; }

        /// <summary>
        /// The tie breaker for the dis_max query.
        /// This value is only used if multiple fields are specified and use_dis_mas is true.
        /// Defaults to 0.
        /// </summary>
        public int TieBreaker { get; set; }

        internal QueryStringQuery()
        {
            DefaultOperator = QueryStringSerializer._DEFAULT_OPERATOR_DEFAULT;
            AllowLeadingWildcard = QueryStringSerializer._ALLOW_LEADING_WILDCARD_DEFAULT;
            LowerCaseExpandedTerms = QueryStringSerializer._LOWERCASE_EXPANDED_TERMS_DEFAULT;
            EnablePositionIncrements = QueryStringSerializer._ENABLE_POSITION_INCREMENTS_DEFAULT;
            FuzzyMaximumExpansions = QueryStringSerializer._FUZZY_MAXIMUM_EXPANSIONS_DEFAULT;
            FuzzyPrefixLength = QueryStringSerializer._FUZZY_PREFIX_LENGTH_DEFAULT;
            PhraseSlop = QueryStringSerializer._PHRASE_SLOP_DEFAULT;
            Boost = QuerySerializer._BOOST_DEFAULT;
            AnalyzeWildcard = QueryStringSerializer._ANALYZE_WILDCARD_DEFAULT;
            AutoGeneratePhraseQueries = QueryStringSerializer._AUTO_GENERATE_PHRASE_QUERIES_DEFAULT;
            MinimumShouldMatch = QueryStringSerializer._MINIMUM_SHOULD_MATCH_DEFAULT;
            IsLenient = QueryStringSerializer._IS_LENIENT_DEFAULT;
            UseDisMax = QueryStringSerializer._USE_DIS_MAX_DEFAULT;
            TieBreaker = QueryStringSerializer._TIE_BREAKER_DEFAULT;
        }

        /// <summary>
        /// Create a query_string query against the _all field.
        /// </summary>
        /// <param name="query"></param>
        public QueryStringQuery(string query) : this()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "QueryStringQuery requires a query.");

            Query = query;
        }

        public QueryStringQuery(string field, string query)
            : this(new List<string>(){ field }, query)
        { }

        public QueryStringQuery(IEnumerable<string> fields, string query)
            : this(query)
        {
            if (fields == null || fields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("field", "This constructor for the QueryStringQuery requires at least one field.");

            Fields = fields;
        }
    }
}
