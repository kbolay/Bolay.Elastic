using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Numbers.Bytes
{
    [JsonConverter(typeof(BytePropertySerializer))]
    public class ByteProperty : NumberProperty
    {
        private System.Byte? _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        public override object NullValue 
        { 
            get { return _NullValue; } 
            set 
            { 
                if(value == null)
                {
                    _NullValue = null;
                    return;
                }

                System.Byte byteValue = new System.Byte();
                if (System.Byte.TryParse(value.ToString(), out byteValue))
                    _NullValue = byteValue;
                else
                    throw new ArgumentException("NullValue", "NullValue must be a Byte.");
            } 
        }

        /// <summary>
        /// Create a Byte property.
        /// </summary>
        /// <param name="name"></param>
        public ByteProperty(string name) : base(name, PropertyTypeEnum.Byte) { }
    }
}
