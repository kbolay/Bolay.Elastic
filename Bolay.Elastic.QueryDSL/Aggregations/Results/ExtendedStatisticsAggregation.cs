using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class ExtendedStatisticsAggregation : StatisticsAggregation
    {
        [JsonProperty("sum_of_squares")]
        public Double SumOfSquares { get; set; }

        [JsonProperty("variance")]
        public Double Variance { get; set; }

        [JsonProperty("std_deviation")]
        public Double StandardDeviation { get; set; }
    }
}
