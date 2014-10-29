using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.SimpleQueryString
{
    public class SimpleQueryStringSerializer : JsonConverter
    {
        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>(){ " | ", " |", "| ", "|"};

        private const string _FIELDS = "fields";
        private const string _QUERY = "query";
        private const string _DEFAULT_OPERATOR = "default_operator";
        private const string _ANALYZER = "analyzer";
        private const string _FLAGS = "flags";

        internal static OperatorEnum _DEFAULT_OPERATOR_DEFAULT = OperatorEnum.Or;
        internal static List<ParsingFeatureEnum> _FLAGS_DEFAULT = new List<ParsingFeatureEnum>(){ParsingFeatureEnum.All};

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.SimpleQueryString.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SimpleQueryStringQuery query = null;
            string queryStr = fieldDict.GetString(_QUERY);
            if (fieldDict.ContainsKey(_FIELDS))
            {
                query = new SimpleQueryStringQuery(JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_FIELDS)), queryStr);
            }
            else 
            {
                query = new SimpleQueryStringQuery(queryStr);
            }

            query.Analyzer = fieldDict.GetStringOrDefault(_ANALYZER);
            query.DefaultOperator = OperatorEnum.Find(fieldDict.GetString(_DEFAULT_OPERATOR, _DEFAULT_OPERATOR_DEFAULT.ToString()));
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            if (fieldDict.ContainsKey(_FLAGS))
            {
                string flagsValue = fieldDict.GetString(_FLAGS);
                if(!string.IsNullOrWhiteSpace(flagsValue))
                {
                    ParsingFeatureEnum feature = ParsingFeatureEnum.All;
                    List<ParsingFeatureEnum> features = new List<ParsingFeatureEnum>();
                    foreach (string flagValue in flagsValue.Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        feature = ParsingFeatureEnum.Find(flagValue);
                        if (feature == null)
                            throw new Exception("Unable to match " + flagValue + " to a parsing feature.");

                        features.Add(feature);
                    }

                    if (features.Any())
                        query.ParsingFeatureFlags = features;
                }                
            }

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SimpleQueryStringQuery))
                throw new SerializeTypeException<SimpleQueryStringQuery>();

            SimpleQueryStringQuery query = value as SimpleQueryStringQuery;
            
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELDS, query.Fields);
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.AddObject(_ANALYZER, query.Analyzer);
            fieldDict.AddObject(_DEFAULT_OPERATOR, query.DefaultOperator.ToString(), _DEFAULT_OPERATOR_DEFAULT.ToString());
            if (query.ParsingFeatureFlags != null && query.ParsingFeatureFlags.Any(x => x != null))
            {
                string defaultValue = string.Join(_FLAG_DELIMITER, _FLAGS_DEFAULT.Select(x => x.ToString()));
                string flagsValue = string.Join(_FLAG_DELIMITER, query.ParsingFeatureFlags.Where(x => x != null).Select(x => x.ToString()));

                fieldDict.AddObject(_FLAGS, flagsValue, defaultValue);
            }

            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string,object>();
            queryDict.Add(QueryTypeEnum.SimpleQueryString.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
