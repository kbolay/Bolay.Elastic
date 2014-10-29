using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    public class TermBucket
    {
        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the number of documents in which this term appears.
        /// </summary>
        public Int64 Count { get; private set; }

        /// <summary>
        /// Create a term bucket.
        /// </summary>
        /// <param name="value">Sets the value of the term.</param>
        /// <param name="count">Sets the number of documents this term is found in.</param>
        internal TermBucket(object value, Int64 count)
        {
            Value = value;
            Count = count;
        }
    }
}
