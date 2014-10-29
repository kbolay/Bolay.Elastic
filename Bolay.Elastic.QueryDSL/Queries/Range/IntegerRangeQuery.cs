using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Range
{
    public class IntegerRangeQuery : RangeQueryBase
    {
        private Int64? _GreaterThan { get; set; }
        private Int64? _LessThan { get; set; }
        private Int64? _GreaterThanOrEqualTo { get; set; }
        private Int64? _LessThanOrEqualTo { get; set; }

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
        /// Create a range query based on Int64/Int32 values.
        /// </summary>
        /// <param name="field">The field to query against.</param>
        /// <param name="greaterThan">The smallest Int64/Int32 of the range.</param>
        /// <param name="lessThan">The largest Int64/Int32 of the range.</param>
        /// <param name="greaterThanOrEqualTo">The smallest Int64/Int32 of the range including the value.</param>
        /// <param name="lessThanOrEqualTo">The largest Int64/Int32 of the range including the value.</param>
        public IntegerRangeQuery(string field, Int64? greaterThan = null, Int64? lessThan = null, Int64? greaterThanOrEqualTo = null, Int64? lessThanOrEqualTo = null) : base(field) 
        {
            if (greaterThan == null && lessThan == null && greaterThanOrEqualTo == null && lessThanOrEqualTo == null)
                throw new ArgumentNullException("range", "IntegerRangeQuery requires at least one of the optional paramters.");

            _GreaterThan = greaterThan;
            _LessThan = lessThan;
            _GreaterThanOrEqualTo = greaterThanOrEqualTo;
            _LessThanOrEqualTo = lessThanOrEqualTo;
        }
    }
}
