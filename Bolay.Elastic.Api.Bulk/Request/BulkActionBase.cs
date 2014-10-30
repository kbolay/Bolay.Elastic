using Bolay.Elastic.Models;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    [JsonConverter(typeof(BulkActionBaseSerializer))]
    public abstract class BulkActionBase
    {
        internal const string INDEX = "_index";
        internal const string TYPE = "_type";
        internal const string DOCUMENT_ID = "_id";
        internal const string VERSION = "_version";
        internal const string ROUTING = "_routing";
        internal const string PARENT = "_parent";
        internal const string TIMESTAMP = "_timestamp";
        internal const string TIME_TO_LIVE = "_ttl";

        /// <summary>
        /// Gets the action being performed.
        /// </summary>
        public abstract string Action { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public readonly string Index;

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public readonly string Type;

        /// <summary>
        /// Gets the document id.
        /// </summary>
        public readonly string DocumentId;

        /// <summary>
        /// Gets or sets version number of the document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-versioning
        /// </summary>
        public Int64? Version { get; set; }

        /// <summary>
        /// Gets or sets the routing value of the document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-routing
        /// </summary>
        public string Routing { get; set; }

        /// <summary>
        /// Gets or sets the id of the parent document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-parent
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-timestamp
        /// </summary>
        public DateTime? TimeStamp { get; set; }

        internal string TimeStampStr
        {
            get
            {
                if (TimeStamp.HasValue)
                {
                    return TimeStamp.Value.ToString(Bolay.Elastic.Time.DateTimeFormatEnum.Date.Format);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the time to live of the document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-ttl
        /// </summary>
        public TimeValue TimeToLive { get; set; }

        /// <summary>
        /// Create a bulk action.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the document type.</param>
        protected BulkActionBase(string index, string type) 
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("index");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("type");
            }

            Index = index;
            Type = type;
        }

        /// <summary>
        /// Create a bulk action.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the document type.</param>
        /// <param name="documentId">Sets the document id.</param>
        protected BulkActionBase(string index, string type, string documentId)
            : this(index, type)
        {
            if (string.IsNullOrWhiteSpace(documentId))
            {
                throw new ArgumentNullException("documentId");
            }

            DocumentId = documentId;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
