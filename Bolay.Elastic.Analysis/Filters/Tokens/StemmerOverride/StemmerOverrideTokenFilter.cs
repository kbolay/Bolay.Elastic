using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.StemmerOverride
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-stemmer-override-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(StemmerOverrideTokenFilterSerializer))]
    public class StemmerOverrideTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        public IEnumerable<string> Rules { get; set; }

        /// <summary>
        /// Gets or sets the path of the rules configuration file.
        /// </summary>
        public string RulesPath { get; set; }

        /// <summary>
        /// Create a stemmer override token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public StemmerOverrideTokenFilter(string name) : base(name, TokenFilterTypeEnum.StemmerOverride) { }
    }
}
