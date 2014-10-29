using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    public class TermsStatisticsResponseSerializer : JsonConverter
    {
        private const string _TERMS = "terms";
        private const string _TERM = "term";
        private const string _COUNT = "count";
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";
        private const string _TOTAL_COUNT = "total_count";
        private const string _SUM = "total";
        private const string _AVERAGE = "mean";

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

            List<TermStatisticBucket> terms = new List<TermStatisticBucket>();
            foreach (Dictionary<string, object> termDict in termDictList)
            {
                terms.Add(new TermStatisticBucket(
                        termDict[_TERM],
                        termDict.GetDouble(_MINIMUM),
                        termDict.GetDouble(_MAXIMUM),
                        termDict.GetDouble(_SUM),
                        termDict.GetDouble(_AVERAGE),
                        termDict.GetInt64(_COUNT),
                        termDict.GetInt64(_TOTAL_COUNT)
                    ));
            }

            return new TermsStatisticsResponse(name, terms);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
