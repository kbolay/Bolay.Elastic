using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Stop
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-stop-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(StopTokenFilterSerializer))]
    public class StopTokenFilter : TokenFilterBase
    {
        internal const bool _IGNORE_CASE_DEFAULT = false;
        internal const bool _REMOVE_TRAILING_DEFAULT = true;

        /// <summary>
        /// Gets or sets the stopwords. A null value defaults to the English stopword set.
        /// Defaults to null.
        /// </summary>
        public IEnumerable<string> Stopwords { get; set; }

        /// <summary>
        /// Gets or sets the path, absolute or relative, of a stopwords config file.
        /// </summary>
        public string StopwordsPath { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore the case of the tokens.
        /// Defaults to false.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets whether to remove the last token from a search if the token is a stopword.
        /// Defaults to true.
        /// </summary>
        public bool RemoveTrailing { get; set; }

        /// <summary>
        /// Creates a stop token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public StopTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.StopWord) 
        {
            IgnoreCase = _IGNORE_CASE_DEFAULT;
            RemoveTrailing = _REMOVE_TRAILING_DEFAULT;
        }
    }
}
