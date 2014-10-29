using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.EdgeNGram
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-edgengram-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(EdgeNGramTokenFilterSerializer))]
    public class EdgeNGramTokenFilter : TokenFilterBase
    {
        internal const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        internal const Int64 _MAXIMUM_SIZE_DEFAULT = 2;
        internal static SideEnum _SIDE_DEFAULT = SideEnum.Front;

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
                if (value <= 0 || value < _MinimumSize)
                    throw new ArgumentOutOfRangeException("MaximumSize", "Must be greater than or equal to the minimum size.");

                _MaximumSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the side to create ngrams from.
        /// </summary>
        public SideEnum Side { get; set; }

        /// <summary>
        /// Create an edge ngram token filter.
        /// </summary>
        /// <param name="name">Sets the name for the token filter.</param>
        public EdgeNGramTokenFilter(string name) : 
            base(name, TokenFilterTypeEnum.EdgeNGram) 
        {
            _MinimumSize = _MINIMUM_SIZE_DEFAULT;
            _MaximumSize = _MAXIMUM_SIZE_DEFAULT;
            Side = _SIDE_DEFAULT;
        }
    }
}