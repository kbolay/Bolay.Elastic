using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    public class TermsResponseSerializer : JsonConverter
    {
        private const string _MISSING = "missing";
        private const string _TOTAL = "total";
        private const string _OTHER = "other";
        private const string _TERMS = "terms";
        private const string _TERM = "term";
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
            List<Dictionary<string, object>> termDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(facetDict.GetString(_TERMS));

            List<TermBucket> terms = new List<TermBucket>();
            foreach(Dictionary<string, object> termDict in termDictList)
            {
                terms.Add(new TermBucket(termDict[_TERM], termDict.GetInt64(_COUNT)));
            }

            return new TermsResponse(
                    name,
                    facetDict.GetInt64(_MISSING),
                    facetDict.GetInt64(_TOTAL),
                    facetDict.GetInt64(_OTHER),
                    terms);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
