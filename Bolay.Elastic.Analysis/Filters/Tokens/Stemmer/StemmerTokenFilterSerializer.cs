using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Stemmer
{
    internal class StemmerTokenFilterSerializer : JsonConverter
    {
        private const string _LANGUAGE = "language";
        private const string _NAME = "name";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            StemmerLanguageEnum language = StemmerLanguageEnum.MinimalEnglish;
            if(fieldDict.ContainsKey(_LANGUAGE))
            {
                language = StemmerLanguageEnum.Find(fieldDict.GetString(_LANGUAGE));
            }
            else if(fieldDict.ContainsKey(_NAME))
            {
                language = StemmerLanguageEnum.Find(fieldDict.GetString(_NAME));
            }
            else
            {
                throw new RequiredPropertyMissingException(_LANGUAGE + "/" + _NAME);
            }            

            StemmerTokenFilter filter = new StemmerTokenFilter(filterDict.First().Key, language);
            TokenFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StemmerTokenFilter))
                throw new SerializeTypeException<StemmerTokenFilter>();

            StemmerTokenFilter filter = value as StemmerTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_LANGUAGE, filter.Language.ToString());

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
