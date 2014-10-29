using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Ttl
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-ttl-field.html
    /// </summary>
    [JsonConverter(typeof(DocumentTimeToLiveSerializer))]
    public class DocumentTimeToLive : MappingBase
    {
        internal const bool _IS_ENABLED_DEFAULT = false;
        internal static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.NotAnalyzed;
        internal const bool _STORE_DEFAULT = true;

        /// <summary>
        /// Gets or sets the enabled flag. Automatically set to true on creation of this object.
        /// Defaults to false in ES.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets the default time to live for documents that this mapping is applied to.
        /// </summary>
        public TimeValue DefaultTimeToLive { get; private set; }

        /// <summary>
        /// Creates a default time to live for the documents that this mapping applies to. Sets IsEnabled to true.
        /// </summary>
        /// <param name="defaultTimeToLive">Sets the DefaultTimeToLive.</param>
        public DocumentTimeToLive(TimeValue defaultTimeToLive)
            : base(_INDEX_DEFAULT, _STORE_DEFAULT)
        {
            if (defaultTimeToLive == null)
                throw new ArgumentNullException("defaultTimeToLive", "DocumentTimeToLive requires a TimeValue.");

            DefaultTimeToLive = defaultTimeToLive;
            IsEnabled = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DocumentTimeToLive))
                return false;

            if (obj == null)
                return false;

            DocumentTimeToLive ttl = obj as DocumentTimeToLive;
            if (this.DefaultTimeToLive.Equals(ttl.DefaultTimeToLive) && this.Index == ttl.Index && this.IsEnabled == ttl.IsEnabled && this.Store == ttl.Store)
            {
                return true;
            }

            return false;
        }
    }
}
