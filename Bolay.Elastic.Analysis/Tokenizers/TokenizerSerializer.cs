using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    internal class TokenizerSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> tokenDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenDict.First().Value.ToString());

            TokenizerTypeEnum tokenizerType = TokenizerTypeEnum.EdgeNGram;
            string tokenizerTypeStr = tokenDict.GetString(_TYPE);
            tokenizerType = TokenizerTypeEnum.Find(tokenizerTypeStr);
            if (tokenizerType == null)
                throw new Exception(tokenizerTypeStr + " is not a valid tokenizer.");

            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tokenDict), tokenizerType.ImplementationType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
