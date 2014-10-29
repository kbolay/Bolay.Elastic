using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Stemmer : TokenFilterBase
    {
        private StemmerLanguageEnum _Language { get; set; }

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
                    StemmerLanguageEnum lang = StemmerLanguageEnum.Arabic;
                    lang = StemmerLanguageEnum.Find(value);
                    if (lang != null)
                        _Language = lang;
                    else
                        throw new ArgumentOutOfRangeException("Language", "Not a valid stemmer language.");
                }
                else
                    _Language = null;
            }
        }

        public Stemmer() : base(TokenFilterEnum.Stemmer) { }
    }
}
