using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Models
{
    internal class IndexMappingCollectionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> indexCollectionDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            IndexMappingCollection collection = new IndexMappingCollection();
            foreach (KeyValuePair<string, object> indexKvp in indexCollectionDict)
            {
                Dictionary<string, object> indexDict = new Dictionary<string, object>();
                indexDict.Add(indexKvp.Key, indexKvp.Value);

                string indexJson = JsonConvert.SerializeObject(indexDict);
                collection.Add(JsonConvert.DeserializeObject<IndexMapping>(indexJson));
            }

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
