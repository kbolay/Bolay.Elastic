using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-date-format.html
    /// </summary>
    [JsonConverter(typeof(DateFormatSerializer))]
    public class DateFormat
    {
        private const string _DELIMITER = "||";
        private static List<string> _DELIMITERS = new List<string>() { " || ", "|| ", " ||", "||" };

        /// <summary>
        /// Gets the datetime format.
        /// </summary>
        public string Format { get; private set; }        

        /// <summary>
        /// Create a datetime format based on an accepted enumeration.
        /// </summary>
        /// <param name="format">Sets the format for datetimes.</param>
        public DateFormat(DateTimeFormatEnum format)
        {
            if (format == null)
                throw new ArgumentNullException("format", "DateFormat requires a DateTimeFormatEnum in this constructor.");

            Format = format.ToString();
        }

        /// <summary>
        /// Creates the datetime format based on a single string. This could be date math or standard string formatting for a datetime.
        /// </summary>
        /// <param name="format">Sets the format for datetimes.</param>
        public DateFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException("format", "DateFormat requires a string based format in this constructor.");

            Format = format;
        }

        /// <summary>
        /// Creates the datetime format based on multiple strings. These can be date math or standard string formatting.
        /// </summary>
        /// <param name="formats">Sets the formats to be used for datetime.</param>
        public DateFormat(IEnumerable<string> formats)
        {
            if (formats == null || formats.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("formats", "DateFormat requires at least one string based format in this constructor.");

            StringBuilder builder = new StringBuilder();
            foreach (string format in formats.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (builder.Length > 0)
                    builder.Append(_DELIMITER);

                builder.Append(format);
            }

            if(builder.Length > 0)
                Format = builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DateFormat))
                return false;

            if (obj == null)
                return false;

            DateFormat format = obj as DateFormat;
            if (this.Format.Equals(format.Format))
            {
                return true;
            }

            return false;
        }
    }
}
