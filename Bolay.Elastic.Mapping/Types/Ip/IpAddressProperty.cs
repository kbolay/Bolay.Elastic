using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Ip
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-ip-type.html
    /// </summary>
    [JsonConverter(typeof(IpAddressPropertySerializer))]
    public class IpAddressProperty : DocumentPropertyBase
    {
        internal const int _PRECISION_STEP_DEFAULT = 4;
        internal const Double _BOOST_DEFAULT = 1.0;
       
        private string _NullValue { get; set; }

        /// <summary>
        /// Gets or sets the value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        public object NullValue { get { return _NullValue; } set { _NullValue = value.ToString(); } }

        /// <summary>
        /// Gets or sets the precision step (number of terms generated for each number value). 
        /// Defaults to 4.
        /// </summary>
        public int PrecisionStep { get; set; }

        /// <summary>
        /// Gets or sets the boost value.
        /// Defaults to 1.0.
        /// </summary>
        public Double Boost { get; set; }

        public IpAddressProperty(string name)
            : base(name, PropertyTypeEnum.IpAddress)
        {
            PrecisionStep = _PRECISION_STEP_DEFAULT;
            Boost = _BOOST_DEFAULT;
        }
    }
}
