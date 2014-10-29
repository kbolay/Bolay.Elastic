using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.MinimumShouldMatch
{
    public class IntegerMatch : SingleValueMatchBase
    {
        private int _Value { get; set; }

        /// <summary>
        /// Create a minimum should match integer value.
        /// </summary>
        /// <param name="value">This value can be positive or negative.</param>
        public IntegerMatch(int value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException("value", "IntegerMatch requires a positive or negative value.");
            _Value = value;
        }

        public override object GetValue()
        {
            return _Value;
        }
    }
}
