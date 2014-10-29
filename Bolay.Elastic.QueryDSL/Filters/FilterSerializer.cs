using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters
{
    public class FilterSerializer : JsonConverter
    {
        internal const string _CACHE = "_cache";
        internal const string _CACHE_KEY = "_cache_key";
        internal const string _FILTER_NAME = "_name";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (filterDict == null || !filterDict.Any() || filterDict.Count != 1)
                return null;

            FilterTypeEnum filterType = FilterTypeEnum.Term;
            filterType = FilterTypeEnum.Find(filterDict.First().Key);
            if (filterType == null)
                throw new Exception("filterType not found.");

            object filter = JsonConvert.DeserializeObject(filterDict.First().Value.ToString(), filterType.ImplementationType);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        internal static void DeserializeBaseValues(IFilter filter, bool cacheDefault, Dictionary<string, object> fieldDict)
        {
            filter.Cache = fieldDict.GetBool(_CACHE, cacheDefault);
            filter.CacheKey = fieldDict.GetStringOrDefault(_CACHE_KEY);
            filter.FilterName = fieldDict.GetStringOrDefault(_FILTER_NAME);
        }

        internal static void SerializeBaseValues(IFilter filter, bool cacheDefault, Dictionary<string, object> fieldDict)
        {
            fieldDict.AddObject(_CACHE, filter.Cache, cacheDefault);
            fieldDict.AddObject(_CACHE_KEY, filter.CacheKey);
            fieldDict.AddObject(_FILTER_NAME, filter.FilterName);
        }
    }
}
