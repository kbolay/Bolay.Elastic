using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class CompoundWord : TokenFilterBase
    {
        private const int _MINIMUM_WORD_SIZE_DEFAULT = 5;
        private const int _MINIMUM_SUBWORD_SIZE_DEFAULT = 2;
        private const int _MAXIMUM_SUBWORD_SIZE_DEFAULT = 15;

        private DecompounderEnum _Type { get; set; }
        private int? _MinimumWordSize { get; set; }
        private int? _MinimumSubWordSize { get; set; }
        private int? _MaximumSubWordSize { get; set; }

        /// <summary>
        /// Expects the string value from a DecompounderEnum
        /// </summary>
        [JsonProperty("type")]
        [DefaultValue(default(string))]
        new public string Type 
        {
            get
            {
                if (_Type != null)
                    return _Type.ToString();
                return default(string);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    DecompounderEnum decom = DecompounderEnum.Dictionary;
                    decom = DecompounderEnum.Find(value);
                    if (decom != null)
                        _Type = decom;
                    else
                        throw new ArgumentOutOfRangeException("Type", value + " not a valid decompounder.");
                }
                else
                    throw new ArgumentNullException("Type", "Decompounder expected.");
            }
        }

        [JsonProperty("word_list")]
        [DefaultValue(default(List<string>))]
        public List<string> WordList { get; set; }

        [JsonProperty("word_list_path")]
        [DefaultValue(default(string))]
        public string WordListPath { get; set; }

        [JsonProperty("min_word_size")]
        [DefaultValue(_MINIMUM_WORD_SIZE_DEFAULT)]
        public int MinimumWordSize 
        {
            get
            {
                if (_MinimumWordSize.HasValue)
                    return _MinimumWordSize.Value;
                return _MINIMUM_WORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumWordSize", "Must be greater than zero.");

                _MinimumWordSize = value;
            }
        }

        [JsonProperty("min_subword_size")]
        [DefaultValue(_MINIMUM_SUBWORD_SIZE_DEFAULT)]
        public int MinimumSubWordSize
        {
            get
            {
                if (_MinimumSubWordSize.HasValue)
                    return _MinimumSubWordSize.Value;
                return _MINIMUM_SUBWORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSubWordSize", "Must be greater than zero.");

                _MinimumSubWordSize = value;
            }
        }

        [JsonProperty("max_subword_size")]
        [DefaultValue(_MAXIMUM_SUBWORD_SIZE_DEFAULT)]
        public int MaximumSubWordSize
        {
            get
            {
                if (_MaximumSubWordSize.HasValue)
                    return _MaximumSubWordSize.Value;
                return _MAXIMUM_SUBWORD_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumSubWordSize", "Must be greater than zero.");

                _MaximumSubWordSize = value;
            }
        }

        [JsonProperty("only_longest_match")]
        [DefaultValue(default(bool))]
        public bool OnlyLongestMatch { get; set; }

        public CompoundWord() : base(TokenFilterEnum.CompoundWord) { }
    }
}
