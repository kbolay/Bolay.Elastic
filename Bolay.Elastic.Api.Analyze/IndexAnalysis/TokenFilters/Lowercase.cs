using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Lowercase : TokenFilterBase
    {
        private LowercaseSupportedLanguageEnum _Language { get; set; }

        [JsonProperty("language")]
        [DefaultValue(default(string))]
        public string Language 
        {
            get
            {
                if (_Language != null)
                    return _Language.ToString();
                return default(string);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    LowercaseSupportedLanguageEnum lang = LowercaseSupportedLanguageEnum.Greek;
                    lang = LowercaseSupportedLanguageEnum.Find(value);
                    if (lang != null)
                        _Language = lang;
                    else
                        throw new ArgumentOutOfRangeException("Language", "Not a valid language.");
                }
                else
                    _Language = null;
            }
        }

        public Lowercase() : base(TokenFilterEnum.Lowercase) { }
    }
}
