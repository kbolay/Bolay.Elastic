using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Exists
{
    public class ExistsSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Exists.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            ExistsFilter filter = new ExistsFilter(fieldDict.First().Key, fieldDict.First().Value.ToString());
            filter.FilterName = fieldDict.GetStringOrDefault(FilterSerializer._FILTER_NAME);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ExistsFilter))
                throw new SerializeTypeException<ExistsFilter>();

            ExistsFilter filter = value as ExistsFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, filter.Value);
            fieldDict.AddObject(FilterSerializer._CACHE_KEY, filter.CacheKey);
            fieldDict.AddObject(FilterSerializer._FILTER_NAME, filter.FilterName);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.Exists.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
