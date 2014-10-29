using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Interfaces;
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
        private const string _ROUTING_KEY = "routing";
        private const string _REPLICATION_KEY = "replication";
        private const string _CONSISTENCY_KEY = "consistency";

        private const string _QUERY_VALUE = "_query";
        private const string _REPLICATION_VALUE = "async";

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
        }

        public override Uri BuildUri(IElasticUriProvider clusterUriProvider)
        {
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append(Index);
            pathBuilder.Append("/");

            if(string.IsNullOrWhiteSpace(DocumentType))
            {
                pathBuilder.Append(DocumentType);
                pathBuilder.Append("/");
            }

            pathBuilder.Append(_QUERY_VALUE);

            return new Uri(clusterUriProvider.ClusterUri, pathBuilder.ToString());
        }

        public override string BuildQueryString()
        {
            if (QueryString == null)
                return null;

            StringBuilder builder = new StringBuilder();
            builder = HttpRequest.AddToQueryString(builder, QueryStringSearch._QUERY_KEY, QueryString.Query);

            if (!string.IsNullOrWhiteSpace(QueryString.Analyzer))
            {
                builder = HttpRequest.AddToQueryString(builder, QueryStringSearch._ANALYZER_KEY, QueryString.Analyzer);
            }
            if (!string.IsNullOrWhiteSpace(QueryString.DefaultField))
            {
                builder = HttpRequest.AddToQueryString(builder, QueryStringSearch._DEFAULT_FIELD_KEY, QueryString.DefaultField);
            }
            if (QueryString.UseAndForDefaultOperator)
            {
                builder = HttpRequest.AddToQueryString(builder, QueryStringSearch._DEFAULT_OPERATOR_KEY, QueryStringSearch._AND_OPERATOR_VALUE);
            }
            if (!string.IsNullOrWhiteSpace(Routing))
            {
                builder = HttpRequest.AddToQueryString(builder, _ROUTING_KEY, Routing);
            }
            if (WriteConsistency == null)
            {
                builder = HttpRequest.AddToQueryString(builder, _CONSISTENCY_KEY, WriteConsistency.ToString());
            }
            if (UseAsyncReplication)
            {
                builder = HttpRequest.AddToQueryString(builder, _REPLICATION_KEY, _REPLICATION_VALUE);
            }

            if (builder.Length == 0)
                return null;

            return builder.ToString();
        }
    }
}
