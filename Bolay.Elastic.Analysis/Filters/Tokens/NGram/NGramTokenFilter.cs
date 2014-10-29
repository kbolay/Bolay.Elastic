using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.NGram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-ngram-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(NGramTokenFilterSerializer))]
    public class NGramTokenFilter : TokenFilterBase
    {
        internal const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        internal const Int64 _MAXIMUM_SIZE_DEFAULT = 2;

        private Int64 _MinimumSize { get; set; }
        private Int64 _MaximumSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of an ngram.
        /// Defaults to 1.
        /// </summary>
        public Int64 MinimumSize
        {
            get { return _MinimumSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSize", "Must be greater than zero.");
                
                _MinimumSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum size of an ngram.
        /// Defaults to 2.
        /// </summary>
        public Int64 MaximumSize
        {
            get { return _MaximumSize; }
            set
            {
                if (value <= 0 || value < MinimumSize)
                    throw new ArgumentOutOfRangeException("MaximumSize", "Must be greater than or equal to the minimum size.");

                _MaximumSize = value;
            }
        }

        /// <summary>
        /// Creates an ngram token filter.
        /// </summary>
        /// <param name="name">Sets the name of a token filter.</param>
        public NGramTokenFilter(string name) : base(name, TokenFilterTypeEnum.Ngram) 
        {
            _MaximumSize = _MAXIMUM_SIZE_DEFAULT;
            _MinimumSize = _MINIMUM_SIZE_DEFAULT;
        }
    }
}
