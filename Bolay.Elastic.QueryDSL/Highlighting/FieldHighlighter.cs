using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-highlighting.html
    /// </summary>
    public class FieldHighlighter : HighlighterOptions
    {
        /// <summary>
        /// Gets the field to highlight.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets or sets the matched_fields.
        /// </summary>
        public IEnumerable<string> MatchedFields { get; set; }

        /// <summary>
        /// Gets or sets the highlight_query, this is another way to control which documents are highlighted.
        /// </summary>
        public IQuery HighlightQuery { get; set; }

        /// <summary>
        /// Create a field highlighter.
        /// </summary>
        /// <param name="field">Sets the field to highlight.</param>
        public FieldHighlighter(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "FieldHighlighter requires a field.");

            Field = field;
        }
    }
}
