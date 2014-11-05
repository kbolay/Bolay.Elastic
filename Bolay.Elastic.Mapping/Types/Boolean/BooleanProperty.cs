using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Boolean
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-core-types.html#boolean
    /// </summary>
    [JsonConverter(typeof(BooleanPropertySerializer))]
    public class BooleanProperty : FieldProperty
    {
        private bool? _NullValue { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        public override object NullValue 
        {
            get { return _NullValue; }
            set
            {
                if (value != null)
                {
                    bool boolValue = false;
                    if (System.Boolean.TryParse(value.ToString(), out boolValue))
                        _NullValue = boolValue;
                    else
                        _NullValue = null;
                }
                else
                    _NullValue = null;
            }
        }

        /// <summary>
        /// Establish Defaults
        /// </summary>
        public BooleanProperty(string name) : base(name, PropertyTypeEnum.Boolean) { }
    }
}
