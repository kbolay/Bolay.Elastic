using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class StatisticsAggregation : AggregationResultBase
    {
        [JsonProperty("count")]
        public Int64 Count { get; set; }

        [JsonProperty("min")]
        public Double Minimum { get; set; }

        [JsonProperty("max")]
        public Double Maximum { get; set; }

        [JsonProperty("sum")]
        public Double Sum { get; set; }

        [JsonProperty("avg")]
        public Double Average { get; set; }
    }
}
