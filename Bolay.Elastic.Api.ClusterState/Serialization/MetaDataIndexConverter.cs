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
    public class MetaDataIndexConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
            //two things can come into here. 
            //1. the full index { "index_name" : { "type_name": { ... "properties": {...}}}}
            //2. the index object { "type_name": { ... "properties": {...}}}
            //Index index = new Index();
            //Dictionary<string, object> indexKvps = serializer.Deserialize<Dictionary<string, object>>(reader);       

            //index.Types = new List<IndexType>();
            //foreach (KeyValuePair<string, object> typeKvp in indexKvps)
            //{
            //    IndexType type = JsonConvert.DeserializeObject<IndexType>(typeKvp.Value.ToString());

            //    if (type == null)
            //        continue;

            //    type.Name = typeKvp.Key;
            //    index.Types.Add(type);
            //}

            //if (!index.Types.Any())
            //    index.Types = null;

            //return index;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
