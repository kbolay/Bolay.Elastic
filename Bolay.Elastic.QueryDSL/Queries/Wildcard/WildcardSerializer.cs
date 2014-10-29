using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Wildcard
{
    public class WildcardSerializer : JsonConverter
    {
        private const string _WILDCARD = "wildcard";
        private const string _VALUE = "value";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Wildcard.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            Dictionary<string, object> internalDict = null;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }
            catch
            {
                return new WildcardQuery(fieldDict.First().Key, fieldDict.First().Value.ToString());
            }

            string value = null;
            if(internalDict.ContainsKey(_WILDCARD))
                value = internalDict.GetString(_WILDCARD);
            else if(internalDict.ContainsKey(_VALUE))
                value = internalDict.GetString(_VALUE);
            else
                throw new RequiredPropertyMissingException(_WILDCARD + "/" + _VALUE);

            WildcardQuery query = new WildcardQuery(fieldDict.First().Key, value);
            query.Boost = internalDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.QueryName = internalDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is WildcardQuery))
                throw new SerializeTypeException<WildcardQuery>();

            WildcardQuery query = value as WildcardQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (query.Boost != QuerySerializer._BOOST_DEFAULT || string.IsNullOrWhiteSpace(query.QueryName))
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(_VALUE, query.Value);
                internalDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
                internalDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
                fieldDict.Add(query.Field, internalDict);
            }
            else
            {
                fieldDict.Add(query.Field, query.Value);
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Wildcard.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
