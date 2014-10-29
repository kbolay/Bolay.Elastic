using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Snowball
{
    internal class SnowballTokenFilterSerializer : JsonConverter
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

            SnowballLanguageEnum language = SnowballLanguageEnum.Finnish;
            language = SnowballLanguageEnum.Find(fieldDict.GetString(_LANGUAGE));

            SnowballTokenFilter filter = new SnowballTokenFilter(filterDict.First().Key, language);
            TokenFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SnowballTokenFilter))
                throw new SerializeTypeException<SnowballTokenFilter>();

            SnowballTokenFilter filter = value as SnowballTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_LANGUAGE, filter.Language.ToString());

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
