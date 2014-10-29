using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Range
{
    public class RangeResponseSerializer : JsonConverter
    {
        private const string _RANGES = "ranges";
        private const string _TO = "to";
        private const string _FROM = "from";
        private const string _COUNT = "count";
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";
        private const string _TOTAL_COUNT = "total_count";
        private const string _SUM = "total";
        private const string _AVERAGE = "mean";

        private const Double _FROM_DEFAULT = 0.0;
        private const Double _TO_DEFAULT = 0.0;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string name = facetDict.First().Key;

            facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.First().Value.ToString());

            List<Dictionary<string, object>> rangeDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(facetDict.GetString(_RANGES));

            List<RangeResponseBucket> ranges = new List<RangeResponseBucket>();
            foreach (Dictionary<string, object> rangeDict in rangeDictList)
            {
                ranges.Add(new RangeResponseBucket(
                        rangeDict.GetDouble(_FROM, _FROM_DEFAULT),
                        rangeDict.GetDouble(_TO, _TO_DEFAULT),
                        rangeDict.GetDouble(_MINIMUM),
                        rangeDict.GetDouble(_MAXIMUM),
                        rangeDict.GetDouble(_SUM),
                        rangeDict.GetDouble(_AVERAGE),
                        rangeDict.GetInt64(_COUNT),
                        rangeDict.GetInt64(_TOTAL_COUNT)
                    ));
            }

            return new RangeResponse(name, ranges);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
