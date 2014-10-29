using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    internal class TokenizerCollectionSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> collectionDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            TokenizerTypeEnum tokenizerType = TokenizerTypeEnum.EdgeNGram;

            TokenizerCollection collection = new TokenizerCollection();
            foreach (KeyValuePair<string, object> kvp in collectionDict)
            {
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(kvp.Value.ToString());
                string filterTypeStr = fieldDict.GetString(_TYPE);
                tokenizerType = TokenizerTypeEnum.Find(filterTypeStr);
                if (tokenizerType == null)
                    throw new Exception(filterTypeStr + " is not a valid tokenizer.");

                Dictionary<string, object> tokenFilterDict = new Dictionary<string, object>();
                tokenFilterDict.Add(kvp.Key, kvp.Value);
                string charFilterJson = JsonConvert.SerializeObject(tokenFilterDict);
                collection.Add(JsonConvert.DeserializeObject(charFilterJson, tokenizerType.ImplementationType) as ITokenizer);
            }

            if (!collection.Any())
                return null;

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TokenizerCollection))
                throw new SerializeTypeException<TokenizerCollection>();

            TokenizerCollection collection = value as TokenizerCollection;
            Dictionary<string, object> collectionDict = new Dictionary<string, object>();
            foreach (ITokenizer tokenizer in collection)
            {
                string tokenFilterJson = JsonConvert.SerializeObject(tokenizer);
                Dictionary<string, object> tokenFilterDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenFilterJson);

                collectionDict.Add(tokenFilterDict.First().Key, tokenFilterDict.First().Value);
            }

            if (collectionDict.Any())
            {
                serializer.Serialize(writer, collectionDict);
            }
        }
    }
}
