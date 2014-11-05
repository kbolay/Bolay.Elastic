using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.TokenCount
{
    public class TokenCountPropertySerializer : JsonConverter
    {
        private const string _ANALYZER = "analyzer";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());

            TokenCountProperty prop = new TokenCountProperty(propDict.First().Key, fieldDict.GetString(_ANALYZER));
            NumberProperty.DeserializeNumber(prop, fieldDict);

            return prop;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TokenCountProperty))
                throw new SerializeTypeException<TokenCountProperty>();

            TokenCountProperty prop = value as TokenCountProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            NumberProperty.SerializeNumber(prop, fieldDict);
            fieldDict.AddObject(_ANALYZER, prop.Analyzer);

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(prop.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}
