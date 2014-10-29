using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Lowercase
{
    internal class LowercaseTokenFilterSerializer : JsonConverter
    {
        private const string _LANGUAGE = "language";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            LowercaseTokenFilter filter = new LowercaseTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            LowercaseSupportedLanguageEnum lowercaseLanguage = LowercaseSupportedLanguageEnum.Greek;
            filter.Language = LowercaseSupportedLanguageEnum.Find(fieldDict.GetString(_LANGUAGE));

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LowercaseTokenFilter))
                throw new SerializeTypeException<LowercaseTokenFilter>();

            LowercaseTokenFilter filter = value as LowercaseTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            if (filter.Language != null)
                fieldDict.AddObject(_LANGUAGE, filter.Language.ToString());

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
