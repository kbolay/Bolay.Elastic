using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Numbers.Longs
{
    [JsonConverter(typeof(LongPropertySerializer))]
    public class LongProperty : NumberProperty
    {
        private Int64? _NullValue { get; set; }

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
                Int64 intValue = 0;
                if (Int64.TryParse(value.ToString(), out intValue))
                    _NullValue = intValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a Int64.");
            }
        }

        public LongProperty(string name) : base(name, PropertyTypeEnum.Long) { }
    }
}
