using Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Custom : AnalyzerBase
    {
        private string _TokenizerName { get; set; }
        private List<string> _TokenFilterNames { get; set; }
        private List<string> _CharacterFilterNames { get; set; }

        [JsonIgnore]
        internal TokenizerBase Tokenizer { get; set; }

        [JsonProperty("tokenizer")]
        public string TokenizerName 
        {
            get
            {
                if (Tokenizer != null)
                    return Tokenizer.Name;
                return _TokenizerName;
            }
            set
            {
                _TokenizerName = value;
            }
        }

        [JsonIgnore]
        internal List<TokenFilterBase> TokenFilters { get; set; }

        [JsonProperty("filter")]
        [DefaultValue(default(List<string>))]
        public List<string> TokenFilterNames 
        {
            get
            { 
                List<string> names = new List<string>();
                if (TokenFilters != null && TokenFilters.Any())
                    names.AddRange(TokenFilters.Select(x => x.Name).Distinct());

                if (_TokenFilterNames != null && _TokenFilterNames.Any())
                    names.AddRange(_TokenFilterNames.Distinct());

                names = names.Distinct().ToList();

                if (!names.Any())
                    return null;

                return names;
            }
            set
            {
                _TokenFilterNames = value;
            }
        }

        [JsonIgnore]
        internal List<CharacterFilterBase> CharacterFilters { get; set; }

        [JsonProperty("char_filter")]
        [DefaultValue(default(List<string>))]
        public List<string> CharacterFilterNames 
        {
            get
            {
                List<string> names = new List<string>();
                if (CharacterFilters != null && CharacterFilters.Any())
                    names.AddRange(CharacterFilters.Select(x => x.Name).Distinct());

                if (_CharacterFilterNames != null && _CharacterFilterNames.Any())
                    names.AddRange(_CharacterFilterNames.Distinct());

                names = names.Distinct().ToList();

                if (!names.Any())
                    return null;

                return names;
            }
            set
            {
                _CharacterFilterNames = value;
            }
        }

        public Custom() : base(AnalyzerEnum.Custom) { }
    }
}
