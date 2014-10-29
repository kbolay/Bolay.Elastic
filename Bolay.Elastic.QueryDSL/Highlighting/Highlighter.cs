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
    [JsonConverter(typeof(HighlighterSerializer))]
    public class Highlighter : HighlighterOptions, ISearchPiece
    {
        /// <summary>
        /// Gets the fields to highlight.
        /// </summary>
        public IEnumerable<FieldHighlighter> FieldHighlighters { get; private set; }

        /// <summary>
        /// Create the highlighter.
        /// </summary>
        /// <param name="fieldHighlighters">Sets the fields to highlight.</param>
        public Highlighter(IEnumerable<FieldHighlighter> fieldHighlighters)
        {
            if (fieldHighlighters == null || fieldHighlighters.All(x => x == null))
                throw new ArgumentNullException("fieldHighlighters", "Highlighter requires at least one field highlighter.");

            FieldHighlighters = fieldHighlighters.Where(x => x != null);
        }
    }
}
