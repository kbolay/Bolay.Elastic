using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    [JsonConverter(typeof(MissingSerializer))]
    public class MissingValue
    {
        /// <summary>
        /// Gets the missing value, can be _first or _last.
        /// </summary>
        public MissingTypeEnum MissingType { get; private set; }

        /// <summary>
        /// Gets the custom value to use for sorting when a value is missing.
        /// </summary>
        public string CustomValue { get; private set; }

        /// <summary>
        /// Create a missing value for sorting.
        /// </summary>
        /// <param name="missingType">Set the positioning of missing values in the sort process.</param>
        public MissingValue(MissingTypeEnum missingType)
        {
            if (missingType == null)
                throw new ArgumentNullException("missingType", "MissingValue requires a missing type in this constructor.");

            MissingType = missingType;
        }

        /// <summary>
        /// Create a missing value for sorting using a custom value.
        /// </summary>
        /// <param name="customValue">Set the value to use for sorting when the field is missing.</param>
        public MissingValue(string customValue)
        {
            if (string.IsNullOrWhiteSpace(customValue))
                throw new ArgumentNullException("customValue", "MissingValue requires a custom value in this constructor.");

            CustomValue = customValue;
        }

        public override string ToString()
        {
            if (MissingType != null)
                return MissingType.ToString();
            else
                return CustomValue;
        }
    }
}
