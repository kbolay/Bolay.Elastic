using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Standard
{
    internal class StandardAnalyzerSerializer : JsonConverter
    {
        private const string _STOPWORDS = "stopwords";
        private const string _MAXIMUM_TOKEN_LENGTH = "max_token_length";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analyzerDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            StandardAnalyzer analyzer = new StandardAnalyzer(analyzerDict.First().Key);
            AnalyzerBase.Deserialize(analyzer, fieldDict);

            if (fieldDict.ContainsKey(_STOPWORDS))
            {
                analyzer.Stopwords = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_STOPWORDS));
            }

            analyzer.MaximumTokenLength = fieldDict.GetInt32(_MAXIMUM_TOKEN_LENGTH, StandardAnalyzer._MAXIMUM_TOKEN_LENGTH_DEFAULT);

            return analyzer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StandardAnalyzer))
                throw new SerializeTypeException<StandardAnalyzer>();

            StandardAnalyzer analyzer = value as StandardAnalyzer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);

            if (analyzer.Stopwords != null && analyzer.Stopwords.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                fieldDict.AddObject(_STOPWORDS, analyzer.Stopwords);
            }

            fieldDict.AddObject(_MAXIMUM_TOKEN_LENGTH, analyzer.MaximumTokenLength, StandardAnalyzer._MAXIMUM_TOKEN_LENGTH_DEFAULT);

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
