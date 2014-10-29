using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Truncate
{
    internal class TruncateTokenFilterSerializer : JsonConverter
    {
        private const string _LENGTH = "length";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            TruncateTokenFilter filter = new TruncateTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.Length = fieldDict.GetInt32(_LENGTH, TruncateTokenFilter._LENGTH_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TruncateTokenFilter))
                throw new SerializeTypeException<TruncateTokenFilter>();

            TruncateTokenFilter filter = value as TruncateTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_LENGTH, filter.Length, TruncateTokenFilter._LENGTH_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
