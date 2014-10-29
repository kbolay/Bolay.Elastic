using Bolay.Elastic.Api.Analyze.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Snowball : TokenFilterBase
    {
        private SnowballLanguageEnum _Language { get; set; }

        [JsonProperty("language")]
        [DefaultValue(default(string))]
        public string Language
        {
            get
            {
                if(_Language != null)
                    return _Language.ToString();
                return default(string);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SnowballLanguageEnum lang = SnowballLanguageEnum.Armenian;
                    lang = SnowballLanguageEnum.Find(value);
                    if (lang != null)
                        _Language = lang;
                    else
                        throw new ArgumentOutOfRangeException("Language", value + " is not a valid snowball language.");
                }
                else
                    _Language = null;
            }
        }

        public Snowball() : base(TokenFilterEnum.Snowball) { }
    }
}
