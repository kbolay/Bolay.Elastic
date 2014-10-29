using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.KeywordMarker
{
    internal class KeywordMarkerTokenFilterSerializer : JsonConverter
    {
        private const string _KEYWORDS = "keywords";
        private const string _KEYWORDS_PATH = "keywords_path";
        private const string _IGNORE_CASE = "ignore_case";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            KeywordMarkerTokenFilter filter = new KeywordMarkerTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            if (fieldDict.ContainsKey(_KEYWORDS))
            {
                filter.Keywords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_KEYWORDS));
            }
            filter.KeywordsPath = fieldDict.GetStringOrDefault(_KEYWORDS_PATH);
            filter.IgnoreCase = fieldDict.GetBool(_IGNORE_CASE, KeywordMarkerTokenFilter._IGNORE_CASE_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is KeywordMarkerTokenFilter))
                throw new SerializeTypeException<KeywordMarkerTokenFilter>();

            KeywordMarkerTokenFilter filter = value as KeywordMarkerTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            if (filter.Keywords != null && filter.Keywords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_KEYWORDS, filter.Keywords);
            }
            fieldDict.AddObject(_KEYWORDS_PATH, filter.KeywordsPath);
            fieldDict.AddObject(_IGNORE_CASE, filter.IgnoreCase, KeywordMarkerTokenFilter._IGNORE_CASE_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
