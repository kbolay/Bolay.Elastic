using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Term
{
    public class TermSerializer : JsonConverter
    {
        private const string _TERM = "term";
        private const string _VALUE = "value";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            if (fieldDict.First().Key.Equals(QueryTypeEnum.Term.ToString(), StringComparison.OrdinalIgnoreCase))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            Dictionary<string, object> internalDict = null;
            try
            {
                internalDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());
            }
            catch
            {
                return new TermQuery(fieldDict.First().Key, fieldDict.First().Value);
            }

            object value = null;
            if (internalDict.ContainsKey(_VALUE))
                value = internalDict[_VALUE];
            else if (internalDict.ContainsKey(_TERM))
                value = internalDict[_TERM];
            else
                throw new RequiredPropertyMissingException(_VALUE + "/" + _TERM);

            TermQuery query = new TermQuery(fieldDict.First().Key, value);
            query.Boost = internalDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.QueryName = internalDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermQuery))
                throw new SerializeTypeException<TermQuery>();

            TermQuery query = value as TermQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            if (query.Boost != QuerySerializer._BOOST_DEFAULT)
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
            queryDict.Add(QueryTypeEnum.Term.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
