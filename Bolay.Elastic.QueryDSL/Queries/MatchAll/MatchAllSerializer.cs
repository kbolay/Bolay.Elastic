using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MatchAll
{
    public class MatchAllSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.MatchAll.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            MatchAllQuery query = new MatchAllQuery();
            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MatchAllQuery))
                throw new SerializeTypeException<MatchAllQuery>();

            MatchAllQuery query = value as MatchAllQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string,object>();
            fieldDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.MatchAll.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
