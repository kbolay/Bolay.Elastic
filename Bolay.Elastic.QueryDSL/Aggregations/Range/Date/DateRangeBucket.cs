using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Range.Date
{
    public class DateRangeBucket
    {
        public object To { get; set; }
        public object From { get; set; }
    }
}
