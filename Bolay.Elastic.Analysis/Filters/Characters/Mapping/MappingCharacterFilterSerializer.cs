using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.Mapping
{
    internal class MappingCharacterFilterSerializer : JsonConverter
    {
        private const string _MAPPINGS = "mappings";
        private const string _MAPPINGS_PATH = "mappings_path";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());
            string filterName = filterDict.First().Key;

            MappingCharacterFilter filter = null;
            if (fieldDict.ContainsKey(_MAPPINGS))
            { 
                filter = new MappingCharacterFilter(filterName, JsonConvert.DeserializeObject<IEnumerable<CharacterMapping>>(fieldDict.GetString(_MAPPINGS)));
            }
            else if(fieldDict.ContainsKey(_MAPPINGS_PATH))
            {
                filter = new MappingCharacterFilter(filterName, fieldDict.GetString(_MAPPINGS_PATH));
            }
            else
            {
                throw new RequiredPropertyMissingException(_MAPPINGS + "/" + _MAPPINGS_PATH);
            }

            CharacterFilterBase.Deserialize(filter, fieldDict);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MappingCharacterFilter))
                throw new SerializeTypeException<MappingCharacterFilter>();

            MappingCharacterFilter filter = value as MappingCharacterFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            CharacterFilterBase.Serialize(filter, fieldDict);

            if (filter.Mappings != null && filter.Mappings.Any(x => x != null))
            {
                fieldDict.AddObject(_MAPPINGS, filter.Mappings);
            }
            else
            {
                fieldDict.AddObject(_MAPPINGS_PATH, filter.MappingsPath);
            }

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
