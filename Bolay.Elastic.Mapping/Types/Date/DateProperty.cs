using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Date
{
    [JsonConverter(typeof(DatePropertySerializer))]
    public class DateProperty : FieldProperty
    {
        internal const int _PRECISION_STEP_DEFAULT = 4;
        internal const bool _IGNORE_MALFORMED_DEFAULT = false;
        internal static DateFormat _FORMAT_DEFAULT = new DateFormat(DateTimeFormatEnum.DateOptionalTime);
        internal const bool _DOCS_VALUE_DEFAULT = false;

        private string _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        public override object NullValue 
        {
            get { return _NullValue; }
            set
            { 
                if(string.IsNullOrWhiteSpace(value.ToString()))
                    _NullValue =  null;
                else
                    _NullValue = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the docs_value.
        /// Defaults to false.
        /// </summary>
        public bool DocValues { get; set; }

        /// <summary>
        /// The date format.
        /// http://www.elasticsearch.org/guide/reference/mapping/date-format/
        /// </summary>
        public DateFormat Format { get; set; }

        /// <summary>
        /// The precision step (number of terms generated for each number value).
        /// Defaults to 4.
        /// </summary>
        public int PrecisionStep { get; set; }

        /// <summary>
        /// Ignored a malformed number. 
        /// Defaults to false.
        /// </summary>
        public bool IgnoreMalformed { get; set; }

        /// <summary>
        /// Establish defaults
        /// </summary>
        public DateProperty(string name) : base(name, PropertyTypeEnum.Date)
        {
            PrecisionStep = _PRECISION_STEP_DEFAULT;
            IgnoreMalformed = _IGNORE_MALFORMED_DEFAULT;
            DocValues = _DOCS_VALUE_DEFAULT;
            Format = _FORMAT_DEFAULT;
        }
    }
}
