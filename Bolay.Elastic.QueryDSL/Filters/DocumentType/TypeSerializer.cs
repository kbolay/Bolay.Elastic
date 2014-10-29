using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.DocumentType
{
    public class TypeSerializer : JsonConverter
    {
        private const string _TYPE = "value";

        internal const bool _CACHE_DEFAULT = false;

        public override bool CanConvert(System.Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Type.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            TypeFilter filter = new TypeFilter(fieldDict.GetString(_TYPE));
            filter.FilterName = fieldDict.GetStringOrDefault(FilterSerializer._FILTER_NAME);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TypeFilter))
                throw new SerializeTypeException<TypeFilter>();

            TypeFilter filter = value as TypeFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_TYPE, filter.DocumentType);
            fieldDict.AddObject(FilterSerializer._FILTER_NAME, filter.FilterName);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Type.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
