using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-suggesters-phrase.html
    /// </summary>
    [JsonConverter(typeof(PhraseSerializer))]
    public class PhraseSuggester : SuggesterBase
    {
        private int? _GramSize { get; set; }
        private Double _RealWordErrorLikelyhood { get; set; }
        private Double _Confidence { get; set; }
        private Double _MaximumErrors { get; set; }
        private string _Separator { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of shingles in the field. 
        /// </summary>
        public int? GramSize
        {
            get { return _GramSize; }
            set 
            {
                if (value.HasValue && value <= 0)
                    throw new ArgumentOutOfRangeException("GramSize", "GramSize must be greater than zero if it has a value.");

                _GramSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the percentage value for real world likelyhood of a term being mispelled. Must be between zero and one.
        /// Defaults to 0.95. Meaning 5% of real words are likely to be misspelled.
        /// </summary>
        public Double RealWordErrorLikelyhood
        {
            get { return _RealWordErrorLikelyhood; }
            set 
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("RealWordErrorLikelyhood", "The value must be greater than zero and less than one.");

                _RealWordErrorLikelyhood = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that suggestions must surpass in order to be returned.
        /// Defaults to 1.0.
        /// </summary>
        public Double Confidence
        {
            get { return _Confidence; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Confidence", "Confidence must be greater than zero.");

                _Confidence = value;
            }
        }

        /// <summary>
        /// The maximum number of misspellings to allow in order to qualify a suggestion. Values between 0 and 1 will be treated as percentages, while values of 1 or more should be whole numbers treated as the absolute number.
        /// Defaults to 1.0
        /// </summary>
        public Double MaximumErrors
        {
            get { return _MaximumErrors; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaximumErrors", "MaximumErrors must be greater than zero.");

                if (value >= 1.0 && value % 1.0 > 0)
                    throw new ArgumentOutOfRangeException("MaximumErrors", "MaximumErrors greater than 1.0 must be whole numbers.");

                _MaximumErrors = value;
            }
        }

        /// <summary>
        /// Gets or sets the separator to use for a bigram field.
        /// Defaults to the whitespace character.
        /// </summary>
        public string Separator
        {
            get { return _Separator; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Separator", "Separator must have a value.");

                _Separator = value;
            }
        }

        /// <summary>
        /// Gets or sets the analyzer to use against the text.
        /// Defaults to the analyzer on the field.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Gets or sets the number of suggestions to retrieve from each shard before the results are reduced to the size value.
        /// Defaults to 5 or the size value.
        /// </summary>
        public int ShardSize { get; set; }

        /// <summary>
        /// Gets or sets the smoothing model use for generating suggestions.
        /// </summary>
        public ISmoothing Smoothing { get; set; }

        /// <summary>
        /// Gets or sets direct candidate generators.
        /// </summary>
        public IEnumerable<DirectGenerator> DirectGenerators { get; set; }

        /// <summary>
        /// Create a phrase generator.
        /// </summary>
        /// <param name="suggestName">The name of the phrase suggest request.</param>
        /// <param name="field">The name of the field to search in.</param>
        public PhraseSuggester(string suggestName, string field) : base(suggestName, field) 
        {
            RealWordErrorLikelyhood = PhraseSerializer._REAL_WORD_ERROR_LIKELYHOOD_DEFAULT;
            Confidence = PhraseSerializer._CONFIDENCE_DEFAULT;
            MaximumErrors = PhraseSerializer._MAXIMUM_ERRORS_DEFAULT;
            Separator = PhraseSerializer._SEPARATOR_DEFAULT;
            ShardSize = PhraseSerializer._SHARD_SIZE_DEFAULT;
            Smoothing = PhraseSerializer._SMOOTHING_DEFAULT;
        }
    }
}
