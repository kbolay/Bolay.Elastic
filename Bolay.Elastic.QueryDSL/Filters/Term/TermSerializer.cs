using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Term
{
    public class TermSerializer : JsonConverter
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

            if (fieldDict.First().Key.Equals(FilterTypeEnum.Term.ToString(), StringComparison.OrdinalIgnoreCase))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            KeyValuePair<string, object> fieldKvp = fieldDict.FirstOrDefault(x => !_KnownFields.Contains(x.Key, StringComparer.OrdinalIgnoreCase));

            if(string.IsNullOrWhiteSpace(fieldKvp.Key))
                throw new RequiredPropertyMissingException("field");

            TermFilter filter = new TermFilter(fieldKvp.Key, fieldKvp.Value);
            FilterSerializer.DeserializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);
            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermFilter))
                throw new SerializeTypeException<TermFilter>();

            TermFilter filter = value as TermFilter;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(filter.Field, filter.Value);
            FilterSerializer.SerializeBaseValues(filter, _CACHE_DEFAULT, fieldDict);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("term", fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}
