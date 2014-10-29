using Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers;
using Bolay.Elastic.Api.Analyze.Models.IndexAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class Pattern : TokenizerBase
    {
        private const string _REGEX_PATTERN_DEFAULT = @"\\W+";
        private const Int64 _GROUP_DEFAULT = -1;

        private string _RegexPattern { get; set; }
        private List<RegexFlagEnum> _RegexFlags { get; set; }
        private Int64? _Group { get; set; }

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

        /// <summary>
        /// A value of 1 is equivalent to split.
        /// Using group >= 0 selects the matching group as the token. 
        /// For example, if you have:
        ///     pattern = \\'([^\']+)\\'
        ///     group   = 0
        ///     input   = aaa 'bbb' 'ccc'
        /// the output will be two tokens: bbb and ccc (including the ' marks). 
        /// With the same input but using group=1, 
        /// the output would be: bbb and ccc (no ' marks).
        /// </summary>
        [JsonProperty("group")]
        [DefaultValue(default(int))]
        public Int64 Group
        {
            get
            {
                if (_Group.HasValue)
                    return _Group.Value;
                return _GROUP_DEFAULT;
            }
            set
            {
                _Group = value;
            }
        }

        public Pattern() : base(TokenizerEnum.Pattern) { }
    }
}
