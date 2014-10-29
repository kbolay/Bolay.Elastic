using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Common
{
    [JsonConverter(typeof(CommonSerializer))]
    public class CommonQuery : QueryBase
    {
        /// <summary>
        /// Terms to look for in the document.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// The cutoff point between high and low frequency tokens.
        /// </summary>
        public Double CutOffFrequency { get; set; }

        /// <summary>
        /// The operator to use for high frequency tokens.
        /// </summary>
        public OperatorEnum HighFrequencyOperator { get; set; }

        /// <summary>
        /// The operator to use for low frequency tokens.
        /// </summary>
        public OperatorEnum LowFrequencyOperator { get; set; }

        /// <summary>
        /// The minimum should match value for high and low frequency.
        /// </summary>
        public MinimumShouldMatch MinimumShouldMatch { get; set; }

        /// <summary>
        /// For serialization.
        /// </summary>
        internal CommonQuery() 
        {
            CutOffFrequency = CommonSerializer._CUTOFF_FREQUENCY_DEFAULT;
            HighFrequencyOperator = CommonSerializer._HIGH_FREQUENCY_OPERATOR_DEFAULT;
            LowFrequencyOperator = CommonSerializer._LOW_FREQUENCY_OPERATOR_DEFAULT;
        }

        /// <summary>
        /// Create a common query.
        /// </summary>
        /// <param name="query">The values to search for.</param>
        /// <param name="cutOffFrequency">The frequency of a tokens appearance in the index. 
        /// Tokens from the query that exist more often than the frequency will be considered high frequency.
        /// Tokens from the query that exist less often than the frequency will be considered low frequency.
        /// </param>
        public CommonQuery(string query, Double cutOffFrequency = 0.001) : this()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "CommonQuery requires a query.");
            if (cutOffFrequency <= 0 || cutOffFrequency > 1)
                throw new ArgumentOutOfRangeException("cutOffFrequency", "CommonQuery requires a cutoff frequency greater than 0 and less than or equal to 1.");

            Query = query;
            CutOffFrequency = cutOffFrequency;
        }
    }
}
