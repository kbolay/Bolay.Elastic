using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Source
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-source-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentSourceSerializer))]
    public class DocumentSource
    {
        internal const bool _IS_ENABLED_DEFAULT = true;
        internal const bool _IS_COMPRESSED_DEFAULT = false;

        /// <summary>
        /// Gets or sets the enabled property.
        /// Defaults to true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the compress property.
        /// Defaults to false.
        /// </summary>
        public bool IsCompressed { get; set; }

        /// <summary>
        /// The size threshold for compression.
        /// </summary>
        public CompressionThreshold CompressionThreshold { get; set; }

        /// <summary>
        /// A list of items to include in the source.
        /// </summary>
        public IEnumerable<string> Includes { get; set; }

        /// <summary>
        /// A list of items to exclude from the source.
        /// </summary>
        public IEnumerable<string> Excludes { get; set; }

        public DocumentSource() 
        {
            IsEnabled = _IS_ENABLED_DEFAULT;
            IsCompressed = _IS_COMPRESSED_DEFAULT;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentSource))
                return false;

            if (obj == null)
                return false;

            DocumentSource source = obj as DocumentSource;
            if (this.CompressionThreshold.Equals(source.CompressionThreshold) && this.Excludes == source.Excludes && this.Includes == source.Includes && this.IsCompressed == source.IsCompressed && this.IsEnabled == source.IsEnabled)
            {
                return true;
            }

            return false;
        }
    }
}