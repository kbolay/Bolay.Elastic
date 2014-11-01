using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Models;
using Bolay.Elastic.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Index
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html
    /// </summary>
    public class IndexDocumentRequest<T> : DocumentRequestBase
    {
        internal const string VERSION_KEY = "version";
        internal const string OPERATION_TYPE_KEY = "op_type";
        internal const string PARENT_ID_KEY = "parent";
        internal const string TIMESTAMP_KEY = "timestamp";
        internal const string TIME_TO_LIVE_KEY = "ttl";
        internal const string ROUTING_KEY = "routing";
        internal const string PERCOLATE_KEY = "percolate";
        internal const string WRITE_CONSISTENCY_KEY = "consistency";
        internal const string ASYNCHRONOUS_REPLICATION_KEY = "replication";
        internal const string REFRESH_KEY = "refresh";
        internal const string OPERATION_TIMEOUT_KEY = "timeout";
        internal const string VERSION_TYPE_KEY = "version_type";

        internal const string ASYNCHRONOUS_REPLICATION_VALUE = "async";
        internal const string TIMESTAMP_FORMAT = "yyyy-MM-ddTHH:mm:ss";
        internal const string CREATE_OPERATION = "create";
        internal static readonly VersionTypeEnum VERSION_TYPE_DEFAULT = VersionTypeEnum.Internal;
        internal static readonly WriteConsistencyEnum WRITE_CONSISTENCY_DEFAULT = WriteConsistencyEnum.QuorumOfShards;

        private Int64? _Version { get; set; }
        private TimeValue _TimeToLive { get; set; }

        /// <summary>
        /// Only affective if version_type of the index has been set to external.
        /// Allows implementation of http://en.wikipedia.org/wiki/Optimistic_concurrency_control.
        /// </summary>
        public Int64? Version 
        {
            get { return _Version; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentOutOfRangeException("Version", "Document version must be greater than 0.");

                _Version = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of versioning for the document to use.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html#_version_types
        /// </summary>
        public VersionTypeEnum VersionType { get; set; }

        /// <summary>
        /// This allows for "put-if-absent" behavior. When create is used, 
        /// the index operation will fail if a document by that id already 
        /// exists in the index.
        /// </summary>
        public bool UseCreateOperationType { get; set; }

        /// <summary>
        /// A child document can be indexed by specifying it’s parent when indexing.
        /// When indexing a child document, the routing value is automatically 
        /// set to be the same as it’s parent, unless the routing value is explicitly 
        /// specified using the routing parameter.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// A document can be indexed with a timestamp associated with it. 
        /// The timestamp value of a document can be set using the timestamp parameter.
        /// If the timestamp value is not provided externally or in the _source, 
        /// the timestamp will be automatically set to the date the document 
        /// was processed by the indexing chain.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-timestamp-field.html
        /// </summary>
        public DateTime? TimeStamp { get; set; }

        /// <summary>
        /// A document can be indexed with a ttl (time to live) 
        /// associated with it. Expired documents will be expunged automatically. 
        /// The expiration date that will be set for a document with a provided 
        /// ttl is relative to the timestamp of the document, meaning it can be 
        /// based on the time of indexing or on any time provided. This value must be positive.
        /// </summary>
        public TimeValue TimeToLive 
        {
            get { return _TimeToLive; }
            set 
            {
                if (value != null && value.TimeSpan.TotalMilliseconds <= 0)
                    throw new ArgumentOutOfRangeException("TimeToLive", "Document TimeToLive must be a positive value.");

                _TimeToLive = value;
            } 
        }

        /// <summary>
        /// By default, shard placement — or routing — is controlled by 
        /// using a hash of the document’s id value. For more explicit control, 
        /// the value fed into the hash function used by the router can be 
        /// directly specified on a per-operation basis using the routing parameter.
        /// If the _routing mapping is defined, and set to be required, the index 
        /// operation will fail if no routing value is provided or extracted.
        /// </summary>
        public string Routing { get; set; }

        /// <summary>
        /// To prevent writes from taking place on the "wrong" side 
        /// of a network partition, by default, index operations only 
        /// succeed if a quorum (>replicas/2+1) of active shards are available. 
        /// This default can be overridden on a node-by-node basis using the 
        /// action.write_consistency setting. To alter this behavior per-operation, 
        /// this parameter can be used.
        /// </summary>
        public WriteConsistencyEnum WriteConsistency { get; set; }

        /// <summary>
        /// By default, the index operation only returns after all shards 
        /// within the replication group have indexed the document (sync replication). 
        /// To enable asynchronous replication, causing the replication process to 
        /// take place in the background, set the replication parameter to async. 
        /// When asynchronous replication is used, the index operation will return 
        /// as soon as the operation succeeds on the primary shard.
        /// </summary>
        public bool UseAsynchronousReplication { get; set; }

        /// <summary>
        /// To refresh the index immediately after the operation occurs, 
        /// so that the document appears in search results immediately, 
        /// the refresh parameter can be set to true. Setting this option 
        /// to true should ONLY be done after careful thought and verification 
        /// that it does not lead to poor performance, both from an indexing 
        /// and a search standpoint. Note, getting a document using the get 
        /// API is completely realtime.
        /// </summary>
        public bool Refresh { get; set; }

        /// <summary>
        /// The primary shard assigned to perform the index operation 
        /// might not be available when the index operation is executed. 
        /// Some reasons for this might be that the primary shard is 
        /// currently recovering from a gateway or undergoing relocation. 
        /// By default, the index operation will wait on the primary shard 
        /// to become available for up to 1 minute before failing and 
        /// responding with an error. This parameter can be used 
        /// to explicitly specify how long it waits.
        /// </summary>
        public TimeSpan? OperationTimeOut { get; set; }

        public readonly T Document;

        public IndexDocumentRequest(string index, string documentType, T document, string documentId = null)
        { 
            // TODO: may have a problem when not specifying the document type...
            // not sure if i am supposed to wrap the document in its type name in that case or not?
            if(string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "ES cluster index must be specified.");
            if(string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "Document Type must be specified in order to index a document.");
            if (document == null)
                throw new ArgumentNullException("document", "There must be a document to be indexed.");

            Index = index;
            DocumentType = documentType;
            DocumentId = documentId;
            Document = document;
            WriteConsistency = WriteConsistencyEnum.QuorumOfShards;
        }
    }
}
