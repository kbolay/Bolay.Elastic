using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.CommonGrams
{
    internal class CommonGramsTokenFilterSerializer : JsonConverter
    {
        private const string _COMMON_WORDS = "common_words";
        private const string _COMMON_WORDS_PATH = "common_words_path";
        private const string _IGNORE_CASE = "ignore_case";
        private const string _QUERY_MODE = "query_mode";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            string filterName = filterDict.First().Key;
            CommonGramsTokenFilter filter = null;
            if (fieldDict.ContainsKey(_COMMON_WORDS))
            {
                filter = new CommonGramsTokenFilter(filterName, JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_COMMON_WORDS)));
            }
            else if (fieldDict.ContainsKey(_COMMON_WORDS_PATH))
            {
                filter = new CommonGramsTokenFilter(filterName, fieldDict.GetString(_COMMON_WORDS_PATH));
            }
            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.IgnoreCase = fieldDict.GetBool(_IGNORE_CASE, CommonGramsTokenFilter._IGNORE_CASE_DEFAULT);
            filter.QueryMode = fieldDict.GetBool(_QUERY_MODE, CommonGramsTokenFilter._QUERY_MODE_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CommonGramsTokenFilter))
                throw new SerializeTypeException<CommonGramsTokenFilter>();

            CommonGramsTokenFilter filter = value as CommonGramsTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_IGNORE_CASE, filter.IgnoreCase, CommonGramsTokenFilter._IGNORE_CASE_DEFAULT);
            fieldDict.AddObject(_QUERY_MODE, filter.QueryMode, CommonGramsTokenFilter._QUERY_MODE_DEFAULT);
            if (filter.CommonWords != null && filter.CommonWords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_COMMON_WORDS, filter.CommonWords);
            }
            else
            {
                fieldDict.AddObject(_COMMON_WORDS_PATH, filter.CommonWordsPath);
            }            

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
