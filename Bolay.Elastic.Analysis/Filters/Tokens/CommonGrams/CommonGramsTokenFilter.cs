using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.CommonGrams
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-common-grams-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(CommonGramsTokenFilterSerializer))]
    public class CommonGramsTokenFilter : TokenFilterBase
    {
        internal const bool _IGNORE_CASE_DEFAULT = false;
        internal const bool _QUERY_MODE_DEFAULT = false;

        /// <summary>
        /// Gets the common words.
        /// </summary>
        public IEnumerable<string> CommonWords { get; private set; }

        /// <summary>
        /// Gets the path of the commmon words configuration file. 
        /// </summary>
        public string CommonWordsPath { get; private set; }

        /// <summary>
        /// Gets or sets whether to ignore case.
        /// Defaults to false.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets whether to use the query mode.
        /// Defaults to false.
        /// </summary>
        public bool QueryMode { get; set; }


        public CommonGramsTokenFilter(string name, IEnumerable<string> commonWords) : base(name, TokenFilterTypeEnum.CommonGrams)
        {
            if (commonWords == null || commonWords.All(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentNullException("commonWords", "CommonGramsTokenFilter requires at least one common word in this constructor.");
            }

            CommonWords = commonWords.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        public CommonGramsTokenFilter(string name, string commonWordsPath)
            : base(name, TokenFilterTypeEnum.CommonGrams)
        { 
            if(string.IsNullOrWhiteSpace(commonWordsPath))
            {
                throw new ArgumentNullException("commonWordsPath", "CommonGramsTokenFilter requires commonWordsPath in this constructor.");
            }

            CommonWordsPath = commonWordsPath;
        }
    }
}
