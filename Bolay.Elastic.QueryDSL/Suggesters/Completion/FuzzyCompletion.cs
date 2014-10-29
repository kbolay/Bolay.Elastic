using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Completion
{
    public class FuzzyCompletion
    {
        private int _MinimumLength { get; set; }
        private int _PrefixLength { get; set; }

        /// <summary>
        /// Gets or sets the allowed range for the completion suggestions.
        /// Defaults to AUTO.
        /// </summary>
        public object Fuzziness { get; set; }

        /// <summary>
        /// Gets or sets whether the transpositions should be counted as one or two changes.
        /// Defaults to true.
        /// </summary>
        public bool Transpositions { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the text for fuzzy suggestions to be returned.
        /// Default to 3.
        /// </summary>
        public int MinimumLength 
        {
            get { return _MinimumLength; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumLength", "MinimumLength must have a value greater than zero.");

                _MinimumLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the required length for a prefix before fuzzy suggestions can be returned.
        /// Defaults to 1.
        /// </summary>
        public int PrefixLength
        {
            get { return _PrefixLength; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentNullException("PrefixLength", "PrefixLength must have a value greater than zero.");

                _PrefixLength = value;
            }
        }

        /// <summary>
        /// Gets or sets all measurements characters instead of bytes.
        /// Defaults to false.
        /// </summary>
        public bool IsUnicodeAware { get; set; }

        public FuzzyCompletion()
        { 
            Fuzziness = CompletionSerializer._FUZZINESS_DEFAULT;
            Transpositions = CompletionSerializer._TRANSPOSITIONS_DEFAULT;
            MinimumLength = CompletionSerializer._MINIMUM_LENGTH_DEFAULT;
            PrefixLength = CompletionSerializer._PREFIX_LENGTH_DEFAULT;
            IsUnicodeAware = CompletionSerializer._IS_UNICODE_AWARE_DEFAULT;
        }
    }
}
