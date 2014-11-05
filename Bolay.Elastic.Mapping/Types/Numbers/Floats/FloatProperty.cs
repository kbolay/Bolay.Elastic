using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.Floats
{
    [JsonConverter(typeof(FloatPropertySerializer))]
    public class FloatProperty : NumberProperty
    {
        private float? _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
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
                float floatValue = new float();
                if (float.TryParse(value.ToString(), out floatValue))
                    _NullValue = floatValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a float.");
            }
        }

        public FloatProperty(string name) : base(name, PropertyTypeEnum.Float) { }
    }
}
