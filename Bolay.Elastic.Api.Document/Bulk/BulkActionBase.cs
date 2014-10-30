using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BulkActionBase
    {
        /// <summary>
        /// The _action of the bulk request.
        /// </summary>
        [JsonIgnore]
        public OperationTypeEnum OperationType { get; set; }

        /// <summary>
        /// The index to operate in. Part of the metadata.
        /// </summary>
        [JsonProperty("_index")]
        public string Index { get; set; }

        /// <summary>
        /// Specify the document type. Part of the metadata.
        /// </summary>
        [JsonProperty("_type")]
        public string DocumentType { get; set; }

        /// <summary>
        /// The _id of the document. Part of the metadata.
        /// </summary>
        [JsonProperty("_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// The _optional_source of this request.
        /// </summary>
        [JsonIgnore]
        public object Document { get; set; }

        /// <summary>
        /// The version of the document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-versioning
        /// </summary>
        [JsonProperty("version")]
        public Int64? Version { get; set; }

        [JsonProperty("routing")]
        public string Routing { get; set; }

        [JsonProperty("percolator")]
        public string Percolator { get; set; }

        [JsonProperty("parent")]
        public string ParentId { get; set; }

        /// <summary>
        /// The timestamp for the document. Use UTC DateTime values.
        /// </summary>
        [JsonIgnore]
        public DateTime? UtcTimeStamp { get; set; }

        /// <summary>
        /// Do not attempt to assign to this value it is strictly for 
        /// </summary>
        [JsonProperty("timestamp")]
        protected string TimeStamp
        {
            get
            {
                if(UtcTimeStamp.HasValue)
                    return UtcTimeStamp.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public TimeSpan? TimeToLive { get; set; }

        /// <summary>
        /// Assign TimeSpan to TimeToLive to make this work.
        /// Spits out the total milliseconds.
        /// </summary>
        [JsonProperty("ttl")]
        protected string TimeToLiveStr 
        { 
            get
            {
                if (TimeToLive.HasValue)
                    return Convert.ToInt64(TimeToLive.Value.TotalMilliseconds).ToString();

                return null;
            }
        }

        /// <summary>
        /// Determine the shard consistency required for this operation.
        /// Defaults to quorum.
        /// </summary>
        [JsonIgnore]
        public WriteConsistencyEnum WriteConsistency { get; set; }

        /// <summary>
        /// The serialization value, coming from WriteConsistency.
        /// </summary>
        [JsonProperty("consistency")]
        protected string Consistency
        {
            get
            {
                if (WriteConsistency == null)
                    return null;

                return WriteConsistency.ToString();
            }
        }

        /// <summary>
        /// Refresh the shard(s) immediately after the the operation.
        /// </summary>
        [JsonProperty("refresh")]
        public bool RefreshAfterOperation { get; set; }

        /// <summary>
        /// Only assign a value here if this is an update action.
        /// Must be greater than 0.
        /// The number of times to retry update operations when a conflict occurs.
        /// </summary>
        [JsonProperty("_retry_on_conflict")]
        [DefaultValue(default(int))]
        public int ConflictRetryAttempts { get; set; }
    }
}
