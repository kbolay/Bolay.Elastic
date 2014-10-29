using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Shingle : TokenFilterBase
    {
        private const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        private const Int64 _MAXIMUM_SIZE_DEFAULT = 2;
        private const bool _OUTPUT_UNIGRAMS_DEFAULT = true;
        private const string _TOKEN_SEPARATOR_DEFAULT = " ";

        private Int64? _MinimumSize { get; set; }
        private Int64? _MaximumSize { get; set; }
        private bool? _OutputUnigrams { get; set; }
        private bool _OutputUnigramsIfNoShingles { get; set; }
        private string _TokenSeparator { get; set; }

        [JsonProperty("min_gram")]
        [DefaultValue(_MINIMUM_SIZE_DEFAULT)]
        public Int64 MinimumSize
        {
            get
            {
                if (_MinimumSize.HasValue)
                    return _MinimumSize.Value;
                return _MINIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSize", "Must be greater than zero.");

                _MinimumSize = value;
            }
        }

        [JsonProperty("max_gram")]
        [DefaultValue(_MAXIMUM_SIZE_DEFAULT)]
        public Int64 MaximumSize
        {
            get
            {
                if (_MaximumSize.HasValue)
                    return _MaximumSize.Value;
                return _MAXIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumSize", "Must be greater than zero.");

                _MaximumSize = value;
            }
        }

        [JsonProperty("output_unigrams")]
        [DefaultValue(_OUTPUT_UNIGRAMS_DEFAULT)]
        public bool OutputUnigrams
        {
            get
            {
                if (_OutputUnigrams.HasValue)
                    return _OutputUnigrams.Value;
                return _OUTPUT_UNIGRAMS_DEFAULT;
            }
            set
            {
                _OutputUnigrams = value;
            }
        }

        [JsonProperty("output_unigrams_if_no_shingles")]
        [DefaultValue(default(bool))]
        public bool OutputUnigramsIfNoShingles
        {
            get
            {
                if (!_OutputUnigramsIfNoShingles || OutputUnigrams)
                    return default(bool);

                return _OutputUnigramsIfNoShingles;
            }
            set
            {
                _OutputUnigramsIfNoShingles = value;
            }
        }

        [JsonProperty("token_separator")]
        [DefaultValue(_TOKEN_SEPARATOR_DEFAULT)]
        public string TokenSeparator
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_TokenSeparator))
                    return _TokenSeparator;

                return _TOKEN_SEPARATOR_DEFAULT;
            }
            set
            {
                _TokenSeparator = value;
            }
        }

        public Shingle() : base(TokenFilterEnum.Shingle) { }
    }
}
