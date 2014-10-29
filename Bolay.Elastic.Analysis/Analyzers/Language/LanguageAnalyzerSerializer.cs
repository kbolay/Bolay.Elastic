using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    internal class LanguageAnalyzerSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _STOPWORDS = "stopwords";
        private const string _STOPWORDS_PATH = "stopwords_path";
        private const string _STEM_EXCLUSIONS = "stem_exclusion";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analyzerDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            string analyzerTypeStr = fieldDict.GetString(_TYPE);
            AnalyzerTypeEnum analyzerType = AnalyzerTypeEnum.English;
            analyzerType = AnalyzerTypeEnum.Find(analyzerTypeStr);
            if (analyzerType == null)
                throw new Exception(analyzerTypeStr + " is not a valid Analyzer Type.");

            object[] constructorArgs = new object[] { analyzerDict.First().Key };

            object analyzerObj = Activator.CreateInstance(analyzerType.ImplementationType, constructorArgs);
            LanguageAnalyzerBase analyzer = null;
            if(analyzerObj is StemExclusionLanguageAnalyzerBase)
            {
                StemExclusionLanguageAnalyzerBase stemAnalyzer = analyzerObj as StemExclusionLanguageAnalyzerBase;
                if(fieldDict.ContainsKey(_STEM_EXCLUSIONS))
                {
                    stemAnalyzer.StemExclusions = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_STEM_EXCLUSIONS));
                }

                analyzer = stemAnalyzer;
            }
            else if (analyzerObj is LanguageAnalyzerBase)
            {
                analyzer = analyzerObj as LanguageAnalyzerBase;
            }
            else
            {
                throw new Exception(analyzerType.ToString() + " is not a language analyzer.");
            }

            AnalyzerBase.Deserialize(analyzer, fieldDict);
            if (fieldDict.ContainsKey(_STOPWORDS))
            {
                string stopwordsStr = fieldDict.GetString(_STOPWORDS);
                if (stopwordsStr.Equals(LanguageAnalyzerBase._EMPTY_STOPWORDS_DEFAULT))
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
            bool isStemExclusions = false;
            LanguageAnalyzerBase analyzer = null;
            if (value is StemExclusionLanguageAnalyzerBase)
            {
                isStemExclusions = true;
            }
            else if (value is LanguageAnalyzerBase)
            {
                isStemExclusions = false;
            }
            else
            {
                throw new SerializeTypeException<LanguageAnalyzerBase>();
            }

            analyzer = value as LanguageAnalyzerBase;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);

            if (analyzer.Stopwords != null)
            {
                if (analyzer.Stopwords == new List<string>() || analyzer.Stopwords.All(x => string.IsNullOrWhiteSpace(x)))
                    fieldDict.Add(_STOPWORDS, LanguageAnalyzerBase._EMPTY_STOPWORDS_DEFAULT);
                else
                    fieldDict.Add(_STOPWORDS, analyzer.Stopwords);
            }

            fieldDict.AddObject(_STOPWORDS_PATH, analyzer.StopwordsPath);

            if (isStemExclusions)
            {
                StemExclusionLanguageAnalyzerBase stemAnalyzers = value as StemExclusionLanguageAnalyzerBase;

                if (stemAnalyzers.StemExclusions != null && stemAnalyzers.StemExclusions.Any())
                {
                    fieldDict.AddObject(_STEM_EXCLUSIONS, stemAnalyzers.StemExclusions);
                }
            }

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
