using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters
{
    internal class CharacterFilterCollectionSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> collectionDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            CharacterFilterTypeEnum filterType = CharacterFilterTypeEnum.Mapping;

            CharacterFilterCollection collection = new CharacterFilterCollection();
            foreach (KeyValuePair<string, object> kvp in collectionDict)
            {
                // determine char filter type
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(kvp.Value.ToString());
                string filterTypeStr = fieldDict.GetString(_TYPE);
                filterType = CharacterFilterTypeEnum.Find(filterTypeStr);
                if (filterType == null)
                    throw new Exception(filterTypeStr + " is not a valid character filter.");

                Dictionary<string, object> charFilterDict = new Dictionary<string, object>();
                charFilterDict.Add(kvp.Key, kvp.Value);
                string charFilterJson = JsonConvert.SerializeObject(charFilterDict);
                collection.Add(JsonConvert.DeserializeObject(charFilterJson, filterType.ImplementationType) as ICharacterFilter);
            }

            if (!collection.Any())
                return null;

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is CharacterFilterCollection))
                throw new SerializeTypeException<CharacterFilterCollection>();

            CharacterFilterCollection collection = value as CharacterFilterCollection;
            Dictionary<string, object> collectionDict = new Dictionary<string,object>();
            foreach (ICharacterFilter charFilter in collection)
            {
                string charFilterJson = JsonConvert.SerializeObject(charFilter);
                Dictionary<string, object> charFilterDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(charFilterJson);

                collectionDict.Add(charFilterDict.First().Key, charFilterDict.First().Value);
            }

            if (collectionDict.Any())
            {
                serializer.Serialize(writer, collectionDict);
            }
        }
    }
}
