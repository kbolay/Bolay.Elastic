using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Type
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-type-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentTypeSerializer))]
    public class DocumentType : MappingBase
    {
        private static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.NotAnalyzed;
        private const bool _STORE_DEFAULT = false;

        /// <summary>
        /// Create a _type mapping.
        /// </summary>
        public DocumentType() : base(_INDEX_DEFAULT, _STORE_DEFAULT) { }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentType))
                return false;

            if (obj == null)
                return false;

            DocumentType type = obj as DocumentType;
            if (this.Index == type.Index && this.Store == type.Store)
                return true;

            return false;
        }
    }
}
