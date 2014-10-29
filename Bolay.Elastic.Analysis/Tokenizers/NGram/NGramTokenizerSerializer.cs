using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.NGram
{
    internal class NGramTokenizerSerializer : JsonConverter
    {
        private const string _MINIMUM_GRAM = "min_gram";
        private const string _MAXIMUM_GRAM = "max_gram";
        private const string _TOKEN_CHARACTERS = "token_chars";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            NGramTokenizer token = new NGramTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);
            token.MinimumSize = fieldDict.GetInt64(_MINIMUM_GRAM, NGramTokenizer._MINIMUM_SIZE_DEFAULT);
            token.MaximumSize = fieldDict.GetInt64(_MAXIMUM_GRAM, NGramTokenizer._MAXIMUM_SIZE_DEFAULT);

            if (fieldDict.ContainsKey(_TOKEN_CHARACTERS))
            {
                CharacterClassEnum charClass = CharacterClassEnum.Digit;
                List<CharacterClassEnum> characterClasses = new List<CharacterClassEnum>();
                foreach (string charClassStr in JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_TOKEN_CHARACTERS)))
                {
                    charClass = CharacterClassEnum.Find(charClassStr);
                    if (charClass != null)
                    {
                        characterClasses.Add(charClass);
                    }
                    else
                    {
                        throw new Exception(charClassStr + " is not a valid instance of the character class enumeration.");
                    }
                }

                if (characterClasses.Any())
                    token.TokenCharacters = characterClasses;
            }

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is NGramTokenizer))
                throw new SerializeTypeException<NGramTokenizer>();

            NGramTokenizer token = value as NGramTokenizer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);
            fieldDict.AddObject(_MINIMUM_GRAM, token.MinimumSize, NGramTokenizer._MINIMUM_SIZE_DEFAULT);
            fieldDict.AddObject(_MAXIMUM_GRAM, token.MaximumSize, NGramTokenizer._MAXIMUM_SIZE_DEFAULT);

            if (token.TokenCharacters != null && token.TokenCharacters.Any())
            {
                fieldDict.AddObject(_TOKEN_CHARACTERS, token.TokenCharacters.Select(x => x.ToString()));
            }

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
