using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Binary
{
    /// <summary>
    /// The binary type is a base64 representation of binary data that can be stored in the index. 
    /// The field is stored by default and not indexed at all.
    /// According to http://www.elasticsearch.org/guide/reference/mapping/core-types/ only index_name can be used with this type.
    /// </summary>
    [JsonConverter(typeof(BinaryPropertySerializer))]
    public class BinaryProperty : FieldProperty
    {
        //internal const bool _STORE_DEFAULT = true;

        private object _NullValue { get; set; }

        /// <summary>
        /// Gets or sets the null_value.
        /// </summary>
        public override object NullValue
        {
            get
            {
                return _NullValue;
            }
            set
            {
                if(value == null)
                {
                    _NullValue = null;
                    return;
                }

                Int32 intValue = 0;
                if (Int32.TryParse(value.ToString(), out intValue) && (intValue == 0 || intValue == 1))
                { 
                    _NullValue = intValue;
                }
                else
                    throw new ArgumentOutOfRangeException("NullValue", "NullValue must be a binary value, 1 or 0.");
            }
        }

        public BinaryProperty(string name) : base(name, PropertyTypeEnum.Binary)
        {
            //Store = _STORE_DEFAULT;
        }
    }
}
