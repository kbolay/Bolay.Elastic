using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Suggestions
{
    public class TermSuggestion
    {
        /// <summary>
        /// Gets or sets the text that the suggestion options are for.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the offset of this term in the text submitted for suggestions.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the length of the term.
        /// </summary>
        [JsonProperty("length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the options for this term.
        /// </summary>
        [JsonProperty("options")]
        public IEnumerable<TermOption> Options { get; set; }
    }
}
