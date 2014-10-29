using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Statistics
{
    /// <summary>
    /// The response of a statistical facet.
    /// </summary>
    [JsonConverter(typeof(StatisticsResponseSerializer))]
    public class StatisticsResponse : IFacetResponse
    {
        /// <summary>
        /// Gets the name of the facet this is in response to.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the minimum values for this field.
        /// </summary>
        public Double Minimum { get; private set; }

        /// <summary>
        /// Gets the maximum values for this field.
        /// </summary>
        public Double Maximum { get; private set; }

        /// <summary>
        /// Get the sum of the values for this field.
        /// </summary>
        public Double Sum { get; private set; }

        /// <summary>
        /// Gets the average value for this field.
        /// </summary>
        public Double Average { get; private set; }

        /// <summary>
        /// Gets the total number of documents that have this field.
        /// </summary>
        public Int64 Count { get; private set; }

        /// <summary>
        /// Gets the sum of the squares for this field.
        /// </summary>
        public Double SumOfSquares { get; private set; }

        /// <summary>
        /// Gets the variance of this field.
        /// </summary>
        public Double Variance { get; private set; }

        /// <summary>
        /// Gets the standard deviation for this field.
        /// </summary>
        public Double StandardDeviation { get; private set; }

        /// <summary>
        /// Create a statistics facet response.
        /// </summary>
        /// <param name="minimum">Sets the minimum value of the field.</param>
        /// <param name="maximum">Sets the maximum value of the field.</param>
        /// <param name="sum">Sets the sum value of the field.</param>
        /// <param name="average">Sets the average value of the field.</param>
        /// <param name="sumOfSquares">Sets the sum of squares value of the field.</param>
        /// <param name="variance">Sets the variance value of the field.</param>
        /// <param name="standardDeviation">Sets the standard deviation of the field.</param>
        /// <param name="count">Sets the number of the documents with this field.</param>
        internal StatisticsResponse(string name, Double minimum, Double maximum, Double sum, Double average, Double sumOfSquares, Double variance, Double standardDeviation, Int64 count)
        {
            Name = name;
            Minimum = minimum;
            Maximum = maximum;
            Sum = sum;
            Average = average;
            SumOfSquares = sumOfSquares;
            Variance = variance;
            StandardDeviation = standardDeviation;
            Count = count;
        }
    }
}
