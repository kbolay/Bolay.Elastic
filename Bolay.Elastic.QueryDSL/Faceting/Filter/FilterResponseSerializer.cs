using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Filter
{
    public class FilterResponseSerializer : JsonConverter
    {
        private const string _COUNT = "count";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string name = facetDict.First().Key;

            facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.First().Value.ToString());
            return new FilterResponse(name, facetDict.GetInt64(_COUNT));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
