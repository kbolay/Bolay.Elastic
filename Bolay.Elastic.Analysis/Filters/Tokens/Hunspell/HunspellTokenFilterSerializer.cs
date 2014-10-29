using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Hunspell
{
    internal class HunspellTokenFilterSerializer : JsonConverter
    {
        private const string _IGNORE_CASE = "ignore_case";
        private const string _STRICT_AFFIX_PARSING = "strict_affix_parsing";
        private const string _LOCALE = "locale";
        private const string _LANGUAGE = "language";
        private const string _DICTIONARY = "dictionary";
        private const string _DEDUPLICATE = "dedup";
        private const string _RECURSION_LEVEL = "recursion_level";

        private static List<string> _LOCALE_KEYS = new List<string>() { "language", "lang", "locale" };

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            KeyValuePair<string, object> localKvp = fieldDict.FirstOrDefault(x => _LOCALE_KEYS.Contains(x.Key));
            if (string.IsNullOrWhiteSpace(localKvp.Key))
                throw new RequiredPropertyMissingException(string.Join("/", _LOCALE_KEYS));

            HunspellTokenFilter filter = new HunspellTokenFilter(filterDict.First().Key, localKvp.Value.ToString(), localKvp.Key.Equals(_LOCALE) ? true : false);
            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.IgnoreCase = fieldDict.GetBool(_IGNORE_CASE, HunspellTokenFilter._IGNORE_CASE_DEFAULT);
            filter.StrictAffixParsing = fieldDict.GetBool(_STRICT_AFFIX_PARSING, HunspellTokenFilter._STRICT_AFFIX_PARSING_DEFAULT);
            filter.Deduplicate = fieldDict.GetBool(_DEDUPLICATE, HunspellTokenFilter._DEDUPLICATE_DEFAULT);
            filter.Dictionary = fieldDict.GetStringOrDefault(_DICTIONARY);
            filter.RecursionLevel = fieldDict.GetInt32(_RECURSION_LEVEL, HunspellTokenFilter._RECURSION_LEVEL_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is HunspellTokenFilter))
                throw new SerializeTypeException<HunspellTokenFilter>();

            HunspellTokenFilter filter = value as HunspellTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_IGNORE_CASE, filter.IgnoreCase, HunspellTokenFilter._IGNORE_CASE_DEFAULT);
            fieldDict.AddObject(_STRICT_AFFIX_PARSING, filter.StrictAffixParsing, HunspellTokenFilter._STRICT_AFFIX_PARSING_DEFAULT);
            fieldDict.AddObject(_LOCALE, filter.Locale);
            fieldDict.AddObject(_LANGUAGE, filter.Language);
            fieldDict.AddObject(_DICTIONARY, filter.Dictionary);
            fieldDict.AddObject(_DEDUPLICATE, filter.Deduplicate, HunspellTokenFilter._DEDUPLICATE_DEFAULT);
            fieldDict.AddObject(_RECURSION_LEVEL, filter.RecursionLevel, HunspellTokenFilter._RECURSION_LEVEL_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
