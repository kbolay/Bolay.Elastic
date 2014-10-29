using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    public class FuzzyStringQuery : FuzzyQueryBase
    {
        private string _Value { get; set; }
        private int? _Fuzziness { get; set; }

        public override object Value
        {
            get { return _Value; }
        }

        public override object Fuzziness
        {
            get { return _Fuzziness; }
        }

        public FuzzyStringQuery(string field, string value) : this(field, value, null) { }
        public FuzzyStringQuery(string field, string value, int? fuzziness) 
            : base(field)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "FuzzyStringQuery requires a value.");

            if (fuzziness.HasValue && fuzziness.Value < 0)
                throw new ArgumentOutOfRangeException("fuzziness", "Fuzziness must be greater than or equal to zero.");

            _Value = value;
            _Fuzziness = fuzziness;
        }
    }
}
