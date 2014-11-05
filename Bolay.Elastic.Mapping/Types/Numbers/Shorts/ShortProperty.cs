using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.Shorts
{
    [JsonConverter(typeof(ShortPropertySerializer))]
    public class ShortProperty : NumberProperty
    {
        private Int16? _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        [JsonProperty(PropertyName = "null_value")]
        [DefaultValue(default(Int16))]
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
                Int16 intValue = 0;
                if (Int16.TryParse(value.ToString(), out intValue))
                    _NullValue = intValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a Int16.");
            }
        }

        public ShortProperty(string name) : base(name, PropertyTypeEnum.Short) { }
    }
}
