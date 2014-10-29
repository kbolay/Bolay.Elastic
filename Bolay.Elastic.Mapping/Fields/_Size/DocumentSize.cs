using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Size
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-size-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentSizeSerializer))]
    public class DocumentSize
    {
        internal const bool _IS_ENABLED_DEFAULT = false;
        internal const bool _STORE_DEFAULT = false;

        /// <summary>
        /// Gets or sets whether the document _source size is indexed.
        /// Defaults to false.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether the document _source size is stored.
        /// Defaults to false.
        /// </summary>
        public bool Store { get; set; }

        /// <summary>
        /// Create settings for document size indexing, and storing. Sets IsEnabled to true.
        /// </summary>
        public DocumentSize()
        {
            IsEnabled = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentSize))
                return false;

            if (obj == null)
                return false;

            DocumentSize size = obj as DocumentSize;

            if (this.IsEnabled == size.IsEnabled && this.Store == size.Store)
            {
                return true;
            }

            return false;
        }
    }
}
