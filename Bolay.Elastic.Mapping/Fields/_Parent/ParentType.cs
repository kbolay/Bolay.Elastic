using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Parent
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-parent-field.html
    /// </summary>
    [JsonConverter(typeof(ParentTypeSerializer))]
    public class ParentType
    {
        /// <summary>
        /// Gets the type of the parent document.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Creates the _parent value.
        /// </summary>
        /// <param name="type">Sets the type of the parent document.</param>
        public ParentType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException("type", "ParentType requires a type.");

            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ParentType))
                return false;

            if (obj == null)
                return false;

            ParentType parent = obj as ParentType;

            if (this.Type.Equals(parent.Type))
                return true;

            return false;
        }
    }
}
