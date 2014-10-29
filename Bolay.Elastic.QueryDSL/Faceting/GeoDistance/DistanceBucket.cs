using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.GeoDistance
{
    public class DistanceBucket
    {
        public Double GreaterThan { get; set; }
        public Double LessThan { get; set; }
        public Double GreaterThanOrEqualTo { get; set; }
        public Double LessThanOrEqualTo { get; set; }
    }
}
