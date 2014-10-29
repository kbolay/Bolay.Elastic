using Bolay.Elastic.Api.Alias.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias.Serialization
{
    public class IndexCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> indices = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (!indices.Any())
                return null;

            List<Index> indexList = new List<Index>();

            foreach (KeyValuePair<string, object> indexKvp in indices)
            {
                Index index = JsonConvert.DeserializeObject<Index>(indexKvp.Value.ToString());
                if (index != null)
                {
                    index.Name = indexKvp.Key;
                    indexList.Add(index);
                }
            }

            if (!indexList.Any())
                return null;

            return indexList;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
