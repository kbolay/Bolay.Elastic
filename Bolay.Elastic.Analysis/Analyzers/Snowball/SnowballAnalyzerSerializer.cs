using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Snowball
{
    internal class SnowballAnalyzerSerializer : JsonConverter
    {
        private const string _LANGUAGE = "language";
        private const string _STOPWORDS = "stopwords";
        private const string _STOPWORDS_PATH = "stopwords_path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analyzerDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            SnowballAnalyzer analyzer = new SnowballAnalyzer(analyzerDict.First().Key);
            AnalyzerBase.Deserialize(analyzer, fieldDict);
            analyzer.Language = SnowballLanguageEnum.Find(fieldDict.GetString(_LANGUAGE, SnowballAnalyzer._LANGUAGE_DEFAULT.ToString()));
            if (fieldDict.ContainsKey(_STOPWORDS))
            {
                string stopwordsStr = fieldDict.GetString(_STOPWORDS);
                if (stopwordsStr.Equals(SnowballAnalyzer._EMPTY_STOPWORDS_DEFAULT))
                {
                    analyzer.Stopwords = new List<string>();
                }
                else
                {
                    analyzer.Stopwords = JsonConvert.DeserializeObject<IEnumerable<string>>(stopwordsStr);
                }
            }

            analyzer.StopwordsPath = fieldDict.GetStringOrDefault(_STOPWORDS_PATH);

            return analyzer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SnowballAnalyzer))
                throw new SerializeTypeException<SnowballAnalyzer>();

            SnowballAnalyzer analyzer = value as SnowballAnalyzer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);
            fieldDict.AddObject(_LANGUAGE, analyzer.Language.ToString(), SnowballAnalyzer._LANGUAGE_DEFAULT.ToString());
            if (analyzer.Stopwords != null)
            {
                if (analyzer.Stopwords == new List<string>() || analyzer.Stopwords.All(x => string.IsNullOrWhiteSpace(x)))
                    fieldDict.Add(_STOPWORDS, SnowballAnalyzer._EMPTY_STOPWORDS_DEFAULT);
                else
                    fieldDict.Add(_STOPWORDS, analyzer.Stopwords);
            }

            fieldDict.AddObject(_STOPWORDS_PATH, analyzer.StopwordsPath);

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
