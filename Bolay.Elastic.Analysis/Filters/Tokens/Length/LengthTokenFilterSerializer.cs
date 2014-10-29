using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Length
{
    internal class LengthTokenFilterSerializer : JsonConverter
    {
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> filterDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(filterDict.First().Value.ToString());

            LengthTokenFilter filter = new LengthTokenFilter(filterDict.First().Key);
            TokenFilterBase.Deserialize(filter, fieldDict);
            filter.Maximum = fieldDict.GetInt32(_MAXIMUM, LengthTokenFilter._MAXIMUM_DEFAULT);
            filter.Minimum = fieldDict.GetInt32(_MINIMUM, LengthTokenFilter._MINIMUM_DEFAULT);

            return filter;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is LengthTokenFilter))
                throw new SerializeTypeException<LengthTokenFilter>();

            LengthTokenFilter filter = value as LengthTokenFilter;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            TokenFilterBase.Serialize(filter, fieldDict);
            fieldDict.AddObject(_MINIMUM, filter.Minimum, LengthTokenFilter._MINIMUM_DEFAULT);
            fieldDict.AddObject(_MAXIMUM, filter.Maximum, LengthTokenFilter._MAXIMUM_DEFAULT);

            Dictionary<string, object> filterDict = new Dictionary<string, object>();
            filterDict.Add(filter.Name, fieldDict);

            serializer.Serialize(writer, filterDict);
        }
    }
}
