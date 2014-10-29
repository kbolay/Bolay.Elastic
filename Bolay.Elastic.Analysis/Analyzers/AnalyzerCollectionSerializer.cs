using Bolay.Elastic.Analysis.Analyzers.Custom;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers
{
    internal class AnalyzerCollectionSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private static AnalyzerTypeEnum _TYPE_DEFAULT = AnalyzerTypeEnum.Custom;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> collectionDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            AnalyzerTypeEnum analyzerType = AnalyzerTypeEnum.Standard;

            AnalyzerCollection collection = new AnalyzerCollection();
            foreach (KeyValuePair<string, object> kvp in collectionDict)
            {
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(kvp.Value.ToString());
                string analyzerTypeStr = fieldDict.GetString(_TYPE, _TYPE_DEFAULT.ToString());
                analyzerType = AnalyzerTypeEnum.Find(analyzerTypeStr);
                if (analyzerType == null)
                    throw new Exception(analyzerTypeStr + " is not a valid analyzer.");

                Dictionary<string, object> analyzerDict = new Dictionary<string, object>();
                analyzerDict.Add(kvp.Key, kvp.Value);
                string analyzerJson = JsonConvert.SerializeObject(analyzerDict);

                IAnalyzer analyzer = null;
                if (analyzerType != AnalyzerTypeEnum.Custom)
                {
                    analyzer = JsonConvert.DeserializeObject(analyzerJson, analyzerType.ImplementationType) as IAnalyzer;
                }
                else
                {
                    analyzer = JsonConvert.DeserializeObject<CustomAnalyzer>(analyzerJson);
                }

                collection.Add(analyzer);
            }

            if (!collection.Any())
                return null;

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is AnalyzerCollection))
                throw new SerializeTypeException<AnalyzerCollection>();

            AnalyzerCollection collection = value as AnalyzerCollection;
            Dictionary<string, object> collectionDict = new Dictionary<string, object>();
            foreach (IAnalyzer analyzer in collection)
            {
                string analyzerJson = JsonConvert.SerializeObject(analyzer);
                Dictionary<string, object> analyzerDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(analyzerJson);

                collectionDict.Add(analyzerDict.First().Key, analyzerDict.First().Value);
            }

            if (collectionDict.Any())
            {
                serializer.Serialize(writer, collectionDict);
            }
        }
    }
}
