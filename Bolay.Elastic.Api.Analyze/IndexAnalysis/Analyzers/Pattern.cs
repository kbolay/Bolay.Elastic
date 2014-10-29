using Bolay.Elastic.Api.Analyze.Models.IndexAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Pattern
    {
        private const bool _LOWERCASE_DEFAULT = true;
        private const string _REGEX_PATTERN_DEFAULT = @"\W+";

        private bool? _Lowercase { get; set; }
        private string _RegexPattern { get; set; }
        private List<RegexFlagEnum> _RegexFlags { get; set; }

        [JsonProperty("lowercase")]
        [DefaultValue(_LOWERCASE_DEFAULT)]
        public bool Lowercase 
        {
            get
            {
                if (_Lowercase.HasValue)
                    return _Lowercase.Value;
                return _LOWERCASE_DEFAULT;
            }
            set
            {
                _Lowercase = value;
            }
        }

        [JsonProperty("pattern")]
        [DefaultValue(_REGEX_PATTERN_DEFAULT)]
        public string RegexPattern 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_RegexPattern))
                    return _RegexPattern;
                return _REGEX_PATTERN_DEFAULT;
            }
            set
            {
                _RegexPattern = value;
            }
        }

        [JsonProperty("flags")]
        [DefaultValue(default(string))]
        public string Flags 
        {
            get
            {
                if (_RegexFlags == null || !_RegexFlags.Any())
                    return default(string);

                StringBuilder flagBuilder = new StringBuilder();
                foreach (RegexFlagEnum flag in _RegexFlags)
                {
                    if (flagBuilder.Length > 0)
                        flagBuilder.Append("|");

                    flagBuilder.Append(flag.ToString());
                }

                if (flagBuilder.Length == 0)
                    return default(string);

                return flagBuilder.ToString();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _RegexFlags = null;
                else
                {
                    List<RegexFlagEnum> flags = new List<RegexFlagEnum>();
                    foreach (string flag in value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        RegexFlagEnum temp = RegexFlagEnum.CanonicalEquivalence;
                        temp = RegexFlagEnum.Find(flag);
                        if (temp != null)
                            flags.Add(temp);
                        else
                            throw new ArgumentOutOfRangeException("Flags", flag + " is not a valid flag.");
                    }
                }
            }
        }
    }
}
