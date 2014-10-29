using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Regex;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Regex
{
    public class RegexSerializer : JsonConverter
    {
        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>(){ " | ", " |", "| ", "|"};

        private const string _PATTERN = "value";
        private const string _FLAGS = "flags";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Regex.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            Dictionary<string, object> internalDict = null;
            string field = null;
            string pattern = null;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }
            catch
            {
                internalDict = fieldDict;
            }

            RegexQuery query = null;

            if (internalDict.ContainsKey(_PATTERN))
            {
                field = fieldDict.First().Key;
                pattern = internalDict.GetString(_PATTERN);
                query = new RegexQuery(field, pattern);

                query.Boost = internalDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
                query.QueryName = internalDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
                if (internalDict.ContainsKey(_FLAGS))
                {
                    string flagsValue = internalDict.GetString(_FLAGS);
                    if (!string.IsNullOrWhiteSpace(flagsValue))
                    {
                        RegexOperatorEnum feature = RegexOperatorEnum.All;
                        List<RegexOperatorEnum> features = new List<RegexOperatorEnum>();
                        foreach (string flagValue in flagsValue.Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                        {
                            feature = RegexOperatorEnum.Find(flagValue);
                            if (feature == null)
                                throw new Exception("Unable to match " + flagValue + " to a regex operator.");

                            features.Add(feature);
                        }

                        if (features.Any())
                            query.RegexOperatorFlags = features;
                    }
                }
            }
            else
            {
                field = internalDict.First().Key;
                pattern = internalDict.First().Value.ToString();
                query = new RegexQuery(field, pattern);
            }

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is RegexQuery))
                throw new SerializeTypeException<RegexQuery>();

            RegexQuery query = value as RegexQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (query.Boost != QuerySerializer._BOOST_DEFAULT || !string.IsNullOrWhiteSpace(query.QueryName) || (query.RegexOperatorFlags != null && query.RegexOperatorFlags.Any(x => x != null)))
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(_PATTERN, query.Pattern);
                internalDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
                internalDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

                if (query.RegexOperatorFlags != null && query.RegexOperatorFlags.Any(x => x != null))
                {
                    string flagsValue = string.Join(_FLAG_DELIMITER, query.RegexOperatorFlags.Where(x => x != null).Select(x => x.ToString()));

                    internalDict.Add(_FLAGS, flagsValue);
                }

                fieldDict.Add(query.Field, internalDict);
            }
            else
            {
                fieldDict.Add(query.Field, query.Pattern);
            }            

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Regex.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
