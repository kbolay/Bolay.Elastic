using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    public class PercentageMatch : SingleValueMatchBase
    {
        internal const string _PERCENTAGE = "%";

        private Double _Percent { get; set; }

        /// <summary>
        /// Create a minimum should match value based on percentages.
        /// </summary>
        /// <param name="percent">The value should be between -1.0 and 1.0.</param>
        public PercentageMatch(Double percent)
        {
            if (percent < -1 || percent > 1)
                throw new ArgumentOutOfRangeException("percent", "PercentageMatch requires the value for a percentage to be between -1.0 and 1.0.");

            _Percent = percent;
        }

        public override object GetValue()
        {
            return (_Percent * 100).ToString() + _PERCENTAGE;
        }
    }
}
