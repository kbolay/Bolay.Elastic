using Bolay.Elastic.QueryDSL.Suggesters.Term;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-suggesters.html
    /// </summary>
    [JsonConverter(typeof(SuggestSerializer))]
    public class Suggest : ISearchPiece
    {
        /// <summary>
        /// Gets or sets the global text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets the fields to get suggestions from.
        /// </summary>
        public IEnumerable<ISuggester> Suggestors { get; private set; }

        /// <summary>
        /// Create a suggest request body document.
        /// </summary>
        /// <param name="suggestors">Sets the fields to get suggests from.</param>
        public Suggest(IEnumerable<ISuggester> suggestors)
        {
            if (suggestors == null || suggestors.All(x => x == null))
                throw new ArgumentNullException("suggestors", "Suggest requires at least one field suggest.");

            Suggestors = suggestors.Where(x => x != null);
        }
    }
}
