using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Timestamp
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-timestamp-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentTimestampSerializer))]
    public class DocumentTimestamp : MappingBase
    {
        internal const bool _IS_ENABLED_DEFAULT = false;
        internal static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.NotAnalyzed;
        internal const bool _STORE_DEFAULT = false;

        /// <summary>
        /// Gets or sets the enabled flag. This is set to true on the creation of this class.
        /// Defaults to false in ES.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the document path for the timestamp of the document.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the format of the datetime.
        /// Defaults to dateOptionalTime.
        /// </summary>
        public DateFormat Format { get; set; }

        /// <summary>
        /// Create the _timestamp. Constructor sets IsEnabled to true.
        /// </summary>
        public DocumentTimestamp()
            : base(_INDEX_DEFAULT, _STORE_DEFAULT)
        {
            IsEnabled = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentTimestamp))
                return false;

            if (obj == null)
                return false;

            DocumentTimestamp timestamp = obj as DocumentTimestamp;
            if (this.Format.Equals(timestamp.Format) && this.Index == timestamp.Index && this.IsEnabled == timestamp.IsEnabled && this.Path.Equals(timestamp.Path) && this.Store == timestamp.Store)
            {
                return true;
            }

            return false;
        }
    }
}
