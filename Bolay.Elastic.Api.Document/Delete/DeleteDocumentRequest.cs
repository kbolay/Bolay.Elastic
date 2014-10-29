﻿using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Delete
{
    public class DeleteDocumentRequest : DocumentRequestBase
    {
        private const string _VERSION_KEY = "version";
        private const string _PARENT_ID_KEY = "parent";
        private const string _ROUTING_KEY = "routing";
        private const string _WRITE_CONSISTENCY_KEY = "consistency";
        private const string _ASYNCHRONOUS_REPLICATION_KEY = "replication";
        private const string _REFRESH_KEY = "refresh";
        private const string _OPERATION_TIMEOUT_KEY = "timeout";

        private const string _ASYNCHRONOUS_REPLICATION_VALUE = "async";

        private Int64? _Version { get; set; }

        /// <summary>
        /// Specify the version to ensure the correct document gets deleted.
        /// </summary>
        public Int64? Version 
        {
            get { return _Version; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentOutOfRangeException("Version", "Document version must be greater than 0 for DELETE.");

                _Version = value;
            }
        }

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

        public DeleteDocumentRequest(string index, string documentType, string documentId)
        { 
            if(string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "ES cluster index must be specified.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "ES index document type must be specified.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "Document Id must be specified.");

            Index = index;
            DocumentType = documentType;
            DocumentId = documentId;
        }

        public override Uri BuildUri(Interfaces.IElasticUriProvider uriProvider)
        {
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append(Index);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentType);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentId);

            return new Uri(uriProvider.ClusterUri, pathBuilder.ToString());
        }

        public override string BuildQueryString()
        {
            StringBuilder builder = new StringBuilder();

            if (Version.HasValue) 
            {
                builder = HttpRequest.AddToQueryString(builder, _VERSION_KEY, Version.Value.ToString()); 
            }

            if (!string.IsNullOrWhiteSpace(ParentId)) 
            { 
                builder = HttpRequest.AddToQueryString(builder, _PARENT_ID_KEY, ParentId); 
            }

            if (!string.IsNullOrWhiteSpace(Routing)) 
            { 
                builder = HttpRequest.AddToQueryString(builder, _ROUTING_KEY, Routing); 
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
                builder = HttpRequest.AddToQueryString(builder, _REFRESH_KEY, Refresh.ToString().ToLower()); 
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
