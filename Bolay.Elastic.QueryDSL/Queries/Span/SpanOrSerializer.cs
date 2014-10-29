using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanOrSerializer : JsonConverter
    {
        private const string _CLAUSES = "clauses";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.Or.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanOrQuery query = new SpanOrQuery(JsonConvert.DeserializeObject<IEnumerable<SpanQueryBase>>(fieldDict.GetString(_CLAUSES)));

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanOrQuery))
                throw new SerializeTypeException<SpanOrQuery>();

            SpanOrQuery query = value as SpanOrQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_CLAUSES, query.Clauses);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(SpanQueryTypeEnum.Or.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
