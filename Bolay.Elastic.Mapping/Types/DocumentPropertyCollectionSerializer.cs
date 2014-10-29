using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    internal class DocumentPropertyCollectionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DocumentPropertyCollection collection = new DocumentPropertyCollection();
            Dictionary<string, object> fieldsDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> propertyKvp in fieldsDict)
            {
                Dictionary<string, object> propertyDict = new Dictionary<string, object>();
                propertyDict.Add(propertyKvp.Key, propertyKvp.Value);
                string propertyJson = JsonConvert.SerializeObject(propertyDict);

                collection.Add(JsonConvert.DeserializeObject<IDocumentProperty>(propertyJson));
            }

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DocumentPropertyCollection))
                throw new SerializeTypeException<DocumentPropertyCollection>();

            DocumentPropertyCollection collection = value as DocumentPropertyCollection;

            Dictionary<string, object> propertyCollectionDict = new Dictionary<string, object>();
            foreach (IDocumentProperty property in collection)
            {
                string propertyJson = JsonConvert.SerializeObject(property);
                Dictionary<string, object> propDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propertyJson);

                propertyCollectionDict.Add(propDict.First().Key, propDict.First().Value);
            }

            serializer.Serialize(writer, propertyCollectionDict);
        }
    }
}
