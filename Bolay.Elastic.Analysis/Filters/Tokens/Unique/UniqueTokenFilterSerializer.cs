using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Unique
{
    internal class UniqueTokenFilterSerializer : JsonConverter
    {
        private const string _ONLY_ON_SAME_POSITION = "only_on_same_position";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            UniqueTokenFilter filter = new UniqueTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.OnlyOnSamePosition = fieldDict.GetBool(_ONLY_ON_SAME_POSITION, UniqueTokenFilter._ONLY_ON_SAME_POSITION_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is UniqueTokenFilter))
                throw new SerializeTypeException<UniqueTokenFilter>();

            UniqueTokenFilter filter = value as UniqueTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);

            fieldDict.AddObject(_ONLY_ON_SAME_POSITION, filter.OnlyOnSamePosition, UniqueTokenFilter._ONLY_ON_SAME_POSITION_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
