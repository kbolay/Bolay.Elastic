using Bolay.Elastic.Mapping.Properties.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Nested
{
    /// <summary>
    /// Nested objects/documents can prevent "cross object" search matches.
    /// Requires use of the nested query or nested filter.
    /// http://www.elasticsearch.org/guide/reference/mapping/nested-type/
    /// </summary>
    [JsonConverter(typeof(NestedObjectPropertySerializer))]
    public class NestedObjectProperty : ObjectProperty
    {
        internal const bool _INCLUDE_IN_PARENT_DEFAULT = false;
        internal const bool _INCLUDE_IN_ROOT_DEFAULT = false;

        /// <summary>
        /// Gets or sets whether to automatically add to the immediate parent.
        /// </summary>
        public bool IncludeInParent { get; set; }

        /// <summary>
        /// Gets or sets whether to automatically add to the root document.
        /// </summary>
        public bool IncludeInRoot { get; set; }

        /// <summary>
        /// Establish defaults.
        /// </summary>
        /// <param name="name">Sets the name.</param>
        public NestedObjectProperty(string name) : base(name, PropertyTypeEnum.Nested)
        {
            IncludeInParent = _INCLUDE_IN_PARENT_DEFAULT;
            IncludeInRoot = _INCLUDE_IN_ROOT_DEFAULT;
        }
    }
}
