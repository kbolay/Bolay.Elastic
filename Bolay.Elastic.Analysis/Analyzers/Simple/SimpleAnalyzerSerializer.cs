using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Simple
{
    internal class SimpleAnalyzerSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> analyzerDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerDict.First().Value.ToString());

            SimpleAnalyzer analyzer = new SimpleAnalyzer(analyzerDict.First().Key);
            AnalyzerBase.Deserialize(analyzer, fieldDict);

            return analyzer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is SimpleAnalyzer))
                throw new SerializeTypeException<SimpleAnalyzer>();

            SimpleAnalyzer analyzer = value as SimpleAnalyzer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            AnalyzerBase.Serialize(analyzer, fieldDict);

            Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
            analyzerDict.Add(analyzer.Name, fieldDict);

            serializer.Serialize(writer, analyzerDict);
        }
    }
}
