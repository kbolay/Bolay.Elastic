using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.WordDelimiter
{
    internal class WordDelimiterTokenFilterSerializer : JsonConverter
    {
        private const string _GENERATE_WORD_PARTS = "generate_word_parts";
        private const string _GENERATE_NUMBER_PARTS = "generate_number_parts";
        private const string _CATENATE_WORDS = "catenate_words";
        private const string _CATENATE_NUMBERS = "catenate_numbers";
        private const string _CATENATE_ALL = "catenate_all";
        private const string _SPLIT_ON_CASE_CHANGE = "split_on_case_change";
        private const string _PRESERVE_ORIGINAL = "preserve_original";
        private const string _SPLIT_ON_NUMERICS = "split_on_numerics";
        private const string _STEM_ENGLISH_POSSESSIVE = "stem_english_possessive";
        private const string _PROTECTED_WORDS = "protected_words";
        private const string _PROTECTED_WORDS_PATH = "protected_words_path";
        private const string _TYPE_TABLE = "type_table";
        private const string _TYPE_TABLE_PATH = "type_table_path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            WordDelimiterTokenFilter filter = new WordDelimiterTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.CatenateAll = fieldDict.GetBool(_CATENATE_ALL, WordDelimiterTokenFilter._CATENATE_ALL_DEFAULT);
            filter.CatenateNumbers = fieldDict.GetBool(_CATENATE_NUMBERS, WordDelimiterTokenFilter._CATENATE_NUMBERS_DEFAULT);
            filter.CatenateWords = fieldDict.GetBool(_CATENATE_WORDS, WordDelimiterTokenFilter._CATENATE_WORDS_DEFAULT);
            filter.GenerateNumberParts = fieldDict.GetBool(_GENERATE_NUMBER_PARTS, WordDelimiterTokenFilter._GENERATE_NUMBER_PARTS_DEFAULT);
            filter.GenerateWordParts = fieldDict.GetBool(_GENERATE_WORD_PARTS, WordDelimiterTokenFilter._GENERATE_WORD_PARTS_DEFAULT);
            filter.PreserveOriginal = fieldDict.GetBool(_PRESERVE_ORIGINAL, WordDelimiterTokenFilter._PRESERVE_ORIGINAL_DEFAULT);
            filter.SplitOnCaseChange = fieldDict.GetBool(_SPLIT_ON_CASE_CHANGE, WordDelimiterTokenFilter._SPLIT_ON_CASE_CHANGE_DEFAULT);
            filter.SplitOnNumerics = fieldDict.GetBool(_SPLIT_ON_NUMERICS, WordDelimiterTokenFilter._SPLIT_ON_NUMERICS_DEFAULT);
            filter.StemEnglishPossessive = fieldDict.GetBool(_STEM_ENGLISH_POSSESSIVE, WordDelimiterTokenFilter._STEM_ENGLISH_POSSESSIVE_DEFAULT);

            if (fieldDict.ContainsKey(_PROTECTED_WORDS))
            {
                filter.ProtectedWords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_PROTECTED_WORDS));
            }
            filter.ProtectedWordsPath = fieldDict.GetStringOrDefault(_PROTECTED_WORDS_PATH);
            if(fieldDict.ContainsKey(_TYPE_TABLE))
            {
                filter.TypeTable = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_TYPE_TABLE));
            }            
            filter.TypeTablePath = fieldDict.GetStringOrDefault(_TYPE_TABLE_PATH);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is WordDelimiterTokenFilter))
                throw new SerializeTypeException<WordDelimiterTokenFilter>();

            WordDelimiterTokenFilter filter = value as WordDelimiterTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_GENERATE_WORD_PARTS, filter.GenerateWordParts, WordDelimiterTokenFilter._GENERATE_WORD_PARTS_DEFAULT);
            fieldDict.AddObject(_GENERATE_NUMBER_PARTS, filter.GenerateNumberParts, WordDelimiterTokenFilter._GENERATE_NUMBER_PARTS_DEFAULT);
            fieldDict.AddObject(_CATENATE_WORDS, filter.CatenateWords, WordDelimiterTokenFilter._CATENATE_WORDS_DEFAULT);
            fieldDict.AddObject(_CATENATE_NUMBERS, filter.CatenateNumbers, WordDelimiterTokenFilter._CATENATE_NUMBERS_DEFAULT);
            fieldDict.AddObject(_CATENATE_ALL, filter.CatenateAll, WordDelimiterTokenFilter._CATENATE_ALL_DEFAULT);
            fieldDict.AddObject(_SPLIT_ON_CASE_CHANGE, filter.SplitOnCaseChange, WordDelimiterTokenFilter._SPLIT_ON_CASE_CHANGE_DEFAULT);
            fieldDict.AddObject(_PRESERVE_ORIGINAL, filter.PreserveOriginal, WordDelimiterTokenFilter._PRESERVE_ORIGINAL_DEFAULT);
            fieldDict.AddObject(_SPLIT_ON_NUMERICS, filter.SplitOnNumerics, WordDelimiterTokenFilter._SPLIT_ON_NUMERICS_DEFAULT);
            fieldDict.AddObject(_STEM_ENGLISH_POSSESSIVE, filter.StemEnglishPossessive, WordDelimiterTokenFilter._STEM_ENGLISH_POSSESSIVE_DEFAULT);
            if(filter.ProtectedWords != null && filter.ProtectedWords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_PROTECTED_WORDS, filter.ProtectedWords);
            }
            fieldDict.AddObject(_PROTECTED_WORDS_PATH, filter.ProtectedWordsPath);
            fieldDict.AddObject(_TYPE_TABLE, filter.TypeTable);
            fieldDict.AddObject(_TYPE_TABLE_PATH, filter.TypeTablePath);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
