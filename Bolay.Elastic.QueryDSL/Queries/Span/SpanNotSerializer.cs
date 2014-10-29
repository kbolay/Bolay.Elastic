using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanNotSerializer : JsonConverter
    {
        private const string _INCLUDE = "include";
        private const string _EXCLUDE = "exclude";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SpanQueryTypeEnum.Not.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            SpanNotQuery query = new SpanNotQuery(
                JsonConvert.DeserializeObject<SpanQueryBase>(fieldDict.GetString(_INCLUDE)),
                JsonConvert.DeserializeObject<SpanQueryBase>(fieldDict.GetString(_EXCLUDE)));

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SpanNotQuery))
                throw new SerializeTypeException<SpanNotQuery>();

            SpanNotQuery query = value as SpanNotQuery;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            fieldDict.Add(_INCLUDE, query.Include);
            fieldDict.Add(_EXCLUDE, query.Exclude);
            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(SpanQueryTypeEnum.Not.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
