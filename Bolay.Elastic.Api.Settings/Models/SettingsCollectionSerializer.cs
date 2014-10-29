using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings.Models
{
    internal class SettingsCollectionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> indicesDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            SettingsCollection collection = new SettingsCollection(); 
            foreach (KeyValuePair<string, object> indexKvp in indicesDict)
            {
                Dictionary<string, object> indexDict = new Dictionary<string, object>();
                indexDict.Add(indexKvp.Key, indexKvp.Value);

                string indexJson = JsonConvert.SerializeObject(indexDict);
                collection.Add(JsonConvert.DeserializeObject<Settings>(indexJson));
            }

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
