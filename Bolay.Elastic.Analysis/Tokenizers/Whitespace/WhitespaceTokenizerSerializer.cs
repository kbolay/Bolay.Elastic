using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Whitespace
{
    internal class WhitespaceTokenizerSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            WhitespaceTokenizer token = new WhitespaceTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is WhitespaceTokenizer))
                throw new SerializeTypeException<WhitespaceTokenizer>();

            WhitespaceTokenizer token = value as WhitespaceTokenizer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
