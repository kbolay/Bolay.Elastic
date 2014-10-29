using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Routing
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-routing-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentRoutingSerializer))]
    public class DocumentRouting : MappingBase
    {
        private static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.NotAnalyzed;
        private const bool _STORE_DEFAULT = false;
        internal const bool _IS_REQUIRED_DEFAULT = false;

        /// <summary>
        /// Gets or sets the required value.
        /// Defaults to false.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the path of the routing value.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Create a _routing value.
        /// </summary>
        public DocumentRouting() : 
            base(_INDEX_DEFAULT, _STORE_DEFAULT)
        {
            IsRequired = _IS_REQUIRED_DEFAULT;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentRouting))
                return false;

            if (obj == null)
                return false;

            DocumentRouting routing = obj as DocumentRouting;
            if (this.Index == routing.Index && this.IsRequired == routing.IsRequired && this.Path.Equals(routing.Path) && this.Store == routing.Store)
            {
                return true;
            }

            return false;
        }
    }
}
