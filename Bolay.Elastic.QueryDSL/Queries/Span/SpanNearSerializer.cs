using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanNearSerializer : JsonConverter
    {
        private const string _CLAUSES = "clauses";
        private const string _SLOP = "slop";
        private const string _IN_ORDER = "in_order";
        private const string _COLLECT_PAYLOADS = "collect_payloads";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.Near.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanNearQuery query = new SpanNearQuery(JsonConvert.DeserializeObject<IEnumerable<SpanQueryBase>>(fieldDict.GetString(_CLAUSES)));

            query.Slop = fieldDict.GetInt32OrNull(_SLOP);
            query.RequireMatchesInOrder = fieldDict.GetBoolOrDefault(_IN_ORDER);
            query.CollectPayloads = fieldDict.GetBoolOrDefault(_COLLECT_PAYLOADS);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanNearQuery))
                throw new SerializeTypeException<SpanNearQuery>();

            SpanNearQuery query = value as SpanNearQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_CLAUSES, query.Clauses);
            fieldDict.AddObject(_SLOP, query.Slop);
            fieldDict.Add(_IN_ORDER, query.RequireMatchesInOrder);
            fieldDict.Add(_COLLECT_PAYLOADS, query.CollectPayloads);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(SpanQueryTypeEnum.Near.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}