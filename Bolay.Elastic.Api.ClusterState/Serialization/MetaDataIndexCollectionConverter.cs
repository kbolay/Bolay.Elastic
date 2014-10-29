using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Api.ClusterState.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Serialization
{
    public class MetaDataIndexCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<MetaDataIndex> indexes = new List<MetaDataIndex>();
            Dictionary<string, object> indexKvps = serializer.Deserialize<Dictionary<string, object>>(reader);
            foreach (KeyValuePair<string, object> indexObj in indexKvps)
            {
                MetaDataIndex index = JsonConvert.DeserializeObject<MetaDataIndex>(indexObj.Value.ToString());
                if (index != null && index.Mappings != null && index.Mappings.Any())
                {
                    index.Name = indexObj.Key;
                    indexes.Add(index);
                }
            }

            if (indexes == null || !indexes.Any())
                return null;
            return indexes;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
