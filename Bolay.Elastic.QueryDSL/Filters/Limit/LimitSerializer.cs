using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Limit
{
    public class LimitSerializer : JsonConverter
    {
        private const string _LIMIT = "value";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Limit.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            LimitFilter filter = new LimitFilter(fieldDict.GetInt64(_LIMIT));
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LimitFilter))
                throw new SerializeTypeException<LimitFilter>();

            LimitFilter filter = value as LimitFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_LIMIT, filter.Size);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Limit.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
