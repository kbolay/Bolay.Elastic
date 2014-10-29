using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Length
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-length-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(LengthTokenFilterSerializer))]
    public class LengthTokenFilter : TokenFilterBase
    {
        internal const Int32 _MINIMUM_DEFAULT = 0;
        internal const Int32 _MAXIMUM_DEFAULT = Int32.MaxValue;

        private Int32 _Minimum { get; set; }
        private Int32 _Maximum { get; set; }

        /// <summary>
        /// Gets or sets minimum length of a token.
        /// </summary>
        public Int32 Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Minimum", "Must be greater than or equal to zero.");

                _Minimum = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum length of a token.
        /// </summary>
        public Int32 Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value <= 0 || value < Minimum)
                    throw new ArgumentOutOfRangeException("Maximum", "Must be greater than or equal to minimum.");

                _Maximum = value;
            }
        }

        /// <summary>
        /// Creates a length token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public LengthTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.Length) 
        {
            _Minimum = _MINIMUM_DEFAULT;
            _Maximum = _MAXIMUM_DEFAULT;
        }
    }
}
