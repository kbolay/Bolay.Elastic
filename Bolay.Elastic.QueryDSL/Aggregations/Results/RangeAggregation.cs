using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class RangeAggregation : BucketAggregation
    {
        [JsonProperty("to")]
        public object To { get; set; }

        [JsonProperty("to_as_string")]
        public string ToAsString { get; set; }

        [JsonProperty("from")]
        public object From { get; set; }

        [JsonProperty("from_as_string")]
        public string FromAsString { get; set; }
    }
}
