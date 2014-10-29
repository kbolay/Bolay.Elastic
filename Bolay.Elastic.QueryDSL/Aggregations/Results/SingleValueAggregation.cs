using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Results
{
    public class SingleValueAggregation : AggregationResultBase
    {
        public object Value { get; set; }

        public string ValueAsString { get; set; }
    }
}
