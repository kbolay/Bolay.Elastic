using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Numbers
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-core-types.html#number
    /// </summary>
    public abstract class NumberProperty : FieldProperty
    {
        private const string _PRECISION_STEP = "precision_step";
        private const string _DOC_VALUES = "doc_values";
        private const string _IGNORE_MALFORMED = "ignore_malformed";
        private const string _COERCE = "coerce";

        internal const int _PRECISION_STEP_DEFAULT = 4;
        internal const bool _DOC_VALUES_DEFAULT = false;
        internal const bool _IGNORE_MALFORMED_DEFAULT = false;
        internal const bool _COERCE_DEFAULT = true;

        private int _PrecisionStep { get; set; }

        /// <summary>
        /// Gets or sets the precision_step.
        /// Defaults to 4.
        /// </summary>
        public int PrecisionStep
        {
            get { return _PrecisionStep; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("PrecisionStep", "PrecisionStep must be greater than zero.");

                _PrecisionStep = value;
            }
        }

        /// <summary>
        /// Gets or sets the doc_values.
        /// Defaults to false, unless TermVector is set to doc_values.
        /// </summary>
        public bool DocValues { get; set; }

        /// <summary>
        /// Ignored a malformed number. Defaults to false. (Since @0.19.9).
        /// Defaults to false.
        /// </summary>
        public bool IgnoreMalformed { get; set; }

        /// <summary>
        /// Gets or sets where to attempt to coerce values into the number type.
        /// Defaults to true.
        /// </summary>
        public bool Coerce { get; set; }

        /// <summary>
        /// Establish defaults.
        /// </summary>
        public NumberProperty(string name, PropertyTypeEnum propertyType) : base(name, propertyType) 
        {
            PrecisionStep = _PRECISION_STEP_DEFAULT;
            DocValues = _DOC_VALUES_DEFAULT;
            IgnoreMalformed = _IGNORE_MALFORMED_DEFAULT;
            Coerce = _COERCE_DEFAULT;
        }

        internal static void SerializeNumber(NumberProperty number, Dictionary<string, object> fieldDict)
        {
            if (number == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            FieldProperty.Serialize(number, fieldDict);
            fieldDict.AddObject(_PRECISION_STEP, number.PrecisionStep, _PRECISION_STEP_DEFAULT);
            fieldDict.AddObject(_DOC_VALUES, number.DocValues, _DOC_VALUES_DEFAULT);
            fieldDict.AddObject(_IGNORE_MALFORMED, number.IgnoreMalformed, _IGNORE_MALFORMED_DEFAULT);
            fieldDict.AddObject(_COERCE, number.Coerce, _COERCE_DEFAULT);
        }

        internal static void DeserializeNumber(NumberProperty number, Dictionary<string, object> fieldDict)
        {
            FieldProperty.Deserialize(number, fieldDict);
            number.PrecisionStep = fieldDict.GetInt32(_PRECISION_STEP, _PRECISION_STEP_DEFAULT);
            number.DocValues = fieldDict.GetBool(_DOC_VALUES, _DOC_VALUES_DEFAULT);
            number.IgnoreMalformed = fieldDict.GetBool(_IGNORE_MALFORMED, _IGNORE_MALFORMED_DEFAULT);
            number.Coerce = fieldDict.GetBool(_COERCE, _COERCE_DEFAULT);
        }
    }
}
