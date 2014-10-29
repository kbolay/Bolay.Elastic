using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Elision
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-elision-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(ElisionTokenFilterSerializer))]
    public class ElisionTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets the articles to remove from tokens.
        /// </summary>
        public IEnumerable<string> Articles { get; private set; }

        /// <summary>
        /// Create an elision token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="articles">Sets the list of articles to remove from tokens.</param>
        public ElisionTokenFilter(string name, IEnumerable<string> articles) 
            : base(name, TokenFilterTypeEnum.Elision) 
        {
            if (articles == null || articles.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("articles", "ElisionTokenFilter requires at least one article.");

            Articles = articles.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
