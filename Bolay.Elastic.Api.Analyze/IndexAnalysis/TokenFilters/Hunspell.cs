using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Hunspell : TokenFilterBase
    {
        private const bool _DEDUP_DEFAULT = true;
        private const int _RECURSION_LEVEL_DEFAULT = 2;

        private bool? _Dedup { get; set; }
        private int? _RecursionLevel { get; set; }

        [JsonProperty("locale")]
        [DefaultValue(default(string))]
        public string Locale { get; set; }

        [JsonProperty("language")]
        [DefaultValue(default(string))]
        public string Language { get; set; }

        [JsonProperty("dictionary")]
        [DefaultValue(default(string))]
        public string Dictionary { get; set; }

        [JsonProperty("dedup")]
        [DefaultValue(_DEDUP_DEFAULT)]
        public bool Dedup 
        {
            get
            {
                if (_Dedup.HasValue)
                    return _Dedup.Value;
                return _DEDUP_DEFAULT;
            }
            set
            {
                _Dedup = value;
            }
        }

        [JsonProperty("recursion_level")]
        [DefaultValue(_RECURSION_LEVEL_DEFAULT)]
        public int RecursionLevel 
        {
            get
            {
                if (_RecursionLevel.HasValue)
                    return _RecursionLevel.Value;
                return _RECURSION_LEVEL_DEFAULT;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("RecursionLevel", "Must be greater than or equal to zero.");
                _RecursionLevel = value;
            }
        }

        public Hunspell() : base(TokenFilterEnum.Hunspell) { }
    }
}