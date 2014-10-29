using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Terms
{
    public class TermsSerializer : JsonConverter
    {
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";

        internal static MinimumShouldMatchBase _MINIMUM_SHOULD_MATCH_DEFAULT = new IntegerMatch(1);

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Terms.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            if(!fieldDict.Any(x => !x.Key.Equals(_MINIMUM_SHOULD_MATCH)))
                throw new RequiredPropertyMissingException("field");

            KeyValuePair<string, object> fieldValuePair = fieldDict.First(x => !x.Key.Equals(_MINIMUM_SHOULD_MATCH));

            TermsQuery query = new TermsQuery(fieldValuePair.Key, JsonConvert.DeserializeObject<IEnumerable<object>>(fieldValuePair.Value.ToString()));

            if (fieldDict.ContainsKey(_MINIMUM_SHOULD_MATCH))
                query.MinimumShouldMatch = MinimumShouldMatchBase.BuildMinimumShouldMatch(fieldDict.GetString(_MINIMUM_SHOULD_MATCH));

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermsQuery))
                throw new SerializeTypeException<TermsQuery>();

            TermsQuery query = value as TermsQuery;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(query.Field, query.Values);
            fieldDict.AddObject(_MINIMUM_SHOULD_MATCH, query.MinimumShouldMatch.GetValue(), _MINIMUM_SHOULD_MATCH_DEFAULT.GetValue());
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Terms.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
