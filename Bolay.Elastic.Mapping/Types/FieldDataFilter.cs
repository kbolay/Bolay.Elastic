using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    public class FieldDataFilter
    {
        private const string _REGEX_PATTERN = "regex";
        private const string _FREQUENCY = "freq";

        /// <summary>
        /// Get the regular expression pattern.
        /// </summary>
        public string RegexPattern { get; private set; }

        /// <summary>
        /// Get the required frequency.
        /// </summary>
        public Frequency Frequency { get; private set; }

        /// <summary>
        /// Create a field_data filter with a regular expression pattern.
        /// </summary>
        /// <param name="regexPattern">Sets the regular expression pattern.</param>
        public FieldDataFilter(string regexPattern)
        {
            if (string.IsNullOrEmpty(regexPattern))
                throw new ArgumentNullException("regexPattern", "FieldDataFilter requires a regular expression pattern.");

            RegexPattern = regexPattern;
        }

        /// <summary>
        /// Create a field_data filter with a regular expression and frequency requirements.
        /// </summary>
        /// <param name="regexPattern">Sets the regular expression pattern.</param>
        /// <param name="frequency">Sets the frequency requirements.</param>
        public FieldDataFilter(string regexPattern, Frequency frequency)
            : this(regexPattern)
        {
            if (frequency == null)
                throw new ArgumentNullException("frequency", "FieldDataFilter requires a frequency in this constructor.");

            Frequency = frequency;
        }

        internal Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_REGEX_PATTERN, this.RegexPattern);

            if (this.Frequency != null)
            {
                fieldDict.Add(_FREQUENCY, this.Frequency.Serialize());
            }

            return fieldDict;
        }

        internal static FieldDataFilter Deserialize(Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return null;

            if (fieldDict.ContainsKey(_FREQUENCY))
            {
                return new FieldDataFilter(fieldDict.GetString(_REGEX_PATTERN), Frequency.Deserialize(JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_FREQUENCY))));
            }

            return new FieldDataFilter(fieldDict.GetString(_REGEX_PATTERN));
        }
    }
}
