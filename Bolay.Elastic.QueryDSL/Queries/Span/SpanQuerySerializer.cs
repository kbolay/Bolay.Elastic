using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public class SpanQuerySerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> queryDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (queryDict == null || !queryDict.Any() || queryDict.Count != 1)
                return null;

            SpanQueryTypeEnum spanType = SpanQueryTypeEnum.Term;
            spanType = SpanQueryTypeEnum.Find(queryDict.First().Key);
            if (spanType == null)
                throw new Exception("Span Query Type not found.");

            object spanQuery = JsonConvert.DeserializeObject(queryDict.First().Value.ToString(), spanType.ImplementationType);

            return spanQuery;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
