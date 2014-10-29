using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanFirstSerializer : JsonConverter
    {
        private const string _MATCH = "match";
        private const string _END = "end";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.First.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanFirstQuery query = new SpanFirstQuery(
                JsonConvert.DeserializeObject<SpanQueryBase>(fieldDict.GetString(_MATCH)),
                fieldDict.GetInt64(_END));

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanFirstQuery))
                throw new SerializeTypeException<SpanFirstQuery>();

            SpanFirstQuery query = value as SpanFirstQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_MATCH, query.Match);
            fieldDict.Add(_END, query.End);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(SpanQueryTypeEnum.First.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
