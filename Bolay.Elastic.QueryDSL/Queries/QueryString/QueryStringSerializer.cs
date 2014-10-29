using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.QueryString
{
    public class QueryStringSerializer : JsonConverter
    {
        private const string _QUERY = "query";
        private const string _DEFAULT_FIELD = "default_field";
        private const string _FIELDS = "fields";
        private const string _DEFAULT_OPERATOR = "default_operator";
        private const string _ANALYZER = "analzyer";
        private const string _ALLOW_LEADING_WILDCARD = "allow_leading_wildcard";
        private const string _LOWERCASE_EXPANDED_TERMS = "lowercase_expanded_terms";
        private const string _ENABLE_POSITION_INCREMENTS = "enable_position_increments";
        private const string _FUZZY_MAXIMUM_EXPANSIONS = "fuzzy_maximum_expansions";
        private const string _FUZZINESS = "fuzziness";
        private const string _FUZZY_PREFIX_LENGTH = "fuzzy_prefix_length";
        private const string _PHRASE_SLOP = "phrase_slop";
        private const string _ANALYZE_WILDCARD = "analyze_wildcard";
        private const string _AUTO_GENERATE_PHRASE_QUERIES = "auto_generate_phrase_queries";
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";
        private const string _IS_LENIENT = "lenient";
        private const string _USE_DIS_MAX = "use_dis_max";
        private const string _TIE_BREAKER = "tie_breaker";

        internal static OperatorEnum _DEFAULT_OPERATOR_DEFAULT = OperatorEnum.Or;
        internal const bool _ALLOW_LEADING_WILDCARD_DEFAULT = true;
        internal const bool _LOWERCASE_EXPANDED_TERMS_DEFAULT = true;
        internal const bool _ENABLE_POSITION_INCREMENTS_DEFAULT = true;
        internal const int _FUZZY_MAXIMUM_EXPANSIONS_DEFAULT = 25;
        internal const int _FUZZY_PREFIX_LENGTH_DEFAULT = 0;
        internal const int _PHRASE_SLOP_DEFAULT = 0;
        internal const bool _ANALYZE_WILDCARD_DEFAULT = false;
        internal const bool _AUTO_GENERATE_PHRASE_QUERIES_DEFAULT = false;
        internal static MinimumShouldMatchBase _MINIMUM_SHOULD_MATCH_DEFAULT = new IntegerMatch(1);
        internal const bool _IS_LENIENT_DEFAULT = false;
        
        // only used when multiple fields are done
        internal const bool _USE_DIS_MAX_DEFAULT = true;
        // used with dis max queries
        internal const int _TIE_BREAKER_DEFAULT = 0;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.QueryString.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            QueryStringQuery query = null;
            string queryStr = fieldDict.GetString(_QUERY);
            IEnumerable<string> fields = null;
            //string fields = null;
            if(fieldDict.ContainsKey(_DEFAULT_FIELD))
                query = new QueryStringQuery(fieldDict.GetString(_DEFAULT_FIELD), queryStr);
            else if(fieldDict.ContainsKey(_FIELDS))
                query = new QueryStringQuery(JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_FIELDS)), queryStr);
            else
                query = new QueryStringQuery(queryStr);

            query.AllowLeadingWildcard = fieldDict.GetBool(_ALLOW_LEADING_WILDCARD, _ALLOW_LEADING_WILDCARD_DEFAULT);
            query.Analyzer = fieldDict.GetStringOrDefault(_ANALYZER);
            query.AnalyzeWildcard = fieldDict.GetBool(_ANALYZE_WILDCARD, _ANALYZE_WILDCARD_DEFAULT);
            query.AutoGeneratePhraseQueries = fieldDict.GetBool(_AUTO_GENERATE_PHRASE_QUERIES, _AUTO_GENERATE_PHRASE_QUERIES_DEFAULT);
            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.DefaultOperator = OperatorEnum.Find(fieldDict.GetString(_DEFAULT_OPERATOR, _DEFAULT_OPERATOR_DEFAULT.ToString()));
            query.EnablePositionIncrements = fieldDict.GetBool(_ENABLE_POSITION_INCREMENTS, _ENABLE_POSITION_INCREMENTS_DEFAULT);
            query.Fuzziness = fieldDict.GetDoubleOrNull(_FUZZINESS);
            query.FuzzyMaximumExpansions = fieldDict.GetInt32(_FUZZY_MAXIMUM_EXPANSIONS, _FUZZY_MAXIMUM_EXPANSIONS_DEFAULT);
            query.FuzzyPrefixLength = fieldDict.GetInt32(_FUZZY_PREFIX_LENGTH, _FUZZY_PREFIX_LENGTH_DEFAULT);
            query.IsLenient = fieldDict.GetBool(_IS_LENIENT, _IS_LENIENT_DEFAULT);
            query.LowerCaseExpandedTerms = fieldDict.GetBool(_LOWERCASE_EXPANDED_TERMS, _LOWERCASE_EXPANDED_TERMS_DEFAULT);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            if (fieldDict.ContainsKey(_MINIMUM_SHOULD_MATCH))
                query.MinimumShouldMatch = MinimumShouldMatchBase.BuildMinimumShouldMatch(fieldDict.GetString(_MINIMUM_SHOULD_MATCH));
            else
                query.MinimumShouldMatch = _MINIMUM_SHOULD_MATCH_DEFAULT;

            query.PhraseSlop = fieldDict.GetInt32(_PHRASE_SLOP, _PHRASE_SLOP_DEFAULT);
            query.TieBreaker = fieldDict.GetInt32(_TIE_BREAKER, _TIE_BREAKER_DEFAULT);
            query.UseDisMax = fieldDict.GetBool(_USE_DIS_MAX, _USE_DIS_MAX_DEFAULT);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is QueryStringQuery))
                throw new SerializeTypeException<QueryStringQuery>();

            QueryStringQuery query = value as QueryStringQuery;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            // first i want to write the fields (if any)
            int fieldCount = query.NumberOfFields;
            if (fieldCount > 0)
            {
                if (fieldCount > 1)
                    fieldDict.Add(_FIELDS, query.Fields.Where(x => !string.IsNullOrWhiteSpace(x)));
                else
                    fieldDict.Add(_DEFAULT_FIELD, query.Fields.First(x => !string.IsNullOrWhiteSpace(x)));
            }

            // next i want to write the query
            fieldDict.Add(_QUERY, query.Query);

            //then i will write all the other stuff
            fieldDict.AddObject(_ALLOW_LEADING_WILDCARD, query.AllowLeadingWildcard, _ALLOW_LEADING_WILDCARD_DEFAULT);
            fieldDict.AddObject(_ANALYZE_WILDCARD, query.AnalyzeWildcard, _ANALYZE_WILDCARD_DEFAULT);
            fieldDict.AddObject(_ANALYZER, query.Analyzer);
            fieldDict.AddObject(_AUTO_GENERATE_PHRASE_QUERIES, query.AutoGeneratePhraseQueries, _AUTO_GENERATE_PHRASE_QUERIES_DEFAULT);
            fieldDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldDict.AddObject(_DEFAULT_OPERATOR, query.DefaultOperator, _DEFAULT_OPERATOR_DEFAULT);
            fieldDict.AddObject(_ENABLE_POSITION_INCREMENTS, query.EnablePositionIncrements, _ENABLE_POSITION_INCREMENTS_DEFAULT);
            fieldDict.AddObject(_FUZZINESS, query.Fuzziness);
            fieldDict.AddObject(_FUZZY_MAXIMUM_EXPANSIONS, query.FuzzyMaximumExpansions, _FUZZY_MAXIMUM_EXPANSIONS_DEFAULT);
            fieldDict.AddObject(_FUZZY_PREFIX_LENGTH, query.FuzzyPrefixLength, _FUZZY_PREFIX_LENGTH_DEFAULT);
            fieldDict.AddObject(_IS_LENIENT, query.IsLenient, _IS_LENIENT_DEFAULT);
            fieldDict.AddObject(_LOWERCASE_EXPANDED_TERMS, query.LowerCaseExpandedTerms, _LOWERCASE_EXPANDED_TERMS_DEFAULT);
            fieldDict.AddObject(_MINIMUM_SHOULD_MATCH, query.MinimumShouldMatch.GetValue(), _MINIMUM_SHOULD_MATCH_DEFAULT.GetValue());
            fieldDict.AddObject(_PHRASE_SLOP, query.PhraseSlop, _PHRASE_SLOP_DEFAULT);
            fieldDict.AddObject(_TIE_BREAKER, query.TieBreaker, _TIE_BREAKER_DEFAULT);
            fieldDict.AddObject(_USE_DIS_MAX, query.UseDisMax, _USE_DIS_MAX_DEFAULT);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.QueryString.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
