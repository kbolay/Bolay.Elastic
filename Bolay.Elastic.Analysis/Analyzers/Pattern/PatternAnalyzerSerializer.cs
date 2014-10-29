using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Pattern
{
    internal class PatternAnalyzerSerializer : JsonConverter
    {
        private const string _LOWERCASE = "lowercase";
        private const string _PATTERN = "pattern";
        private const string _FLAGS = "flags";
        private const string _STOPWORDS = "stopwords";

        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>() { " | ", "| ", " |", "|" }; 

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analyzerDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            PatternAnalyzer analyzer = null;
            if (fieldDict.ContainsKey(_PATTERN))
            {
                analyzer = new PatternAnalyzer(analyzerDict.First().Key, fieldDict.GetString(_PATTERN));
            }
            else
            {
                analyzer = new PatternAnalyzer(analyzerDict.First().Key);
            }
            
            AnalyzerBase.Deserialize(analyzer, fieldDict);
            analyzer.Lowercase = fieldDict.GetBool(_LOWERCASE, PatternAnalyzer._LOWERCASE_DEFAULT);

            if (fieldDict.ContainsKey(_FLAGS))
            {
                List<RegexFlagEnum> flagList = new List<RegexFlagEnum>();
                string flagsStr = fieldDict.GetString(_FLAGS);
                foreach (string flagValue in flagsStr.Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    flagList.Add(RegexFlagEnum.Find(flagValue));
                }

                if (flagList.Any())
                    analyzer.Flags = flagList;
            }

            if (fieldDict.ContainsKey(_STOPWORDS))
            {
                analyzer.Stopwords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_STOPWORDS));
            }

            return analyzer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is PatternAnalyzer))
                throw new SerializeTypeException<PatternAnalyzer>();

            PatternAnalyzer analyzer = value as PatternAnalyzer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);

            fieldDict.AddObject(_LOWERCASE, analyzer.Lowercase, PatternAnalyzer._LOWERCASE_DEFAULT);
            fieldDict.AddObject(_PATTERN, analyzer.Pattern, PatternAnalyzer._REGEX_PATTERN_DEFAULT);

            if (analyzer.Flags != null && analyzer.Flags.Any(x => x != null))
            {
                fieldDict.AddObject(_FLAGS, string.Join(_FLAG_DELIMITER, analyzer.Flags.Where(x => x != null).Select(x => x.ToString())));
            }

            if (analyzer.Stopwords != null && analyzer.Stopwords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_STOPWORDS, analyzer.Stopwords);
            }

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
