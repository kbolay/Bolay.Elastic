using Bolay.Elastic.Analysis.Analyzers;
using Bolay.Elastic.Analysis.Filters.Characters;
using Bolay.Elastic.Analysis.Filters.Tokens;
using Bolay.Elastic.Analysis.Tokenizers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Custom
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-custom-analyzer.html
    /// </summary>
    [JsonConverter(typeof(CustomAnalyzerSerializer))]
    public class CustomAnalyzer : AnalyzerBase
    {
        internal AnalysisSettings _Settings { get; set; }

        /// <summary>
        /// Gets the tokenizer for the analyzer.
        /// </summary>
        public ITokenizer Tokenizer { get; internal set; }

        /// <summary>
        /// Gets the token filters for the analyzer.
        /// </summary>
        public IEnumerable<ITokenFilter> TokenFilters { get; internal set; }

        /// <summary>
        /// Gets the character filters for the analyzer.
        /// </summary>
        public IEnumerable<ICharacterFilter> CharacterFilters { get; internal set; }

        /// <summary>
        /// Create a custom analyzer using only a tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="tokenizer">Sets the tokenizer of the analyzer.</param>
        public CustomAnalyzer(string name, ITokenizer tokenizer) : 
            base(name, AnalyzerTypeEnum.Custom) 
        {
            if (tokenizer == null)
                throw new ArgumentNullException("tokenizer", "CustomAnalyzer requires a tokenizer.");

            Tokenizer = tokenizer;
        }

        /// <summary>
        /// Create a custom analyzer with a tokenizer and token filters.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="tokenizer">Sets the tokenizer.</param>
        /// <param name="tokenFilters">Sets the token filters.</param>
        public CustomAnalyzer(string name, ITokenizer tokenizer, IEnumerable<ITokenFilter> tokenFilters)
            : this(name, tokenizer)
        {
            if (tokenFilters == null || tokenFilters.All(x => x == null))
            {
                throw new ArgumentNullException("tokenFilters", "CustomAnalyzer requires at least one token filter in this constructor.");
            }

            TokenFilters = tokenFilters.Where(x => x != null);
        }

        /// <summary>
        /// Create a custom analyzer with a tokenizer and character filters.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="tokenizer">Sets the tokenizer.</param>
        /// <param name="characterFilters">Sets the character filters.</param>
        public CustomAnalyzer(string name, ITokenizer tokenizer, IEnumerable<ICharacterFilter> characterFilters)
            : this(name, tokenizer)
        {
            if (characterFilters == null || characterFilters.All(x => x == null))
            {
                throw new ArgumentNullException("characterFilters", "CustomAnalyzer requires at least one character filter in this constructor.");
            }

            CharacterFilters = characterFilters.Where(x => x != null);
        }

        /// <summary>
        /// Create a custom analyzer with a tokenizer, token filters, and character filters.
        /// </summary>
        /// <param name="name">Sets the name of the analyzer.</param>
        /// <param name="tokenizer">Sets the tokenizer.</param>
        /// <param name="tokenFilters">Sets the token filters.</param>
        /// <param name="characterFilters">Sets the character filters.</param>
        public CustomAnalyzer(string name, ITokenizer tokenizer, IEnumerable<ITokenFilter> tokenFilters, IEnumerable<ICharacterFilter> characterFilters)
            : this(name, tokenizer)
        {
            if (tokenFilters == null || tokenFilters.All(x => x == null))
            {
                throw new ArgumentNullException("tokenFilters", "CustomAnalyzer requires at least one token filter in this constructor.");
            }

            if (characterFilters == null || characterFilters.All(x => x == null))
            {
                throw new ArgumentNullException("characterFilters", "CustomAnalyzer requires at least one character filter in this constructor.");
            }

            TokenFilters = tokenFilters.Where(x => x != null);
            CharacterFilters = characterFilters.Where(x => x != null);
        }
    }
}
