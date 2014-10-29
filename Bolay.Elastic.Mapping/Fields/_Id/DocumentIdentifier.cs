using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Id
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-id-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentIdentifierSerializer))]
    public class DocumentIdentifier : MappingBase
    {
        private static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.No;
        private const bool _STORE_DEFAULT = false;

        /// <summary>
        /// Gets or sets the path of the property to be used as the value for the _id.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Creates the _id mapping value.
        /// </summary>
        public DocumentIdentifier() : base(_INDEX_DEFAULT, _STORE_DEFAULT) { }

        public override bool Equals(object obj)
        {
            // TODO: throw an exception here?
            if (!(obj is DocumentIdentifier))
                return false;

            if (obj == null)
                return false;

            DocumentIdentifier docId = obj as DocumentIdentifier;
            if (this.Index == docId.Index && this.Path.Equals(docId.Path) && this.Store == docId.Store)
            {
                return true;
            }

            return false;
        }
    }
}
