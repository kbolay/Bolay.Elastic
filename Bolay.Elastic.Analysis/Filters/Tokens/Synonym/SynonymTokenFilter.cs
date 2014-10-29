using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Synonym
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-synonym-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(SynonymTokenFilterSerializer))]
    public class SynonymTokenFilter : TokenFilterBase
    {
        internal const bool _EXPAND_DEFAULT = true;
        internal const bool _INGORE_CASE_DEFAULT = false;

        /// <summary>
        /// Gets or sets whether to ignore case.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets whether to expand.
        /// </summary>
        public bool Expand { get; set; }

        // TODO: Make an object for synonyms. Both formats.
        /// <summary>
        /// Gets or sets the synonyms.
        /// </summary>
        public IEnumerable<string> Synonyms { get; private set; }

        /// <summary>
        /// Gets or sets the path to the configuration file for synonyms.
        /// </summary>
        public string SynonymsPath { get; private set; }

        /// <summary>
        /// Create a synonym token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        private SynonymTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.Synonym) 
        {
            IgnoreCase = _INGORE_CASE_DEFAULT;
            Expand = _EXPAND_DEFAULT;
        }

        /// <summary>
        /// Create SynonymTokenFilter using a list of synonyms.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="synonyms">Sets the synonyms for the synonym token filter.</param>
        public SynonymTokenFilter(string name, IEnumerable<string> synonyms)
            : this(name)
        {
            if (synonyms == null || synonyms.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("synonyms", "SynonymTokenFilter requires a least one synonym in this constructor.");

            Synonyms = synonyms.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        /// <summary>
        /// Create a synonym token filter using a path.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="synonymsPath">Sets the path of the synonyms configuration file.</param>
        public SynonymTokenFilter(string name, string synonymsPath)
            : this(name)
        {
            if (string.IsNullOrWhiteSpace(synonymsPath))
                throw new ArgumentNullException("synonymPath", "SynonymTokenFilter requires a path in this constructor.");

            SynonymsPath = synonymsPath;
        }
    }
}