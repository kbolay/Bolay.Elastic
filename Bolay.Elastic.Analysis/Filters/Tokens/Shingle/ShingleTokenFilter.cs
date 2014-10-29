using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Shingle
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-shingle-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(ShingleTokenFilterSerializer))]
    public class ShingleTokenFilter : TokenFilterBase
    {
        internal const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        internal const Int64 _MAXIMUM_SIZE_DEFAULT = 2;
        internal const bool _OUTPUT_UNIGRAMS_DEFAULT = true;
        internal const bool _OUTPUT_UNIGRAMS_IF_NO_SHINGLES_DEFAULT = false;
        internal const string _TOKEN_SEPARATOR_DEFAULT = " ";
        internal const string _FILLER_TOKEN_DEFAULT = "_";

        private Int64 _MinimumSize { get; set; }
        private Int64 _MaximumSize { get; set; }

        /// <summary>
        /// Gets or sets minimum size of a shingle.
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
        /// Gets or sets the maximum size of a shingle.
        /// Defaults to 2.
        /// </summary>
        public Int64 MaximumSize
        {
            get { return _MaximumSize; }
            set
            {
                if (value <= 0 || value < _MinimumSize)
                    throw new ArgumentOutOfRangeException("MaximumSize", "Must be greater than or equal to the minimum value.");

                _MaximumSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the output unigrams value.
        /// Defaults to false.
        /// </summary>
        public bool OutputUnigrams { get; set; }

        /// <summary>
        /// Gets or sets whether the output will contain the input tokens if noshingles are available. If OutputUnigrams is set to true this will have no effect. 
        /// Defaults to false.
        /// </summary>
        public bool OutputUnigramsIfNoShingles { get; set; }

        /// <summary>
        /// Gets or sets the token separator.
        /// Defaults to ' '.
        /// </summary>
        public string TokenSeparator { get; set; }

        /// <summary>
        /// Gets or sets the filler token.
        /// Defaults to '_'.
        /// </summary>
        public string FillerToken { get; set; }

        /// <summary>
        /// Creates a shingle token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public ShingleTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.Shingle) 
        {
            MinimumSize = _MINIMUM_SIZE_DEFAULT;
            MaximumSize = _MAXIMUM_SIZE_DEFAULT;
            OutputUnigrams = _OUTPUT_UNIGRAMS_DEFAULT;
            OutputUnigramsIfNoShingles = _OUTPUT_UNIGRAMS_IF_NO_SHINGLES_DEFAULT;
            TokenSeparator = _TOKEN_SEPARATOR_DEFAULT;
            FillerToken = _FILLER_TOKEN_DEFAULT;
        }
    }
}
