using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    public class MultipleCombinationMatch : MinimumShouldMatchBase
    {
        internal const string _DELIMITER = " ";

        private IEnumerable<CombinationMatch> _CombinationMatches { get; set; }

        /// <summary>
        /// Create a minimum should match value using multiple combination matches.
        /// </summary>
        /// <param name="combinationMatches">The combination matches to combine.</param>
        public MultipleCombinationMatch(IEnumerable<CombinationMatch> combinationMatches)
        {
            if (combinationMatches == null || !combinationMatches.Any())
                throw new ArgumentNullException("combinationMatches", "MultipleCombinationMatch requires a collection of combination matches.");
            if (combinationMatches.Count() > 1)
                throw new ArgumentNullException("combinationMatches", "Use CombinationMatch if only submitting a collection of one.");

            _CombinationMatches = combinationMatches;
        }
    
        public override object GetValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (CombinationMatch match in _CombinationMatches)
            {
                if (builder.Length > 0)
                    builder.Append(_DELIMITER);

                builder.Append(match.ToString());
            }

            return builder.ToString();
        }
    }
}
