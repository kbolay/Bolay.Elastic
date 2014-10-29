using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    public class FuzzyDateQuery : FuzzyQueryBase
    {
        private const string _DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        private DateTime _Value { get; set; }
        private TimeValue _Fuzziness { get; set; }

        public override object Value
        {
            get { return _Value.ToString(_DATE_FORMAT); }
        }
        public override object Fuzziness
        {
            get
            {
                if (_Fuzziness == null)
                    return null;
                return _Fuzziness.ToString();
            }
        }

        /// <summary>
        /// Create a fuzzy query against a date field, using the default fuzziness value (AUTO).
        /// </summary>
        /// <param name="field">The name of the field to search against.</param>
        /// <param name="value">The date value to search for.</param>
        public FuzzyDateQuery(string field, DateTime value) : this(field, value, null) { }

        /// <summary>
        /// Create a fuzzy query against a date field, specifying a fuzziness value.
        /// </summary>
        /// <param name="field">The name of the field to search against.</param>
        /// <param name="value">The date value to search for.</param>
        /// <param name="fuzziness">The range of time that will be accepted as a match.</param>
        public FuzzyDateQuery(string field, DateTime value, TimeSpan? fuzziness)
            :base(field)
        {
            if (value == default(DateTime))
                throw new ArgumentNullException("value", "FuzzyDateQuery requires non-default date time.");

            _Value = value;

            if(fuzziness.HasValue)
                _Fuzziness = new TimeValue(fuzziness.Value);
        }
    }
}
