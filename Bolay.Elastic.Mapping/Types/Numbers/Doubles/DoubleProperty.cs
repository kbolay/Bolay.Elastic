using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.Doubles
{
    [JsonConverter(typeof(DoublePropertySerializer))]
    public class DoubleProperty : NumberProperty
    {
        private Double? _NullValue { get; set; }

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
                Double doubleValue = new Double();
                if (Double.TryParse(value.ToString(), out doubleValue))
                    _NullValue = doubleValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a Double.");
            }
        }

        public DoubleProperty(string name) : base(name, PropertyTypeEnum.Double) { }
    }
}
