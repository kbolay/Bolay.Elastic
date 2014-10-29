using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanMultiTermSerializer : JsonConverter
    {
        private const string _MATCH = "match";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.MultiTerm.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanMultiTermQuery query = new SpanMultiTermQuery(JsonConvert.DeserializeObject<IQuery>(fieldDict.GetString(_MATCH)));
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanMultiTermQuery))
                throw new SerializeTypeException<SpanMultiTermQuery>();

            SpanMultiTermQuery query = value as SpanMultiTermQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_MATCH, query.Match);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(SpanQueryTypeEnum.MultiTerm.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
