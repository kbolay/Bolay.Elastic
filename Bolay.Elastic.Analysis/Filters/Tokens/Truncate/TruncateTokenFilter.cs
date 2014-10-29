using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Truncate
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-truncate-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(TruncateTokenFilterSerializer))]
    public class TruncateTokenFilter : TokenFilterBase
    {
        internal const int _LENGTH_DEFAULT = 10;

        private int _Length { get; set; }

        /// <summary>
        /// Gets or sets the length of characters to truncate to.
        /// </summary>
        public int Length
        {
            get { return _Length; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Length", "Must be greater than zero.");

                _Length = value;
            }
        }

        /// <summary>
        /// Create a truncate token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public TruncateTokenFilter(string name) : base(name, TokenFilterTypeEnum.Truncate) 
        {
            Length = _LENGTH_DEFAULT;
        }
    }
}
