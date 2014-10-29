using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Stop
{
    internal class StopTokenFilterSerializer : JsonConverter
    {
        private const string _STOPWORDS = "stopwords";
        private const string _STOPWORDS_PATH = "stopwords_path";
        private const string _IGNORE_CASE = "ignore_case";
        private const string _REMOVE_TRAILING = "remove_trailing";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            StopTokenFilter filter = new StopTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.IgnoreCase = fieldDict.GetBool(_IGNORE_CASE, StopTokenFilter._IGNORE_CASE_DEFAULT);
            filter.RemoveTrailing = fieldDict.GetBool(_REMOVE_TRAILING, StopTokenFilter._REMOVE_TRAILING_DEFAULT);

            if (fieldDict.ContainsKey(_STOPWORDS))
            {
                filter.Stopwords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_STOPWORDS));
            }

            filter.StopwordsPath = fieldDict.GetStringOrDefault(_STOPWORDS_PATH);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StopTokenFilter))
                throw new SerializeTypeException<StopTokenFilter>();

            StopTokenFilter filter = value as StopTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            if (filter.Stopwords != null && filter.Stopwords.Any())
            {
                fieldDict.AddObject(_STOPWORDS, filter.Stopwords.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            fieldDict.AddObject(_STOPWORDS_PATH, filter.StopwordsPath);
            fieldDict.AddObject(_IGNORE_CASE, filter.IgnoreCase, StopTokenFilter._IGNORE_CASE_DEFAULT);
            fieldDict.AddObject(_REMOVE_TRAILING, filter.RemoveTrailing, StopTokenFilter._REMOVE_TRAILING_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
