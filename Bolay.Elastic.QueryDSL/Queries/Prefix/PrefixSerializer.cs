using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Prefix
{
    public class PrefixSerializer : JsonConverter
    {
        private const string _VALUE = "value";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Prefix.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            Dictionary<string, object> internalDict = null;
            string field = null;
            string value = null;
            double boost = QuerySerializer._BOOST_DEFAULT;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }
            catch 
            {
                internalDict = fieldDict;
            }

            if (internalDict.ContainsKey(_VALUE))
            {
                field = fieldDict.First().Key;
                value = internalDict.GetString(_VALUE);
                boost = internalDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            }               
            else
            {
                field = internalDict.First().Key;
                value = internalDict.First().Value.ToString();
            }

            PrefixQuery query = new PrefixQuery(field, value) { Boost = boost };
            query.QueryName = internalDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PrefixQuery))
                throw new SerializeTypeException<PrefixQuery>();

            PrefixQuery query = value as PrefixQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (query.Boost == QuerySerializer._BOOST_DEFAULT && string.IsNullOrWhiteSpace(query.QueryName))
            {
                fieldDict.Add(query.Field, query.Value);
            }
            else
            {
                Dictionary<string, object> internalDict = new Dictionary<string, object>();
                internalDict.Add(_VALUE, query.Value);
                internalDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
                internalDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
                fieldDict.Add(query.Field, internalDict);
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Prefix.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
