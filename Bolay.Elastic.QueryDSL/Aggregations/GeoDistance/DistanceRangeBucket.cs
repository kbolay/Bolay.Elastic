using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.GeoDistance
{
    public class DistanceRangeBucket
    {
        public Double To { get; set; }
        public Double From { get; set; }
    }
}
