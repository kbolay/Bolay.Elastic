using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase
{
    public class DirectGenerator
    {
        private Double _MaximumEditDistance { get; set; }
        private int _PrefixLength { get; set; }
        private int _MinimumWordLength { get; set; }
        private int _MaximumInspections { get; set; }
        private Double _MinimumDocumentFrequency { get; set; }
        private Double _MaximumTermFrequency { get; set; }

        /// <summary>
        /// Gets the field to do n-gram lookups against. 
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets maximum suggestions to return from the direct generator. 
        /// Defaults to 5.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the mode to determine which suggestions are included in a response.
        /// Defaults to missing.
        /// </summary>
        public SuggestModeEnum SuggestMode { get; set; }

        /// <summary>
        /// Gets or sets the max_edit value. Must be between 1 and 2.
        /// Defaults to 2.
        /// </summary>
        public Double MaximumEditDistance 
        { 
            get { return _MaximumEditDistance; }
            set
            {
                if(value < 1 || value > 2)
                    throw new ArgumentOutOfRangeException("MaximumEditDistance", "MaximumEditDistance must be between within 1.0 and 2.0.");

                _MaximumEditDistance = value;
            }
        }

        /// <summary>
        /// Gets or sets the required length of a prefix before it is checked for suggestions.
        /// Defaults to 1.
        /// </summary>
        public int PrefixLength 
        {
            get { return _PrefixLength; }
            set
            {
                if (value >= 1)
                    throw new ArgumentOutOfRangeException("PrefixLenght", "PrefixLength must be greater than 0.");

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
                if (value > 1 && value % 1.0 != 0)
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
        /// Gets or sets an analyzer that is applied to tokens passed to the candidate generator.
        /// </summary>
        public string PreFilter { get; set; }

        /// <summary>
        /// Gets or sets an analyzer that is applied to tokens befor they are passed to the phrase scorer.
        /// </summary>
        public string PostFilter { get; set; }

        public DirectGenerator()
        {
            Size = SuggestSerializer._SIZE_DEFAULT;
            SuggestMode = DirectGeneratorSerializer._SUGGEST_MODE_DEFAULT;
            MaximumEditDistance = DirectGeneratorSerializer._MAXIMUM_EDIT_DISTANCE_DEFAULT;
            PrefixLength = DirectGeneratorSerializer._PREFIX_LENGTH_DEFAULT;
            MinimumWordLength = DirectGeneratorSerializer._MINIMUM_WORD_LENGTH_DEFAULT;
            MaximumInspections = DirectGeneratorSerializer._MAXIMUM_INSPECTIONS_DEFAULT;
            MinimumDocumentFrequency = DirectGeneratorSerializer._MINIMUM_DOCUMENT_FREQUENCY_DEFAULT;
            MaximumTermFrequency = DirectGeneratorSerializer._MAXIMUM_TERM_FREQUENCY_DEFAULT;
        }
    }
}
