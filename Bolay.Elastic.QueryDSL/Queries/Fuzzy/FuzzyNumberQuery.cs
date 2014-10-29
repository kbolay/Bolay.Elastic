using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Fuzzy
{
    public class FuzzyNumberQuery : FuzzyQueryBase
    {
        private bool _IsInteger { get; set; }

        private Int64 _IntegerValue { get; set; }
        private Double _DoubleValue { get; set; }
        private Int64? _IntegerFuzziness { get; set; }
        private Double? _DoubleFuzziness { get; set; }

        public override object Value
        {
            get 
            { 
                if(_IsInteger)
                    return _IntegerValue;

                return _DoubleValue;
            }
        }

        public override object Fuzziness
        {
            get 
            {
                if (_IsInteger)
                    return _IntegerFuzziness;

                return _DoubleFuzziness; 
            }
        }

        /// <summary>
        /// Create a fuzzy query against a double or float field using the default fuzziness (AUTO).
        /// </summary>
        /// <param name="field">The numerical field to search against.</param>
        /// <param name="value">The value to search for.</param>
        public FuzzyNumberQuery(string field, Double value) : this(field, value, null) { }

        /// <summary>
        /// Create a fuzzy query against a double or float field using a specific fuzziness.
        /// </summary>
        /// <param name="field">The numeric field to search against.</param>
        /// <param name="value">The numeric value to search with.</param>
        /// <param name="fuzziness">The range, above or below the value, that will be considered matching.</param>
        public FuzzyNumberQuery(string field, Double value, Double? fuzziness)
            : base(field)
        {
            _IsInteger = false;
            _DoubleValue = value;

            if (fuzziness.HasValue && fuzziness.Value < 0)
                throw new ArgumentOutOfRangeException("fuzziness", "Fuzziness must be greater than or equal to zero.");

            _DoubleFuzziness = fuzziness;
        }

        /// <summary>
        /// Create a fuzzy query against a integer or long field using the default fuzziness (AUTO).
        /// </summary>
        /// <param name="field">The numerical field to search against.</param>
        /// <param name="value">The value to search for.</param>
        public FuzzyNumberQuery(string field, Int64 value) : this(field, value, null) { }

        /// <summary>
        /// Create a fuzzy query against a integer or long field using a specific fuzziness.
        /// </summary>
        /// <param name="field">The numerical field to search against.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="fuzziness">The range, above or below the value, that will be considered matching.</param>
        public FuzzyNumberQuery(string field, Int64 value, Int64? fuzziness) 
            : base(field)
        {
            _IsInteger = true;
            _IntegerValue = value;

            if (fuzziness.HasValue && fuzziness.Value < 0)
                throw new ArgumentOutOfRangeException("fuzziness", "Fuzziness must be greater than or equal to zero.");

            _IntegerFuzziness = fuzziness;
        }
    }
}
