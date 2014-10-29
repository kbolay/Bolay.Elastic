using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Keyword
{
    internal class KeywordTokenizerSerializer : JsonConverter
    {
        private const string _BUFFER_SIZE = "buffer_size";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            KeywordTokenizer token = new KeywordTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);
            token.BufferSize = fieldDict.GetInt64(_BUFFER_SIZE, KeywordTokenizer._BUFFER_SIZE_DEFAULT);

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is KeywordTokenizer))
                throw new SerializeTypeException<KeywordTokenizer>();

            KeywordTokenizer token = value as KeywordTokenizer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);
            fieldDict.AddObject(_BUFFER_SIZE, token.BufferSize, KeywordTokenizer._BUFFER_SIZE_DEFAULT);

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
