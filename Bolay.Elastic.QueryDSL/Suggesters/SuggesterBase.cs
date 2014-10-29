using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    public abstract class SuggesterBase : ISuggester
    {
        private int _Size { get; set; }

        // <summary>
        /// Gets the name used for suggestions from this phrase and field.
        /// </summary>
        public string SuggestName { get; private set; }

        /// <summary>
        /// Gets the field used to find suggestions.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the text that is used for making suggestions from.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the number of suggestions to return.
        /// Defaults to 5.
        /// </summary>
        public int Size
        {
            get { return _Size; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Size", "Size must be greater than zero.");

                _Size = value;
            }
        }

        /// <summary>
        /// Create a suggestor base.
        /// </summary>
        /// <param name="suggestName">The name for these suggestions.</param>
        /// <param name="field">The field to look in for suggestions.</param>
        public SuggesterBase(string suggestName, string field)
        {
            if (string.IsNullOrWhiteSpace(suggestName))
                throw new ArgumentNullException("suggestName", "FieldSuggest requires a suggest name.");
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "FieldSuggest requires a field.");

            SuggestName = suggestName;
            Field = field;

            Size = SuggestSerializer._SIZE_DEFAULT;
        }
    }
}
