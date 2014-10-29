using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis
{
    public class FuzzyLikeThisSerializer : JsonConverter
    {
        private const string _FUZZY_LIKE_THIS = "fuzzy_like_this";
        private const string _FLT = "flt";
        private const string _FUZZY_LIKE_THIS_FIELD = "fuzzy_like_this_field";
        private const string _FLT_FIELD = "flt_field";
        private const string _FIELDS = "fields";
        private const string _LIKE_TEXT = "like_text";
        private const string _IGNORE_TERM_FREQUENCY = "ingore_tf";
        private const string _MAX_QUERY_TERMS = "max_query_terms";
        private const string _FUZZINESS = "fuzziness";
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _ANALYZER = "analyzer";

        internal const int _MAX_QUERY_TERMS_DEFAULT = 25;
        internal const Double _FUZZINESS_DEFAULT = 0.5;
        internal const bool _IGNORE_TERM_FREQUENCY_DEFAULT = false;
        internal const int _PREFIX_LENGTH_DEFAULT = 0;

        private List<string> _FuzzyLikeThisAliases = new List<string>() { _FUZZY_LIKE_THIS, _FLT };
        private List<string> _FuzzyLikeThisFieldAliases = new List<string>() { _FUZZY_LIKE_THIS_FIELD, _FLT_FIELD };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            QueryTypeEnum queryType = QueryTypeEnum.FuzzyLikeThis;
            queryType = QueryTypeEnum.Find(fieldDict.First().Key);

            FuzzyLikeThisBase query = null;

            if (queryType != null)
            {
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }

            if (fieldDict.ContainsKey(_LIKE_TEXT))
            {
                query = new FuzzyLikeThisQuery();
            }
            else
            {
                query = new FuzzyLikeThisFieldQuery();
                query.Fields = new List<string>() { fieldDict.First().Key };
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }

            if(fieldDict.ContainsKey(_FIELDS))
                query.Fields = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_FIELDS));

            query.Query = fieldDict.GetString(_LIKE_TEXT);
            query.Analyzer = fieldDict.GetStringOrDefault(_ANALYZER);
            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.Fuzziness = fieldDict.GetDouble(_FUZZINESS, _FUZZINESS_DEFAULT);
            query.IgnoreTermFrequency = fieldDict.GetBool(_IGNORE_TERM_FREQUENCY, _IGNORE_TERM_FREQUENCY_DEFAULT);
            query.MaximumQueryTerms = fieldDict.GetInt32(_MAX_QUERY_TERMS, _MAX_QUERY_TERMS_DEFAULT);
            query.PrefixLength = fieldDict.GetInt32OrDefault(_PREFIX_LENGTH);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FuzzyLikeThisBase))
                throw new SerializeTypeException<FuzzyLikeThisBase>();

            FuzzyLikeThisBase query = value as FuzzyLikeThisBase;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            if (query is FuzzyLikeThisQuery)
            {
                fieldDict.Add(_FIELDS, query.Fields);
                queryDict.Add(_FUZZY_LIKE_THIS, fieldDict);
            }
            else
            {
                Dictionary<string, object> singleFieldDict = new Dictionary<string, object>();
                string fieldName = query.Fields.First();
                singleFieldDict.Add(fieldName, fieldDict);
                queryDict.Add(_FUZZY_LIKE_THIS_FIELD, singleFieldDict);
            }

            fieldDict.Add(_LIKE_TEXT, query.Query);
            fieldDict.AddObject(_ANALYZER, query.Analyzer);
            fieldDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldDict.AddObject(_FUZZINESS, query.Fuzziness, _FUZZINESS_DEFAULT);
            fieldDict.AddObject(_IGNORE_TERM_FREQUENCY, query.IgnoreTermFrequency, _IGNORE_TERM_FREQUENCY_DEFAULT);
            fieldDict.AddObject(_MAX_QUERY_TERMS, query.MaximumQueryTerms, _MAX_QUERY_TERMS_DEFAULT);
            fieldDict.AddObject(_PREFIX_LENGTH, query.PrefixLength, _PREFIX_LENGTH_DEFAULT);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            serializer.Serialize(writer, queryDict);
        }
    }
}
