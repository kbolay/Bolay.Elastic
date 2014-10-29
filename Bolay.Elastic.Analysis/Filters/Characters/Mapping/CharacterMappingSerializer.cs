using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.Mapping
{
    internal class CharacterMappingSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string fullStrValue = reader.Value.ToString();

            string[] splitMapping = fullStrValue.Split(new string[] { CharacterMapping._DELIMITER }, StringSplitOptions.RemoveEmptyEntries);

            if (splitMapping.Count() != 2)
            {
                throw new Exception(fullStrValue + " is not in the correct format.");
            }

            return new CharacterMapping(splitMapping.First(), splitMapping.Last());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is CharacterMapping))
                throw new SerializeTypeException<CharacterMapping>();

            CharacterMapping mapping = value as CharacterMapping;

            serializer.Serialize(writer, mapping.ToString());
        }
    }
}
