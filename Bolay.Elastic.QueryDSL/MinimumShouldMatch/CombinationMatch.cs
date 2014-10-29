using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    public class CombinationMatch : MinimumShouldMatchBase
    {
        internal const string _DELIMITER = "<";

        private int _MinimumShouldMatch { get; set; }
        private SingleValueMatchBase _Value { get; set; }

        /// <summary>
        /// Create a combination match based on an integer minimum should match value.
        /// </summary>
        /// <param name="minimumShouldMatch">The minimum number of optional clauses that must match.</param>
        /// <param name="value">The integer match value.</param>
        public CombinationMatch(int minimumShouldMatch, IntegerMatch value)
        {
            if (minimumShouldMatch <= 0)
            {
                throw new ArgumentOutOfRangeException("minimumShouldMatch", "CombinationMatch expects the minimumShouldMatch value to be a positive integer.");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value", "CombinationMatch integer match must be populated.");
            }

            _MinimumShouldMatch = minimumShouldMatch;
            _Value = value;
        }

        /// <summary>
        /// Create a combination match based on a percentation minimum should match value.
        /// </summary>
        /// <param name="minimumShouldMatch">The minimum number of optional clauses that must match.</param>
        /// <param name="value">The percentage match value.</param>
        public CombinationMatch(int minimumShouldMatch, PercentageMatch value)
        {
            if (minimumShouldMatch <= 0)
            {
                throw new ArgumentOutOfRangeException("minimumShouldMatch", "CombinationMatch expects the minimumShouldMatch value to be a positive integer.");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value", "CombinationMatch percentage match must be populated.");
            }

            _MinimumShouldMatch = minimumShouldMatch;
            _Value = value;
        }

        public override object GetValue()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_MinimumShouldMatch);
            builder.Append(_DELIMITER);
            builder.Append(_Value.GetValue());

            return builder.ToString();
        }
    }
}
