using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    public abstract class HighlighterOptions
    {
        /// <summary>
        /// Gets or sets the type of highlighter to use.
        /// Defaults to plain.
        /// </summary>
        public HighlighterTypeEnum Type { get; set; }

        /// <summary>
        /// Gets or sets the number of fragments to return.
        /// Defaults to 5.
        /// </summary>
        public int NumberOfFragments { get; set; }

        /// <summary>
        /// Gets or sets the number of characters of the fragments.
        /// Defaults to 100.
        /// </summary>
        public int FragmentSize { get; set; }

        /// <summary>
        /// Gets or sets the tag_schema.
        /// </summary>
        public TagsSchemaEnum TagsSchema { get; set; }

        /// <summary>
        /// Gets or sets the pre_tags.
        /// </summary>
        public IEnumerable<string> PreTags { get; set; }

        /// <summary>
        /// Gets or sets the post_tags
        /// </summary>
        public IEnumerable<string> PostTags { get; set; }

        /// <summary>
        /// Gets or sets the encoding type.
        /// Defaults to NoEncoding.
        /// </summary>
        public EncoderTypeEnum Encoder { get; set; }

        /// <summary>
        /// Gets or sets the no_match_size value.
        /// Defaults to 0.
        /// </summary>
        public int NoMatchSize { get; set; }

        /// <summary>
        /// Gets or sets whether to a field is highlighted only if a query matched that field.
        /// </summary>
        public bool RequireFieldMatch { get; set; }

        /// <summary>
        /// Gets or sets the boundary_char value that control the boundary for highlighting.
        /// Defaults to .,!? \t\n.
        /// </summary>
        public string BoundaryCharacters { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance to look for boundary characters.
        /// Defaults to 20.
        /// </summary>
        public int BoundaryMaximumScan { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of phrases to analyze.
        /// Defaults to 256.
        /// </summary>
        public int PhraseLimit { get; set; }

        public HighlighterOptions()
        { 
            Type = HighlighterSerializer._HIGHLIGHTER_TYPE_DEFAULT;
            FragmentSize = HighlighterSerializer._FRAGMENT_SIZE_DEFAULT;
            NumberOfFragments = HighlighterSerializer._NUMBER_OF_FRAGMENTS_DEFAULT;
            Encoder = HighlighterSerializer._ENCODER_DEFAULT;
            NoMatchSize = HighlighterSerializer._NO_MATCH_SIZE_DEFAULT;
            RequireFieldMatch = HighlighterSerializer._REQUIRE_FIELD_MATCH_DEFAULT;
            BoundaryCharacters = HighlighterSerializer._BOUNDARY_CHARACTERS_DEFAULT;
            BoundaryMaximumScan = HighlighterSerializer._BOUNDARY_MAXIMUM_SCAN_DEFAULT;
            PhraseLimit = HighlighterSerializer._PHRASE_LIMIT_DEFAULT;
            TagsSchema = HighlighterSerializer._TAGS_SCHEMA_DEFAULT;
        }
    }
}
