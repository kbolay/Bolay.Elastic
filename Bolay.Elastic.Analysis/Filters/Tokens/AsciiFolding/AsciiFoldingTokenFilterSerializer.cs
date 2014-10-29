using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.AsciiFolding
{
    internal class AsciiFoldingTokenFilterSerializer : JsonConverter
    {
        private const string _PRESERVE_ORIGINAL = "preserve_original";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            AsciiFoldingTokenFilter filter = new AsciiFoldingTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.PreserveOriginal = fieldDict.GetBool(_PRESERVE_ORIGINAL, AsciiFoldingTokenFilter._PRESERVE_ORIGINAL_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is AsciiFoldingTokenFilter))
                throw new SerializeTypeException<AsciiFoldingTokenFilter>();

            AsciiFoldingTokenFilter filter = value as AsciiFoldingTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_PRESERVE_ORIGINAL, filter.PreserveOriginal, AsciiFoldingTokenFilter._PRESERVE_ORIGINAL_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
