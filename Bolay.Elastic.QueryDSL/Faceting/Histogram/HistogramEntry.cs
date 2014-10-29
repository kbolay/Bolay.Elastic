using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Histogram
{
    public class HistogramEntry
    {
        /// <summary>
        /// Gets the key for the entry.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Get number of documents sharing the value.
        /// </summary>
        public Int64 Count { get; private set; }

        internal HistogramEntry(object value, Int64 count)
        {
            Value = value;
            Count = count;
        }
    }
}
