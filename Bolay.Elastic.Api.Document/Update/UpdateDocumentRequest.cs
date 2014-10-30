using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Interfaces;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Update
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html
    /// </summary>
    public class UpdateDocumentRequest : DocumentRequestBase
    {
        private const string _UPDATE_OPERATION = "_update";

        private const string _PARENT_ID_KEY = "parent";
        private const string _ROUTING_KEY = "routing";
        private const string _PERCOLATE_KEY = "percolate";
        private const string _WRITE_CONSISTENCY_KEY = "consistency";
        private const string _ASYNCHRONOUS_REPLICATION_KEY = "replication";
        private const string _REFRESH_KEY = "refresh";
        private const string _OPERATION_TIMEOUT_KEY = "timeout";
        private const string _FIELDS_KEY = "fields";
        private const string _RETRY_ON_CONFLICT_KEY = "retry_on_conflict";

        private const string _ASYNCHRONOUS_REPLICATION_VALUE = "async";

        private TimeSpan? _TimeToLive { get; set; }
        private int? _MaximumRetryAttempts { get; set; }

        /// <summary>
        /// A child document can be indexed by specifying it’s parent when indexing.
        /// When indexing a child document, the routing value is automatically 
        /// set to be the same as it’s parent, unless the routing value is explicitly 
        /// specified using the routing parameter.
        /// </summary>
        public string ParentId { get; set; }

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

        /// <summary>
        /// The fields of the document to return.
        /// Example: ['prop1', 'prop2', 'prop3.prop1']
        /// _source to return the full document.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// The number of times to retry the update when there is a version conflict.
        /// Defaults to 0.
        /// </summary>
        public int? MaximumRetryAttempts 
        {
            get { return _MaximumRetryAttempts; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                {
                    throw new ArgumentOutOfRangeException("MaximumRetryAttempts", "Maximum retry attempts must be greater than zero.");
                }

                _MaximumRetryAttempts = value;
            }
        }

        /// <summary>
        /// The body of the update request.
        /// </summary>
        public UpdateContent Content { get; private set; }

        public UpdateDocumentRequest(string index, string documentType, string documentId, UpdateContent content)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "The index value must be populated.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "The documentId value must be populated.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "The documentType value must be populated.");
            if (content == null || content.IsNull)
                throw new ArgumentNullException("content", "The update document content have the script of partial document populated.");

            Index = index;
            DocumentId = documentId;
            DocumentType = documentType;
            Content = content;
        }

        public override Uri BuildUri(IElasticUriProvider clusterUriProvider)
        {
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append(Index);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentType);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentId);
            pathBuilder.Append("/");
            pathBuilder.Append(_UPDATE_OPERATION);
            
            pathBuilder.Append(BuildQueryString());

            return new Uri(clusterUriProvider.ClusterUri, pathBuilder.ToString());
        }

        public override string BuildQueryString()
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(ParentId))
            {
                builder = HttpRequest.AddToQueryString(builder, _PARENT_ID_KEY, ParentId);
            }

            if (!string.IsNullOrWhiteSpace(Routing))
            {
                builder = HttpRequest.AddToQueryString(builder, _ROUTING_KEY, Routing);
            }

            if (Fields != null && Fields.Any())
            {
                builder = HttpRequest.AddToQueryString(builder, _FIELDS_KEY, string.Join(",", Fields));
            }

            if (WriteConsistency != null)
            {
                builder = HttpRequest.AddToQueryString(builder, _WRITE_CONSISTENCY_KEY, WriteConsistency.ToString());
            }

            if (UseAsynchronousReplication)
            {
                builder = HttpRequest.AddToQueryString(builder, _ASYNCHRONOUS_REPLICATION_KEY, _ASYNCHRONOUS_REPLICATION_VALUE);
            }

            if (Refresh)
            {
                builder = HttpRequest.AddToQueryString(builder, _REFRESH_KEY, Refresh.ToString());
            }

            if (OperationTimeOut.HasValue)
            {
                builder = HttpRequest.AddToQueryString(builder, _OPERATION_TIMEOUT_KEY, Convert.ToInt64(OperationTimeOut.Value.TotalMilliseconds).ToString());
            }

            if (builder.Length == 0)
                return null;

            return builder.ToString();
        }
    }
}
