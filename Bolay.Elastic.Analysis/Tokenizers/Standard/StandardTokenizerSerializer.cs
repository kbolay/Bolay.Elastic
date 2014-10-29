using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Standard
{
    internal class StandardTokenizerSerializer : JsonConverter
    {        
        private const string _MAXIMUM_TOKEN_LENGTH = "max_token_length";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            StandardTokenizer token = new StandardTokenizer(tokenDict.First().Key);
            TokenizerBase.Deserialize(token, fieldDict);
            token.MaximumTokenLength = fieldDict.GetInt32(_MAXIMUM_TOKEN_LENGTH, StandardTokenizer._MAXIMUM_TOKEN_LENGTH_DEFAULT);

            return token;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StandardTokenizer))
                throw new SerializeTypeException<StandardTokenizer>();

            StandardTokenizer token = value as StandardTokenizer;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenizerBase.Serialize(token, fieldDict);
            fieldDict.AddObject(_MAXIMUM_TOKEN_LENGTH, token.MaximumTokenLength, StandardTokenizer._MAXIMUM_TOKEN_LENGTH_DEFAULT);

            Dictionary<string, object> tokenDict = new Dictionary<string, object>();
            tokenDict.Add(token.Name, fieldDict);

            serializer.Serialize(writer, tokenDict);
        }
    }
}
