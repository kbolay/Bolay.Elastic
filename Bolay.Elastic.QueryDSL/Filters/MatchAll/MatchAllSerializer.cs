using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.MatchAll
{
    public class MatchAllSerializer : JsonConverter
    {
        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.MatchAll.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            MatchAllFilter filter = new MatchAllFilter();
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MatchAllFilter))
                throw new SerializeTypeException<MatchAllFilter>();

            MatchAllFilter filter = value as MatchAllFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.MatchAll.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
