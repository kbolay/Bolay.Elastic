using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolay.Elastic.Api.Mapping.Models
{
    internal class IndexMappingSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> indexDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string indexName = indexDict.First().Key;

            // make sure we get the analysis in place before trying to deserialize the types of the index.
            PropertyAnalyzer.PopulateIndexAnalyzers(IndexMappingCollection.GetIndexAnalysis(indexName));

            Dictionary<string, object> typesDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(indexDict.First().Value.ToString());

            List<TypeMapping> types = new List<TypeMapping>();
            foreach (KeyValuePair<string, object> typeKvp in typesDict)
            {
                Dictionary<string, object> typeDict = new Dictionary<string, object>();
                typeDict.Add(typeKvp.Key, typeKvp.Value);                

                string typeJson = JsonConvert.SerializeObject(typeDict);
                types.Add(JsonConvert.DeserializeObject<TypeMapping>(typeJson));
            }

            if (!types.Any())
                return null;

            return new IndexMapping(indexName, types);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IndexMapping))
                throw new SerializeTypeException<IndexMapping>();

            IndexMapping indexMapping = value as IndexMapping;

            Dictionary<string, object> typesDict = new Dictionary<string, object>();
            foreach (TypeMapping type in indexMapping.Types)
            {
                string typeJson = JsonConvert.SerializeObject(type);
                Dictionary<string, object> typeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(typeJson);

                typesDict.Add(typeDict.First().Key, typeDict.First().Value);
            }

            if (!typesDict.Any())
                return;

            Dictionary<string, object> indexDict = new Dictionary<string, object>();
            indexDict.Add(indexMapping.IndexName, typesDict);

            serializer.Serialize(writer, indexDict);
        }
    }
}
