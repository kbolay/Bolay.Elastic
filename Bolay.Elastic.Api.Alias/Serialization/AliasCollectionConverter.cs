using Bolay.Elastic.Api.Alias.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias.Serialization
{
    public class AliasCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> aliasDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (!aliasDict.Any())
                return null;

            List<IndexAlias> aliases = new List<IndexAlias>();
            foreach (KeyValuePair<string, object> aliasKvp in aliasDict)
            {
                IndexAlias alias = JsonConvert.DeserializeObject<IndexAlias>(aliasKvp.Value.ToString());
                if (alias != null)
                {
                    alias.Name = aliasKvp.Key;
                    aliases.Add(alias);
                }
            }

            if (!aliases.Any())
                return null;

            return aliases;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
