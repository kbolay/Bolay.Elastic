using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.DateHistogram
{
    public class DateHistogramEntry
    {
        internal static DateTime _EPOCH = new DateTime(1970, 1, 1);

        /// <summary>
        /// Gets the DateTime value for the DateHistogram entry.
        /// </summary>
        public DateTime Value { get; private set; }

        /// <summary>
        /// Gets the number of documents that are within the interval and value.
        /// </summary>
        public Int64 Count { get; private set; }

        internal DateHistogramEntry(Int64 epochDateTime, Int64 count)
        {
            Value = _EPOCH.AddMilliseconds(epochDateTime);
            Count = count;
        }
    }
}
