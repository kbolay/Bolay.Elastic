using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Statistics
{
    public class StatisticsResponseSerializer : JsonConverter
    {
        private const string _COUNT = "count";
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";
        private const string _SUM = "total";
        private const string _AVERAGE = "mean";
        private const string _SUM_OF_SQUARES = "sum_of_squares";
        private const string _VARIANCE = "variance";
        private const string _STANDARD_DEVIATION = "std_deviation";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> facetDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string name = facetDict.First().Key;

            facetDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(facetDict.First().Value.ToString());

            return new StatisticsResponse(
                name, 
                facetDict.GetDouble(_MINIMUM),
                facetDict.GetDouble(_MAXIMUM),
                facetDict.GetDouble(_SUM),
                facetDict.GetDouble(_AVERAGE),
                facetDict.GetDouble(_SUM_OF_SQUARES),
                facetDict.GetDouble(_VARIANCE),
                facetDict.GetDouble(_STANDARD_DEVIATION),
                facetDict.GetInt64(_COUNT));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
