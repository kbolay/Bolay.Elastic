using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Completion
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-suggesters-completion.html
    /// </summary>
    [JsonConverter(typeof(CompletionSerializer))]
    public class CompletionSuggester : SuggesterBase
    {
        /// <summary>
        /// Gets the values to control the fuzzy completion progress.
        /// </summary>
        public FuzzyCompletion Fuzzy { get; set; }

        /// <summary>
        /// Create a completion suggestor.
        /// </summary>
        /// <param name="suggestName">The name of the suggestor.</param>
        /// <param name="field">The field to find suggestions in.</param>
        public CompletionSuggester(string suggestName, string field) : base(suggestName, field) { }

        /// <summary>
        /// Create a completion suggestor with fuzzy functionality.
        /// </summary>
        /// <param name="suggestName">The name of the suggestor.</param>
        /// <param name="field">The field to find suggestions in.</param>
        /// <param name="fuzzy">The fuzzy completion values. A default value is acceptable.</param>
        public CompletionSuggester(string suggestName, string field, FuzzyCompletion fuzzy)
            : base(suggestName, field)
        {
            if (fuzzy == null)
                throw new ArgumentNullException("fuzzy", "CompletionSuggestor requires a fuzzy value in this constructor.");

            Fuzzy = fuzzy;
        }
    }
}
