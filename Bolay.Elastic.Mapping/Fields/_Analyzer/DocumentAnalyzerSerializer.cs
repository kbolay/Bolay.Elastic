using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Analyzer
{
    public class DocumentAnalyzerSerializer : JsonConverter
    {
        private const string _PATH = "path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            return new DocumentAnalyzer(fieldDict.GetString(_PATH));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentAnalyzer))
                throw new SerializeTypeException<DocumentAnalyzer>();

            DocumentAnalyzer analyzer = value as DocumentAnalyzer;
            Dictionary<string, object> fieldDict = new Dictionary<string,object>();
            fieldDict.AddObject(_PATH, analyzer.Path, DocumentAnalyzer._PATH_DEFAULT);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
