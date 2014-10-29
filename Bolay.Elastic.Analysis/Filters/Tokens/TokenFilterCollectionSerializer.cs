using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens
{
    internal class TokenFilterCollectionSerializer : JsonConverter
    {
        private const string _TYPE = "type";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> collectionDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            TokenFilterTypeEnum filterType = TokenFilterTypeEnum.AsciiFolding;

            TokenFilterCollection collection = new TokenFilterCollection();
            foreach (KeyValuePair<string, object> kvp in collectionDict)
            {
                // determine char filter type
                Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(kvp.Value.ToString());
                string filterTypeStr = fieldDict.GetString(_TYPE);
                filterType = TokenFilterTypeEnum.Find(filterTypeStr);
                if (filterType == null)
                    throw new Exception(filterTypeStr + " is not a valid token filter.");

                Dictionary<string, object> tokenFilterDict = new Dictionary<string, object>();
                tokenFilterDict.Add(kvp.Key, kvp.Value);
                string tokenFilterJson = JsonConvert.SerializeObject(tokenFilterDict);
                collection.Add(JsonConvert.DeserializeObject(tokenFilterJson, filterType.ImplementationType) as ITokenFilter);
            }

            if (!collection.Any())
                return null;

            return collection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TokenFilterCollection))
                throw new SerializeTypeException<TokenFilterCollection>();

            TokenFilterCollection collection = value as TokenFilterCollection;
            Dictionary<string, object> collectionDict = new Dictionary<string, object>();
            foreach (ITokenFilter charFilter in collection)
            {
                string tokenFilterJson = JsonConvert.SerializeObject(charFilter);
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
