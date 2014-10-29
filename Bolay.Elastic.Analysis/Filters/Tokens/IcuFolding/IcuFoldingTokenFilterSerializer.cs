using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.IcuFolding
{
    internal class IcuFoldingTokenFilterSerializer : JsonConverter
    {
        private const string _UNICODE_SET_FILTER = "unicodeSetFilter";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            IcuFoldingTokenFilter filter = new IcuFoldingTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.UnicodeSetFilter = fieldDict.GetStringOrDefault(_UNICODE_SET_FILTER);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IcuFoldingTokenFilter))
                throw new SerializeTypeException<IcuFoldingTokenFilter>();

            IcuFoldingTokenFilter filter = value as IcuFoldingTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_UNICODE_SET_FILTER, filter.UnicodeSetFilter);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
