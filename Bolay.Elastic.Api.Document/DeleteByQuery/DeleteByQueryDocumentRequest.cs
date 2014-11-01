using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Interfaces;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.DeleteByQuery
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html
    /// </summary>
    public class DeleteByQueryDocumentRequest : DocumentRequestBase
    {
        internal const string ROUTING_KEY = "routing";
        internal const string REPLICATION_KEY = "replication";
        internal const string CONSISTENCY_KEY = "consistency";

        internal const string QUERY_VALUE = "_query";
        internal const string REPLICATION_VALUE = "async";
        internal static readonly WriteConsistencyEnum WRITE_CONSISTENCY_DEFAULT = WriteConsistencyEnum.QuorumOfShards;

        /// <summary>
        /// A comma separated list of routing values.
        /// </summary>
        public string Routing { get; set; }

        /// <summary>
        /// Control if the operation will be allowed to execute based on the number of active shards within that partition.
        /// Defaults to quorum.
        /// </summary>
        public WriteConsistencyEnum WriteConsistency { get; set; }

        /// <summary>
        /// The operation will return once it has be executed on the primary shard.
        /// </summary>
        public bool UseAsyncReplication { get; set; }

        public QueryStringSearch QueryString { get; private set; }
        public string ContentQuery { get; private set; }

        public DeleteByQueryDocumentRequest(string index, QueryStringSearch queryString, string documentType = null)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "DeleteByQuery requires an index.");
            if (queryString == null || string.IsNullOrWhiteSpace(queryString.Query))
                throw new ArgumentNullException("queryString", "DeleteByQuery requires a query.");

            Index = index;
            QueryString = queryString;
            DocumentType = documentType;
            WriteConsistency = WriteConsistencyEnum.QuorumOfShards;
        }
        public DeleteByQueryDocumentRequest(string index, string contentQuery, string documentType = null)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "DeleteByQuery requires an index.");
            if (string.IsNullOrWhiteSpace(contentQuery))
                throw new ArgumentNullException("contentQuery", "DeleteByQuery requires a query.");

            ContentQuery = contentQuery;
            Index = index;
            DocumentType = documentType;
            WriteConsistency = WriteConsistencyEnum.QuorumOfShards;
        }
    }
}
