using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MoreLikeThis
{
    public class MoreLikeThisSerializer : JsonConverter
    {
        private const string _FIELD = "field";
        private const string _FIELDS = "fields";
        private const string _LIKE_TEXT = "like_text";
        private const string _PERCENTAGE_TERMS_TO_MATCH = "percent_terms_to_match";
        private const string _MINIMUM_TERM_FREQUENCY = "min_term_freq";
        private const string _MAXIMUM_QUERY_TERMS = "max_query_terms";
        private const string _STOP_WORDS = "stop_words";
        private const string _MINIMUM_DOCUMENT_FREQUENCY = "min_doc_freq";
        private const string _MAXIMUM_DOCUMENT_FREQUENCY = "max_doc_freq";
        private const string _MINIMUM_WORD_LENGTH = "min_word_length";
        private const string _MAXIMUM_WORD_LENGTH = "max_word_length";
        private const string _BOOST_TERMS = "boost_terms";
        private const string _ANALYZER = "analyzer";

        internal const Double _PERCENTAGE_TERMS_TO_MATCH_DEFAULT = 0.3;
        internal const int _MINIMUM_TERM_FREQUENCY_DEFAULT = 2;
        internal const int _MAXIMUM_QUERY_TERMS_DEFAULT = 25;
        internal const int _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT = 5;
        internal const int _MINIMUM_WORD_LENGTH_DEFAULT = 0;
        internal const int _MAXIMUM_WORD_LENGTH_DEFAULT = 0;
        internal const int _BOOST_TERMS_DEFAULT = 1;

        private List<string> _MoreLikeThisAliases = new List<string>() { QueryTypeEnum.MoreLikeThis.ToString(), QueryTypeEnum.Mlt.ToString() };
        private List<string> _MoreLikeThisFieldAliases = new List<string>() { QueryTypeEnum.MoreLikeThisField.ToString(), QueryTypeEnum.MltField.ToString() };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            QueryTypeEnum queryType = QueryTypeEnum.FuzzyLikeThis;
            queryType = QueryTypeEnum.Find(fieldDict.First().Key);

            MoreLikeThisBase query = null;

            if (queryType != null)
            {
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }

            if (fieldDict.ContainsKey(_LIKE_TEXT))
            {
                query = new MoreLikeThisQuery();
            }
            else
            {
                query = new MoreLikeThisFieldQuery();
                query.Fields = new List<string>() { fieldDict.First().Key };
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }

            query.Query = fieldDict.GetString(_LIKE_TEXT);
            query.PercentageTermsToMatch = fieldDict.GetDouble(_PERCENTAGE_TERMS_TO_MATCH, _PERCENTAGE_TERMS_TO_MATCH_DEFAULT);
            query.MinimumTermFrequency = fieldDict.GetInt32(_MINIMUM_TERM_FREQUENCY, _MINIMUM_TERM_FREQUENCY_DEFAULT);
            query.MaximumQueryTerms = fieldDict.GetInt32(_MAXIMUM_QUERY_TERMS, _MAXIMUM_QUERY_TERMS_DEFAULT);
            query.Analyzer = fieldDict.GetStringOrDefault(_ANALYZER);
            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.BoostTerms = fieldDict.GetInt32(_BOOST_TERMS, _BOOST_TERMS_DEFAULT);
            query.MaximumDocumentFrequency = fieldDict.GetInt32OrNull(_MAXIMUM_DOCUMENT_FREQUENCY);
            query.MaximumWordLength = fieldDict.GetInt32(_MAXIMUM_WORD_LENGTH, _MAXIMUM_WORD_LENGTH_DEFAULT);
            query.MinimumDocumentFrequency = fieldDict.GetInt32(_MINIMUM_DOCUMENT_FREQUENCY, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            query.MinimumWordLength = fieldDict.GetInt32(_MINIMUM_WORD_LENGTH, _MINIMUM_WORD_LENGTH_DEFAULT);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            if (fieldDict.ContainsKey(_FIELDS))
                query.Fields = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict[_FIELDS].ToString());

            if(fieldDict.ContainsKey(_STOP_WORDS))
                query.StopWords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict[_STOP_WORDS].ToString());

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MoreLikeThisBase))
                throw new SerializeTypeException<MoreLikeThisBase>();

            MoreLikeThisBase query = value as MoreLikeThisBase;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if (query is MoreLikeThisQuery)
                fieldDict.AddObject(_FIELDS, query.Fields);

            fieldDict.AddObject(_LIKE_TEXT, query.Query);
            fieldDict.AddObject(_PERCENTAGE_TERMS_TO_MATCH, query.PercentageTermsToMatch, _PERCENTAGE_TERMS_TO_MATCH_DEFAULT);
            fieldDict.AddObject(_MINIMUM_TERM_FREQUENCY, query.MinimumTermFrequency, _MINIMUM_TERM_FREQUENCY_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_QUERY_TERMS, query.MaximumQueryTerms, _MAXIMUM_QUERY_TERMS_DEFAULT);
            fieldDict.AddObject(_ANALYZER, query.Analyzer);
            fieldDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldDict.AddObject(_BOOST_TERMS, query.BoostTerms, _BOOST_TERMS_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_DOCUMENT_FREQUENCY, query.MaximumDocumentFrequency);
            fieldDict.AddObject(_MAXIMUM_WORD_LENGTH, query.MaximumWordLength, _MAXIMUM_WORD_LENGTH_DEFAULT);
            fieldDict.AddObject(_MINIMUM_DOCUMENT_FREQUENCY, query.MinimumDocumentFrequency, _MINIMUM_DOCUMENT_FREQUENCY_DEFAULT);
            fieldDict.AddObject(_MINIMUM_WORD_LENGTH, query.MinimumWordLength, _MINIMUM_WORD_LENGTH_DEFAULT);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            if (query is MoreLikeThisFieldQuery)
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(query.Fields.First(), fieldDict);
                queryDict.Add(QueryTypeEnum.MoreLikeThisField.ToString(), internalDict);
            }
            else
            {
                queryDict.Add(QueryTypeEnum.MoreLikeThis.ToString(), fieldDict);
            }

            serializer.Serialize(writer, queryDict);
        }
    }
}
