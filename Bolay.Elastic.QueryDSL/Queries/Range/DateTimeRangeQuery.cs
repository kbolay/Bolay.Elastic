using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Range
{
    public class DateTimeRangeQuery : RangeQueryBase
    {
        private const string _DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        private DateTime? _GreaterThan { get; set; }
        private DateTime? _LessThan { get; set; }
        private DateTime? _GreaterThanOrEqualTo { get; set; }
        private DateTime? _LessThanOrEqualTo { get; set; }

        /// <summary>
        /// Gets the greater than value of the range query.
        /// </summary>
        public override object GreaterThan
        {
            get
            {
                if (!_GreaterThan.HasValue)
                    return null;
                return _GreaterThan.Value.ToString(_DATE_TIME_FORMAT);
            }
        }

        /// <summary>
        /// Gets the less than value of the range query.
        /// </summary>
        public override object LessThan
        {
            get
            {
                if (!_LessThan.HasValue)
                    return null;
                return _LessThan.Value.ToString(_DATE_TIME_FORMAT);
            }
        }

        /// <summary>
        /// Gets the greater than or equal to value of the range query.
        /// </summary>
        public override object GreaterThanOrEqualTo
        {
            get
            {
                if (!_GreaterThanOrEqualTo.HasValue)
                    return null;
                return _GreaterThanOrEqualTo.Value.ToString(_DATE_TIME_FORMAT);
            }
        }

        /// <summary>
        /// Gets the less than or equal to value of the range query.
        /// </summary>
        public override object LessThanOrEqualTo
        {
            get
            {
                if (!_LessThanOrEqualTo.HasValue)
                    return null;
                return _LessThanOrEqualTo.Value.ToString(_DATE_TIME_FORMAT);
            }
        }

        /// <summary>
        /// Create a range query based on DateTime values.
        /// </summary>
        /// <param name="field">The field to query against.</param>
        /// <param name="greaterThan">The starting DateTime of the range.</param>
        /// <param name="lessThan">The ending DateTime of the range.</param>
        /// <param name="greaterThanOrEqualTo">The starting DateTime of the range including the value.</param>
        /// <param name="lessThanOrEqualTo">The ending DateTime of the range including the value.</param>
        public DateTimeRangeQuery(string field, DateTime? greaterThan = null, DateTime? lessThan = null, DateTime? greaterThanOrEqualTo = null, DateTime? lessThanOrEqualTo = null)
            : base(field) 
        {
            if (greaterThan == null && lessThan == null && greaterThanOrEqualTo == null && lessThanOrEqualTo == null)
                throw new ArgumentNullException("range", "DateTimeRangeQuery requires at least one of the optional paramters.");

            _GreaterThan = greaterThan;
            _LessThan = lessThan;
            _GreaterThanOrEqualTo = greaterThanOrEqualTo;
            _LessThanOrEqualTo = lessThanOrEqualTo;
        }
    }
}
