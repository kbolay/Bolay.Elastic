using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Suggestions
{
    public class TermOption
    {
        /// <summary>
        /// Gets the text suggestion.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets the number of times this term appears.
        /// </summary>
        [JsonProperty("freq")]
        public Int64 Frequency { get; set; }

        /// <summary>
        /// Gets the score for this option.
        /// </summary>
        [JsonProperty("score")]
        public Double Score { get; set; }
    }
}
