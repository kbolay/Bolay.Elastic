using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Suggestions
{
    /// <summary>
    /// This is the result of a suggest request.
    /// </summary>
    public class Suggestion
    {
        /// <summary>
        /// Gets or sets the name of the suggester, that this suggestion relates to.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the term suggestions.
        /// </summary>
        public IEnumerable<TermSuggestion> Terms { get; private set; }

        public Suggestion(string name, IEnumerable<TermSuggestion> terms)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Suggestion requires a name.");
            if (terms == null || terms.All(x => x == null))
                throw new ArgumentNullException("terms", "Suggestion requires at least one term.");

            Name = name;
            Terms = terms.Where(x => x != null);
        }
    }
}