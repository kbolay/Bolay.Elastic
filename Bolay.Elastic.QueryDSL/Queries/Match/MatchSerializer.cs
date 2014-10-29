using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Queries;
using Bolay.Elastic.QueryDSL.Queries.Match;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    public class MatchSerializer : JsonConverter
    {
        private const string _FIELDS = "fields";
        private const string _QUERY = "query";
        private const string _OPERATOR = "operator";
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";
        private const string _ANALYZER = "analyzer";
        private const string _FUZZINESS = "fuzziness";
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _MAX_EXPANSIONS = "max_expansions";
        private const string _REWRITE = "rewrite";
        private const string _ZERO_TERMS_QUERY = "zero_terms_query";
        private const string _CUTOFF_FREQUENCY = "cutoff_frequency";
        private const string _LENIENT = "lenient";
        private const string _USE_DIS_MAX = "use_dis_max";
        private const string _TIE_BREAKER = "tie_breaker";        

        internal static OperatorEnum _OPERATOR_DEFAULT = OperatorEnum.Or;
        internal const Double _FUZZINESS_DEFAULT = default(Double);
        internal const int _PREFIX_LENGTH_DEFAULT = default(int);
        internal static ZeroTermsEnum _ZERO_TERMS_QUERY_DEFAULT = ZeroTermsEnum.None;
        internal const double _CUTOFF_FREQUENCY_DEFAULT = default(Double);
        internal const bool _LENIENT_DEFAULT = default(bool);
        internal const bool _USE_DIS_MAX_DEFAULT = false;
        internal const Double _TIE_BREAKER_DEFAULT = default(Double);
        internal const int _MINIMUM_SHOULD_MATCH_DEFAULT = default(int);
        internal static int? _MAXIMUM_EXPANSIONS_DEFAULT = null;

        private List<string> _FieldKeys = new List<string>()
        {
            _FIELDS,
            _QUERY,
            _OPERATOR,
            _MINIMUM_SHOULD_MATCH,
            _ANALYZER,
            _FUZZINESS,
            _PREFIX_LENGTH,
            _MAX_EXPANSIONS,
            _REWRITE,
            _ZERO_TERMS_QUERY,
            _CUTOFF_FREQUENCY,
            _LENIENT
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> dict = serializer.Deserialize<Dictionary<string, object>>(reader);

            MatchQueryTypeEnum matchType = MatchQueryTypeEnum.Boolean;
            matchType = MatchQueryTypeEnum.Find(dict.First().Key);

            if (matchType != null)
                dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(dict.First().Value.ToString());
            else if (dict.ContainsKey(_FIELDS))
                matchType = MatchQueryTypeEnum.Multi;

            MatchQueryBase query = null;
            if (matchType == MatchQueryTypeEnum.Multi)
            {
                MultiMatchQuery multiMatchQuery = new MultiMatchQuery();
                if (dict.ContainsKey(_FIELDS))
                    multiMatchQuery.Fields = JsonConvert.DeserializeObject<List<string>>(dict[_FIELDS].ToString());
                if (dict.ContainsKey(_QUERY))
                    multiMatchQuery.Query = dict[_QUERY].ToString();
                if (dict.ContainsKey(_USE_DIS_MAX))
                    multiMatchQuery.UseDisMaxQuery = Convert.ToBoolean(dict[_USE_DIS_MAX]);
                else
                    multiMatchQuery.UseDisMaxQuery = _USE_DIS_MAX_DEFAULT;
                if (dict.ContainsKey(_TIE_BREAKER))
                    multiMatchQuery.TieBreakerMultiplier = Convert.ToDouble(dict[_TIE_BREAKER]);
                else
                    multiMatchQuery.TieBreakerMultiplier = _TIE_BREAKER_DEFAULT;

                ReadFieldsDict(dict, multiMatchQuery);

                query = multiMatchQuery;
            }
            else
            {
                QueryTypeEnum queryType = QueryTypeEnum.Match;
                queryType = QueryTypeEnum.Find(matchType.ToString());
                KeyValuePair<string, object> kvp = dict.FirstOrDefault(x => !_FieldKeys.Contains(x.Key));

                query = GenerateMatchQuery(queryType);
                if (!string.IsNullOrWhiteSpace(kvp.Key))
                {
                    query.Field = kvp.Key;
                    query.Query = kvp.Value.ToString();
                }

                ReadFieldsDict(dict, query);
            }

            if (query == null)
                throw new Exception("Unable to deserialize match query.");            

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MatchQueryBase))
                throw new SerializeTypeException<MatchQueryBase>();

            Dictionary<string, object> matchDict = new Dictionary<string, object>();
            if (!(value is MultiMatchQuery))
            {
                MatchQueryBase matchQuery = value as MatchQueryBase;
                Dictionary<string, object> fieldDict = CreateFieldsDict(matchQuery);
                fieldDict.Add(matchQuery.Field, matchQuery.Query);
                matchDict.Add(matchQuery.Type, fieldDict);
            }
            else
            {
                MultiMatchQuery multiMatch = value as MultiMatchQuery;

                Dictionary<string, object> fieldDict = CreateFieldsDict(multiMatch);
                fieldDict.Add(_FIELDS, multiMatch.Fields);
                fieldDict.Add(_QUERY, multiMatch.Query);

                if (multiMatch.UseDisMaxQuery != _USE_DIS_MAX_DEFAULT)
                    fieldDict.Add(_USE_DIS_MAX, multiMatch.UseDisMaxQuery);
                if (multiMatch.TieBreakerMultiplier != _TIE_BREAKER_DEFAULT)
                    fieldDict.Add(_TIE_BREAKER, multiMatch.TieBreakerMultiplier);

                matchDict.Add(multiMatch.Type, fieldDict);
            }

            serializer.Serialize(writer, matchDict);
        }

        private Dictionary<string, object> CreateFieldsDict(MatchQueryBase query)
        {
            Dictionary<string, object> fieldsDict = new Dictionary<string, object>();

            fieldsDict.AddObject(_ANALYZER, query.Analyzer);
            fieldsDict.AddObject(_CUTOFF_FREQUENCY, query.CutOffFrequency, _CUTOFF_FREQUENCY_DEFAULT);
            fieldsDict.AddObject(_FUZZINESS, query.Fuzziness, _FUZZINESS_DEFAULT);
            fieldsDict.AddObject(_MAX_EXPANSIONS, query.MaximumExpansions);
            fieldsDict.AddObject(_REWRITE, query.RewriteMethod == null ? null : query.RewriteMethod.ToString(), null);
            fieldsDict.AddObject(_LENIENT, query.IsLenient, _LENIENT_DEFAULT);
            fieldsDict.AddObject(_MINIMUM_SHOULD_MATCH, query.MinimumShouldMatch, _MINIMUM_SHOULD_MATCH_DEFAULT);
            fieldsDict.AddObject(_OPERATOR, query.Operator.ToString(), _OPERATOR_DEFAULT.ToString());
            fieldsDict.AddObject(_PREFIX_LENGTH, query.PrefixLength, _PREFIX_LENGTH_DEFAULT);
            fieldsDict.AddObject(_ZERO_TERMS_QUERY, query.ZeroTerm.ToString(), _ZERO_TERMS_QUERY_DEFAULT.ToString());
            fieldsDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            return fieldsDict;
        }
        private void ReadFieldsDict(Dictionary<string, object> fieldsDict, MatchQueryBase matchQuery)
        {
            matchQuery.Analyzer = fieldsDict.GetStringOrDefault(_ANALYZER);
            matchQuery.CutOffFrequency = fieldsDict.GetDouble(_CUTOFF_FREQUENCY, _CUTOFF_FREQUENCY_DEFAULT);
            matchQuery.Fuzziness = fieldsDict.GetDouble(_FUZZINESS, _FUZZINESS_DEFAULT);
            matchQuery.IsLenient = fieldsDict.GetBool(_LENIENT, _LENIENT_DEFAULT);
            matchQuery.MaximumExpansions = fieldsDict.GetInt32OrNull(_MAX_EXPANSIONS);
            matchQuery.MinimumShouldMatch = fieldsDict.GetInt32(_MINIMUM_SHOULD_MATCH, _MINIMUM_SHOULD_MATCH_DEFAULT);
            matchQuery.Operator = OperatorEnum.Find(fieldsDict.GetString(_OPERATOR, _OPERATOR_DEFAULT.ToString()));
            matchQuery.PrefixLength = fieldsDict.GetInt32(_PREFIX_LENGTH, _PREFIX_LENGTH_DEFAULT);
            matchQuery.RewriteMethod = RewriteMethodsEnum.Find(fieldsDict.GetString(_REWRITE, "not a real object"));
            matchQuery.ZeroTerm = ZeroTermsEnum.Find(fieldsDict.GetString(_ZERO_TERMS_QUERY, _ZERO_TERMS_QUERY_DEFAULT.ToString()));
            matchQuery.QueryName = fieldsDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
        }
        private MatchQueryBase GenerateMatchQuery(QueryTypeEnum type)
        {
            if (type.ImplementationType == typeof(MatchQuery))
                return new MatchQuery();
            else if (type.ImplementationType == typeof(MatchPhraseQuery))
                return new MatchPhraseQuery();
            else if (type.ImplementationType == typeof(MatchPhrasePrefixQuery))
                return new MatchPhrasePrefixQuery();
            else
                throw new Exception("Don't know what type of match query to create.");
        }
    }
}
