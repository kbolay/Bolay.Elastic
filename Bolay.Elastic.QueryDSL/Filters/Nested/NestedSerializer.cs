using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Nested
{
    public class NestedSerializer : JsonConverter
    {
        private const string _PATH = "path";
        private const string _FILTER = "filter";
        private const string _JOIN = "join";
        private const string _CACHE_NAME = "_name";

        internal const bool _CACHE_DEFAULT = false;
        internal const bool _JOIN_DEFAULT = true;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Nested.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            string path = fieldDict.GetString(_PATH);
            IFilter query = JsonConvert.DeserializeObject<IFilter>(fieldDict.GetString(_FILTER));

            NestedFilter nestedFilter = new NestedFilter(path, query);
            nestedFilter.Join = fieldDict.GetBool(_JOIN, _JOIN_DEFAULT);
            nestedFilter.CacheName = fieldDict.GetStringOrDefault(_CACHE_NAME);

            FilterSerializer.DeserializeBaseValues(nestedFilter, _CACHE_DEFAULT, fieldDict);
            return nestedFilter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NestedFilter))
                throw new SerializeTypeException<NestedFilter>();

            NestedFilter filter = value as NestedFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_PATH, filter.Path);
            fieldDict.Add(_FILTER, filter.Filter);
            fieldDict.AddObject(_JOIN, filter.Join, _JOIN_DEFAULT);

            if (filter.Cache && !string.IsNullOrWhiteSpace(filter.CacheName))
                fieldDict.Add(_CACHE_NAME, filter.CacheName);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);            

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(FilterTypeEnum.Nested.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
