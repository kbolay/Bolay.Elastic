using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.DelimitedPayload
{
    internal class DelimitedPayloadTokenFilterSerializer : JsonConverter
    {
        private const string _DELIMITER = "delimiter";
        private const string _ENCODING = "encoding";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            DelimitedPayloadTokenFilter filter = new DelimitedPayloadTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);

            filter.Delimiter = fieldDict.GetString(_DELIMITER, DelimitedPayloadTokenFilter._DELIMITER_DEFAULT);
            filter.Encoding = EncodingTypeEnum.Find(fieldDict.GetString(_ENCODING, DelimitedPayloadTokenFilter._ENCODING_DEFAULT.ToString()));

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DelimitedPayloadTokenFilter))
                throw new SerializeTypeException<DelimitedPayloadTokenFilter>();

            DelimitedPayloadTokenFilter filter = value as DelimitedPayloadTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_DELIMITER, filter.Delimiter, DelimitedPayloadTokenFilter._DELIMITER_DEFAULT);
            fieldDict.AddObject(_ENCODING, filter.Encoding.ToString(), DelimitedPayloadTokenFilter._ENCODING_DEFAULT.ToString());

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
