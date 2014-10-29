using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Index
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-index-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentIndexSerializer))]
    public class DocumentIndex
    {
        internal const bool _IS_ENABLED_DEFAULT = false;

        /// <summary>
        /// Gets or sets the enabled value. This value indicates if the index the document belongs to is stored in the document.
        /// Defaults to false.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Create the _index field setting enabled to true.
        /// </summary>
        public DocumentIndex()
        {
            IsEnabled = _IS_ENABLED_DEFAULT;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentIndex))
                return false;

            if (obj == null)
                return false;

            DocumentIndex index = obj as DocumentIndex;
            if (this.IsEnabled.Equals(index.IsEnabled))
                return true;

            return false;
        }
    }
}