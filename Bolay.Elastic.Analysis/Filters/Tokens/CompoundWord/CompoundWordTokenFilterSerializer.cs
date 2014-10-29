using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.CompoundWord
{
    internal class CompoundWordTokenFilterSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _WORD_LIST = "word_list";
        private const string _WORD_LIST_PATH = "word_list_path";
        private const string _MINIMUM_WORD_SIZE = "min_word_size";
        private const string _MINIMUM_SUBWORD_SIZE = "min_subword_size";
        private const string _MAXIMUM_SUBWORD_SIZE = "max_subword_size";
        private const string _ONLY_LONGEST_MATCH = "only_longest_match";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            string filterName = filterDict.First().Key;
            string wordListPath = null;
            IEnumerable<string> wordList = null;
            if (fieldDict.ContainsKey(_WORD_LIST))
            {
                wordList = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_WORD_LIST));
            }
            else if (fieldDict.ContainsKey(_WORD_LIST_PATH))
            {
                wordListPath = fieldDict.GetStringOrDefault(_WORD_LIST_PATH);
            }
            else
            {
                throw new RequiredPropertyMissingException(_WORD_LIST + "/" + _WORD_LIST_PATH);
            }

            CompoundWordTokenFilter filter = null;

            string tokenFilterStr = fieldDict.GetString(_TYPE);
            TokenFilterTypeEnum tokenFilter = TokenFilterTypeEnum.HyphenationDecompounder;
            tokenFilter = TokenFilterTypeEnum.Find(tokenFilterStr);
            if(tokenFilter == null)
            {
                throw new Exception(tokenFilterStr + " is not a valid token filter.");
            }
            else if(tokenFilter == TokenFilterTypeEnum.DictionaryDecompounder)
            {
                if (wordList != null)
                    filter = new DictionaryDecompounderTokenFilter(filterName, wordList);
                else
                    filter = new DictionaryDecompounderTokenFilter(filterName, wordListPath);
            }
            else if(tokenFilter == TokenFilterTypeEnum.HyphenationDecompounder)
            {
                if (wordList != null)
                    filter = new HyphenationDecompounderTokenFilter(filterName, wordList);
                else
                    filter = new HyphenationDecompounderTokenFilter(filterName, wordListPath);
            }
            else
            {
                throw new Exception(tokenFilterStr + " is not a valid compound word token filter.");
            }

            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.MaximumSubWordSize = fieldDict.GetInt32(_MAXIMUM_SUBWORD_SIZE, CompoundWordTokenFilter._MAXIMUM_SUBWORD_SIZE_DEFAULT);
            filter.MinimumSubWordSize = fieldDict.GetInt32(_MINIMUM_SUBWORD_SIZE, CompoundWordTokenFilter._MINIMUM_SUBWORD_SIZE_DEFAULT);
            filter.MinimumWordSize = fieldDict.GetInt32(_MINIMUM_WORD_SIZE, CompoundWordTokenFilter._MINIMUM_WORD_SIZE_DEFAULT);
            filter.OnlyLongestMatch = fieldDict.GetBool(_ONLY_LONGEST_MATCH, CompoundWordTokenFilter._ONLY_LONGEST_MATCH_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CompoundWordTokenFilter))
                throw new SerializeTypeException<CompoundWordTokenFilter>();

            CompoundWordTokenFilter filter = value as CompoundWordTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_MINIMUM_WORD_SIZE, filter.MinimumWordSize, CompoundWordTokenFilter._MINIMUM_WORD_SIZE_DEFAULT);
            fieldDict.AddObject(_MINIMUM_SUBWORD_SIZE, filter.MinimumSubWordSize, CompoundWordTokenFilter._MINIMUM_SUBWORD_SIZE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_SUBWORD_SIZE, filter.MaximumSubWordSize, CompoundWordTokenFilter._MAXIMUM_SUBWORD_SIZE_DEFAULT);
            fieldDict.AddObject(_ONLY_LONGEST_MATCH, filter.OnlyLongestMatch, CompoundWordTokenFilter._ONLY_LONGEST_MATCH_DEFAULT);

            if (filter.WordList != null && filter.WordList.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_WORD_LIST, filter.WordList);
            }
            fieldDict.AddObject(_WORD_LIST_PATH, filter.WordListPath);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
