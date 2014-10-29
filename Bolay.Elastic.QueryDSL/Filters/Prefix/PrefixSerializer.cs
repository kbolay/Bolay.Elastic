using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Prefix
{
    public class PrefixSerializer : JsonConverter
    {
        internal const bool _CACHE_DEFAULT = true;

        private static List<string> _KnownFields = new List<string>()
        {
            FilterSerializer._CACHE,
            FilterSerializer._CACHE_KEY
        };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(FilterTypeEnum.Prefix.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.First(x => !_KnownFields.Contains(x.Key));
            if (string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("field");

            PrefixFilter filter = new PrefixFilter(fieldKvp.Key, fieldKvp.Value.ToString());
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PrefixFilter))
                throw new SerializeTypeException<PrefixFilter>();

            PrefixFilter filter = value as PrefixFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, filter.Value);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(FilterTypeEnum.Prefix.ToString(), fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
