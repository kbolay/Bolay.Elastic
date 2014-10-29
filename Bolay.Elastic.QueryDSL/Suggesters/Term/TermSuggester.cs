using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Term
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-suggesters-term.html
    /// </summary>
    [JsonConverter(typeof(TermSerializer))]
    public class TermSuggester : SuggesterBase
    {
        private Double _MaximumEditDistance { get; set; }
        private int _MinimumWordLength { get; set; }
        private int _PrefixLength { get; set; }
        private int _ShardSize { get; set; }
        private int _MaximumInspections { get; set; }
        private Double _MinimumDocumentFrequency { get; set; }
        private Double _MaximumTermFrequency { get; set; }

        /// <summary>
        /// Gets or sets the analyzer to use on the suggest text to break it into tokens.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Gets or sets how the suggestions should be sorted per text token.
        /// </summary>
        public SortModeEnum Sort { get; set; }

        /// <summary>
        /// Gets or sets which suggestions are included or what text terms suggestions should be included for.
        /// Defaults to missing.
        /// </summary>
        public SuggestModeEnum SuggestMode { get; set; }

        /// <summary>
        /// Gets or sets whether to lowercase the text tokens after analysis.
        /// Defaults to false.
        /// </summary>
        public bool LowercaseTerms { get; set; }

        /// <summary>
        /// Gets or sets the maximum edit distance for suggestions. Must be between 1 and 2.
        /// Defaults to 2.
        /// </summary>
        public Double MaximumEditDistance
        {
            get { return _MaximumEditDistance; }
            set 
            {
                if (value < 1 || value > 2)
                    throw new ArgumentOutOfRangeException("MaximumEditDistance", "MaximumEditDistance must be between 1 and 2.");

                _MaximumEditDistance = value;
            }
        }

        /// <summary>
        /// Gets or sets the required length of a prefix to be considered as a valid candidate for suggestions.
        /// Defaults to 1.
        /// </summary>
        public int PrefixLength
        {
            get { return _PrefixLength; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("PrefixLength", "PrefixLength must be greater than 0.");

                _PrefixLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the length a text token must have to be included.
        /// Default to 4.
        /// </summary>
        public int MinimumWordLength
        {
            get { return _MinimumWordLength; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumWordLength", "MinimumWordLength must be greater than 0.");

                _MinimumWordLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of suggestions to retrieve from each shard before the results are reduced to the size value.
        /// Defaults to 5 or the size value.
        /// </summary>
        public int ShardSize
        {
            get { return _ShardSize; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentNullException("ShardSize", "ShardSize must have a value greater than zero.");

                _ShardSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the multiplier for shard size to inspect more spelling corrections.
        /// Defaults to 5.
        /// </summary>
        public int MaximumInspections
        {
            get { return _MaximumInspections; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumInspections", "MaximumInspections must have a value greater than zero.");

                _MaximumInspections = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum number of documents the term should appear in. If the value is less than one it is treated as a percentage. Values greater than one should be whole numbers.
        /// Defaults to 0, and is not considered.
        /// </summary>
        public Double MinimumDocumentFrequency
        {
            get { return _MinimumDocumentFrequency; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MinimumDocumentFrequency", "MinimumDocumentFrequency must be greater than 0.");
                if(value > 1 && value % 1.0 != 0)
                    throw new ArgumentOutOfRangeException("MinimumDocumentFrequency", "MinimumDocumentFrequency with a value greater than zero must be a whole number.");

                _MinimumDocumentFrequency = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum frequency a term should appear and still be spell checked. If the value is less than one it is treated as a percentage. Values greater than one should be whole numbers.
        /// Defaults to 0.01.
        /// </summary>
        public Double MaximumTermFrequency
        {
            get { return _MaximumTermFrequency; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaximumTermFrequency", "MaximumTermFrequency must be greater than 0.");
                if (value > 1 && value % 1.0 != 0)
                    throw new ArgumentOutOfRangeException("MaximumTermFrequency", "MaximumTermFrequency with a value greater than zero must be a whole number.");

                _MaximumTermFrequency = value;
            }
        }

        /// <summary>
        /// Create a field suggest clause.
        /// </summary>
        /// <param name="suggestName">The name for these suggestions.</param>
        /// <param name="field">The field to look in for suggestions.</param>
        public TermSuggester(string suggestName, string field) : base(suggestName, field) 
        {
            Size = SuggestSerializer._SIZE_DEFAULT;
            Sort = TermSerializer._SORT_DEFAULT;
            SuggestMode = TermSerializer._SUGGEST_MODE_DEFAULT;
            LowercaseTerms = TermSerializer._LOWERCASE_TERMS_DEFAULT;
            PrefixLength = TermSerializer._PREFIX_LENGTH_DEFAULT;
            MinimumWordLength = TermSerializer._MINIMUM_WORD_LENGTH_DEFAULT;
            ShardSize = TermSerializer._SHARD_SIZE_DEFAULT;
            MaximumInspections = TermSerializer._MAXIMUM_INSPECTIONS_DEFAULT;
            MinimumDocumentFrequency = TermSerializer._MINIMUM_DOCUMENT_FREQUENCY_DEFAULT;
            MaximumTermFrequency = TermSerializer._MAXIMUM_TERM_FREQUENCY_DEFAULT;
            MaximumEditDistance = TermSerializer._MAXIMUM_EDIT_DISTANCE_DEFAULT;
        }
    }
}
