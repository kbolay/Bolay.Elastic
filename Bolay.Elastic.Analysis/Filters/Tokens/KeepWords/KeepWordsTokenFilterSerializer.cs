using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeepWords
{
    internal class KeepWordsTokenFilterSerializer : JsonConverter
    {
        private const string _KEEP_WORDS = "keep_words";
        private const string _KEEP_WORDS_PATH = "keep_words_path";
        private const string _KEEP_WORDS_CASE = "keep_words_case";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());
            string filterName = filterDict.First().Key;
            
            KeepWordsTokenFilter filter = null;

            if (fieldDict.ContainsKey(_KEEP_WORDS))
            {
                filter = new KeepWordsTokenFilter(filterName, JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_KEEP_WORDS)));
            }
            else if (fieldDict.ContainsKey(_KEEP_WORDS_PATH))
            {
                filter = new KeepWordsTokenFilter(filterName, fieldDict.GetString(_KEEP_WORDS_PATH));
            }
            else
            {
                throw new RequiredPropertyMissingException(_KEEP_WORDS + "/" + _KEEP_WORDS_PATH);
            }

            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.Lowercase = fieldDict.GetBool(_KEEP_WORDS_CASE, KeepWordsTokenFilter._LOWERCASE_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is KeepWordsTokenFilter))
                throw new SerializeTypeException<KeepWordsTokenFilter>();

            KeepWordsTokenFilter filter = value as KeepWordsTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_KEEP_WORDS_CASE, filter.Lowercase, KeepWordsTokenFilter._LOWERCASE_DEFAULT);
            if (filter.KeepWords != null && filter.KeepWords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_KEEP_WORDS, filter.KeepWords);
            }
            else
            {
                fieldDict.AddObject(_KEEP_WORDS_PATH, filter.KeepWordsPath);
            }           

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
