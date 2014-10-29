using Bolay.Elastic.Api.Analyze.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Snowball : AnalyzerBase
    {
        private const string _LANGUAGE_DEFAULT = "English";

        private SnowballLanguageEnum _Language { get; set; }

        [JsonProperty("language")]
        [DefaultValue(_LANGUAGE_DEFAULT)]
        public string Language
        {
            get
            {
                if (_Language != null)
                    return _Language.ToString();
                return _LANGUAGE_DEFAULT;
            }
            set
            {
                SnowballLanguageEnum lang = SnowballLanguageEnum.English;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    lang = SnowballLanguageEnum.Find(value);
                    if (lang != null)
                        _Language = lang;
                    else
                        _Language = SnowballLanguageEnum.English;
                }
                else
                    _Language = SnowballLanguageEnum.English;
            }
        }

        [JsonProperty("stopwords")]
        [DefaultValue(default(List<string>))]
        public List<string> StopWords { get; set; }

        public Snowball() : base(AnalyzerEnum.Snowball) { }
    }
}
