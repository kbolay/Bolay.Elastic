using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Range
{
    public class DoubleRangeFilter : RangeFilterBase
    {
        private Double? _GreaterThan { get; set; }
        private Double? _LessThan { get; set; }
        private Double? _GreaterThanOrEqualTo { get; set; }
        private Double? _LessThanOrEqualTo { get; set; }

        /// <summary>
        /// Gets the greater than value of the range filter.
        /// </summary>
        public override object GreaterThan
        {
            get { return _GreaterThan; }
        }

        /// <summary>
        /// Gets the less than value of the range filter.
        /// </summary>
        public override object LessThan
        {
            get { return _LessThan; }
        }

        /// <summary>
        /// Gets the greater than or equal to value of the range filter.
        /// </summary>
        public override object GreaterThanOrEqualTo
        {
            get { return _GreaterThanOrEqualTo; }
        }

        /// <summary>
        /// Gets the less than or equal to value of the range filter.
        /// </summary>
        public override object LessThanOrEqualTo
        {
            get { return _LessThanOrEqualTo; }
        }

        /// <summary>
        /// Create a range filter based on Double values.
        /// </summary>
        /// <param name="field">The field to filter against.</param>
        /// <param name="greaterThan">The smallest Double of the range.</param>
        /// <param name="lessThan">The largest Double of the range.</param>
        /// <param name="greaterThanOrEqualTo">The smallest Double of the range including the value.</param>
        /// <param name="lessThanOrEqualTo">The largest Double of the range including the value.</param>
        public DoubleRangeFilter(string field, Double? greaterThan = null, Double? lessThan = null, Double? greaterThanOrEqualTo = null, Double? lessThanOrEqualTo = null)
            : base(field) 
        {
            if (greaterThan == null && lessThan == null && greaterThanOrEqualTo == null && lessThanOrEqualTo == null)
                throw new ArgumentNullException("range", "DoubleRangeFilter requires at least one of the optional paramters.");

            _GreaterThan = greaterThan;
            _LessThan = lessThan;
            _GreaterThanOrEqualTo = greaterThanOrEqualTo;
            _LessThanOrEqualTo = lessThanOrEqualTo;
        }
    }
}
