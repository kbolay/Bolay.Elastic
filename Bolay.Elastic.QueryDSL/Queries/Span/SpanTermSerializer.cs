using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanTermSerializer : JsonConverter
    {
        private const string _VALUE = "value";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.Term.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanTermQuery query = null;
            Dictionary<string, object> internalDict = null;
            string field = null;
            string value = null;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
                field = fieldDict.First().Key;
            }
            catch
            {
                internalDict = fieldDict;
            }

            if (!string.IsNullOrWhiteSpace(field))
            {
                query = new SpanTermQuery(field, internalDict.GetString(_VALUE));
                query.Boost = internalDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
                query.QueryName = internalDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            }
            else
            {
                query = new SpanTermQuery(internalDict.First().Key, internalDict.First().Value.ToString());
            }

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanTermQuery))
                throw new SerializeTypeException<SpanTermQuery>();

            SpanTermQuery query = value as SpanTermQuery;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();


            if (query.Boost != QuerySerializer._BOOST_DEFAULT || !string.IsNullOrWhiteSpace(query.QueryName))
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
            queryDict.Add(SpanQueryTypeEnum.Term.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
