using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    public class TermStatisticBucket
    {
        /// <summary>
        /// Gets the term.
        /// </summary>
        public object Term { get; private set; }

        /// <summary>
        /// Gets the minimum values produced by a document in this bucket.
        /// </summary>
        public Double Minimum { get; private set; }

        /// <summary>
        /// Gets the maximum values produced by a document in this bucket.
        /// </summary>
        public Double Maximum { get; private set; }        

        /// <summary>
        /// Get the sum of the values produced by the documents in this bucket.
        /// </summary>
        public Double Sum { get; private set; }

        /// <summary>
        /// Gets the average values produced by documents in this bucket.
        /// </summary>
        public Double Average { get; private set; }

        /// <summary>
        /// Gets the total number of documents that belong in this range bucket.
        /// </summary>
        public Int64 Count { get; private set; }

        /// <summary>
        /// Gets the total number of documents belonging in this bucket.
        /// </summary>
        public Int64 TotalCount { get; private set; }

        /// <summary>
        /// Creates a range for the range facet response.
        /// </summary>
        /// <param name="term">Sets the term for this bucket.</param>
        /// <param name="minimum">Sets the minimum value found in this bucket.</param>
        /// <param name="maximum">Sets the maximum value found in this bucket.</param>
        /// <param name="sum">Sets the sum of the value found in this bucket.</param>
        /// <param name="average">Sets the average of the value found in this bucket.</param>
        /// <param name="count">Sets the count of the documents in the bucket.</param>
        /// <param name="totalCount">Sets the total count of the documents in this bucket.</param>
        internal TermStatisticBucket(object term, Double minimum, Double maximum, Double sum, Double average, Int64 count, Int64 totalCount)
        {
            Term = term;
            Minimum = minimum;
            Maximum = maximum;
            Sum = sum;
            Average = average;
            Count = count;
            TotalCount = totalCount;
        }
    }
}
