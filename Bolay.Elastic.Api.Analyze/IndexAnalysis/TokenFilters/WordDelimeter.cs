using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class WordDelimeter : TokenFilterBase
    {
        private const bool _GENERATE_WORD_PARTS_DEFAULT = true;
        private const bool _GENERATE_NUMBER_PARTS_DEFAULT = true;
        private const bool _SPLIT_ON_CASE_CHANGE_DEFAULT = true;
        private const bool _SPLIT_ON_NUMERICS_DEFAULT = true;
        private const bool _STEM_ENGLISH_POSSESSIVE_DEFAULT = true;

        private bool? _GenerateWordParts { get; set; }
        private bool? _GenerateNumberParts { get; set; }
        private bool? _SplitOnCaseChange { get; set; }
        private bool? _SplitOnNumerics { get; set; }
        private bool? _StemEnglishPossessive { get; set; }

        [JsonProperty("generate_word_parts")]
        [DefaultValue(_GENERATE_WORD_PARTS_DEFAULT)]
        public bool GenerateWordParts 
        {
            get
            {
                if (_GenerateWordParts.HasValue)
                    return _GenerateWordParts.Value;
                return _GENERATE_WORD_PARTS_DEFAULT;
            }
            set
            {
                _GenerateWordParts = value;
            }
        }

        [JsonProperty("generate_number_parts")]
        [DefaultValue(_GENERATE_NUMBER_PARTS_DEFAULT)]
        public bool GenerateNumberParts
        {
            get
            {
                if (_GenerateNumberParts.HasValue)
                    return _GenerateNumberParts.Value;
                return _GENERATE_NUMBER_PARTS_DEFAULT;
            }
            set
            {
                _GenerateNumberParts = value;
            }
        }

        [JsonProperty("catenate_words")]
        [DefaultValue(default(bool))]
        public bool CatenateWords { get; set; }

        [JsonProperty("catenate_numbers")]
        [DefaultValue(default(bool))]
        public bool CatenateNumbers { get; set; }

        [JsonProperty("catenate_all")]
        [DefaultValue(default(bool))]
        public bool CatenateAll { get; set; }

        [JsonProperty("split_on_case_change")]
        [DefaultValue(_SPLIT_ON_CASE_CHANGE_DEFAULT)]
        public bool SplitOnCaseChange 
        {
            get
            {
                if (_SplitOnCaseChange.HasValue)
                    return _SplitOnCaseChange.Value;
                return _SPLIT_ON_CASE_CHANGE_DEFAULT;
            }
            set
            {
                _SplitOnCaseChange = _SPLIT_ON_CASE_CHANGE_DEFAULT;
            }
        }

        [JsonProperty("preserve_original")]
        [DefaultValue(default(bool))]
        public bool PreserveOriginal { get; set; }

        [JsonProperty("split_on_numerics")]
        [DefaultValue(_SPLIT_ON_NUMERICS_DEFAULT)]
        public bool SplitOnNumerics
        {
            get
            {
                if (_SplitOnNumerics.HasValue)
                    return _SplitOnNumerics.Value;
                return _SPLIT_ON_NUMERICS_DEFAULT;
            }
            set
            {
                _SplitOnNumerics = value;
            }
        }

        [JsonProperty("stem_english_possessive")]
        [DefaultValue(_STEM_ENGLISH_POSSESSIVE_DEFAULT)]
        public bool StemEnglishPossessive
        {
            get
            {
                if (_StemEnglishPossessive.HasValue)
                    return _StemEnglishPossessive.Value;
                return _STEM_ENGLISH_POSSESSIVE_DEFAULT;
            }
            set
            {
                _StemEnglishPossessive = value;
            }
        }

        [JsonProperty("protected_words")]
        [DefaultValue(default(List<string>))]
        public List<string> ProtectedWords { get; set; }

        [JsonProperty("protected_words_path")]
        [DefaultValue(default(string))]
        public string ProtectedWordPath { get; set; }

        [JsonProperty("type_table")]
        [DefaultValue(default(string))]
        public string TypeTable { get; set; }

        public WordDelimeter() : base(TokenFilterEnum.WordDelimeter) { }
    }
}
