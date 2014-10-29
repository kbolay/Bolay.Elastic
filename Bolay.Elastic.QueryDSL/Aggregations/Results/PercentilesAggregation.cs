using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class PercentilesAggregation : AggregationResultBase
    {
        /// <summary>
        /// Gets the percentiles.
        /// </summary>
        public Dictionary<Double, Int64> Percentiles { get; private set; }

        internal PercentilesAggregation(string name, Dictionary<string, object> percentiles)
        { 
            Name = name;

            if(percentiles != null && percentiles.Any())
            {
                Percentiles = new Dictionary<Double,Int64>();
                foreach (KeyValuePair<string, object> kvp in percentiles)
                {
                    Percentiles.Add(Convert.ToDouble(kvp.Key), Convert.ToInt64(percentiles));
                }
            }
        }
    }
}
