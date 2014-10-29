using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing
{
    public class HighlightTags
    {
        /// <summary>
        /// Gets the pre_tag value for highlighting a phrase suggestion.
        /// </summary>
        public string PreTag { get; private set; }

        /// <summary>
        /// Gets the post_tag value for highlighting a phrase suggestion.
        /// </summary>
        public string PostTag { get; private set; }

        /// <summary>
        /// Create the HighlightTags for a phrase suggestor.
        /// </summary>
        /// <param name="preTag"></param>
        /// <param name="postTag"></param>
        public HighlightTags(string preTag, string postTag)
        {
            if (string.IsNullOrWhiteSpace(preTag))
                throw new ArgumentNullException("preTag", "HighlightTags requires a pre_tag value.");
            if (string.IsNullOrWhiteSpace(postTag))
                throw new ArgumentNullException("postTag", "HighlightTags requires a post_tag value.");

            PreTag = preTag;
            PostTag = postTag;
        }
    }
}
