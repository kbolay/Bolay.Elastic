using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.Integers
{
    [JsonConverter(typeof(IntegerPropertySerializer))]
    public class IntegerProperty : NumberProperty
    {
        private Int32? _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        [JsonProperty(PropertyName = "null_value")]
        [DefaultValue(default(int))]
        public override object NullValue
        {
            get { return _NullValue; }
            set
            {
                if (value == null)
                {
                    _NullValue = null;
                    return;
                }
                Int32 intValue = 0;
                if (Int32.TryParse(value.ToString(), out intValue))
                    _NullValue = intValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a Int32.");
            }
        }

        public IntegerProperty(string name) : base(name, PropertyTypeEnum.Integer) { }
    }
}
