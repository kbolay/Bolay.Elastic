using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Histogram
{
    public class HistogramResponseSerializer : JsonConverter
    {
        private const string _KEY = "key";
        private const string _COUNT = "count";
        private const string _ENTRIES = "entries";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string name = facetDict.First().Key;

            facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.First().Value.ToString());
            List<Dictionary<string, object>> entryDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(facetDict.GetString(_ENTRIES));

            List<HistogramEntry> entries = new List<HistogramEntry>();
            foreach (Dictionary<string, object> entryDict in entryDictList)
            {
                entries.Add(new HistogramEntry(entryDict[_KEY], entryDict.GetInt64(_COUNT)));
            }

            return new HistogramResponse(name, entries);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
