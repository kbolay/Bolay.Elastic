using Bolay.Elastic.Api;
using Bolay.Elastic.Api.Document.Delete;
using Bolay.Elastic.Api.Document.DeleteByQuery;
using Bolay.Elastic.Api.Document.Exist;
using Bolay.Elastic.Api.Document.Get;
using Bolay.Elastic.Api.Document.Index;
using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.Document.MultiGet;
using Bolay.Elastic.Api.Document.Update;
using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocumentRepository<T> : IDocumentRepository<T>
    {
        private readonly IElasticUriProvider _uriProvider;
        private readonly IHttpLayer _httpLayer;

        public DocumentRepository(IElasticUriProvider uriProvider, IHttpLayer httpLayer)
        {
            if (uriProvider == null || uriProvider.Uri == null)
            {
                throw new ArgumentNullException("uriProvider");
            }

            if (httpLayer == null)
            {
                throw new ArgumentNullException("httpLayer");
            }

            _uriProvider = uriProvider;
            _httpLayer = httpLayer;
        }

        public MultiGetResponse<T> MultiGet(MultiGetDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "MultiGetDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri, request.Content);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, MultiGetDocumentRequest.MULTI_GET_VALUE);
            httpRequest.AddToQueryString(MultiGetDocumentRequest.ROUTING_KEY, request.Routing);
            HttpResponse response = _httpLayer.Get(httpRequest);
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // TODO: is this a good enough exception to throw?
                throw new ElasticRequestException(httpRequest, response);
            }

            return JsonConvert.DeserializeObject<MultiGetResponse<T>>(response.Body);
        }

        public GetResponse<T> Get(GetDocumentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "GetDocumentRequest is required.");
            }
            
            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, request.DocumentId, request.ExcludeMetaData ? GetDocumentRequest.EXCLUDE_METADATA_DEFAULT : null);
            httpRequest.AddToQueryString(GetDocumentRequest.REALTIME_KEY, request.DisableRealTime.ToString(), defaultValue: GetDocumentRequest.REALTIME_DEFAULT);

            if(request.Fields != null && request.Fields.Any())
            {
                httpRequest.AddToQueryString(GetDocumentRequest.FIELDS_KEY, string.Join(",", request.Fields));
            }

            httpRequest.AddToQueryString(GetDocumentRequest.ROUTING_KEY, request.Routing);
            httpRequest.AddToQueryString(GetDocumentRequest.SHARD_PREFERENCE_KEY, request.ShardPreference != null ? request.ShardPreference.ToString() : null);
            httpRequest.AddToQueryString(GetDocumentRequest.REFRESH_KEY, request.RefreshBeforeSearch.ToString(), defaultValue: GetDocumentRequest.REFRESH_DEFAULT);

            HttpResponse response = _httpLayer.Get(httpRequest);

            // TODO: should i throw an exception when document is not found, but no reason is given
            // this is relevant for bad document ids and document types...
            // i should ask around
            // when the index doesn't exist we get an error body
            // when the type or id don't exist we get a "normal" response with the _exists: false
            // what if I just returned the normal unless it was a "missing index"

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!request.ExcludeMetaData)
                {
                    return JsonConvert.DeserializeObject<GetResponse<T>>(response.Body);
                }                    

                T document = JsonConvert.DeserializeObject<T>(response.Body);
                return new GetResponse<T>() { Document = document };
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                GetResponse<T> notFoundResponse = JsonConvert.DeserializeObject<GetResponse<T>>(response.Body);
                if (notFoundResponse != null)
                {
                    return notFoundResponse;
                }

                if (response.Body.Contains("IndexMissingException"))
                {
                    throw new IndexMissingException(httpRequest, response);
                }
            }

            throw ExceptionGenerator.GenerateException(httpRequest, response);
        }

        public DoesExistResponse DoesExist(DoesExistDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "DoesExistDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, request.DocumentId, request.ExcludeMetaData ? DoesExistDocumentRequest.EXCLUDE_METADATA_VALUE : null);
            HttpResponse response = _httpLayer.Head(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<DoesExistResponse>(response.Body);
            }

            throw new ElasticRequestException(httpRequest, response);
        }

        public IndexResponse Index(IndexDocumentRequest<T> request)
        {            
            if (request == null)
                throw new ArgumentNullException("request", "IndexDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri, request.Document);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, request.DocumentId);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.VERSION_KEY, request.Version.HasValue ? request.Version.Value.ToString() : null);
            httpRequest.AddToQueryString(
                IndexDocumentRequest<T>.VERSION_TYPE_KEY,
                request.VersionType != null ? request.VersionType.ToString() : IndexDocumentRequest<T>.VERSION_TYPE_DEFAULT.ToString(),
                defaultValue: IndexDocumentRequest<T>.VERSION_TYPE_DEFAULT.ToString());
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.OPERATION_TYPE_KEY, request.UseCreateOperationType ? IndexDocumentRequest<T>.CREATE_OPERATION : null);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.PARENT_ID_KEY, request.ParentId);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.TIMESTAMP_KEY, request.TimeStamp.HasValue ? request.TimeStamp.Value.ToString(IndexDocumentRequest<T>.TIMESTAMP_FORMAT) : null);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.TIME_TO_LIVE_KEY, request.TimeToLive != null ? request.TimeToLive.ToString() : null);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.ROUTING_KEY, request.Routing);
            httpRequest.AddToQueryString(
                IndexDocumentRequest<T>.WRITE_CONSISTENCY_KEY, 
                request.WriteConsistency != null ? request.WriteConsistency.ToString() : IndexDocumentRequest<T>.WRITE_CONSISTENCY_DEFAULT.ToString(), 
                defaultValue: IndexDocumentRequest<T>.WRITE_CONSISTENCY_DEFAULT.ToString());
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.ASYNCHRONOUS_REPLICATION_KEY, request.UseAsynchronousReplication ? IndexDocumentRequest<T>.ASYNCHRONOUS_REPLICATION_VALUE : null);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.REFRESH_KEY, request.Refresh ? request.Refresh.ToString() : null);
            httpRequest.AddToQueryString(IndexDocumentRequest<T>.OPERATION_TIMEOUT_KEY, request.OperationTimeOut.HasValue ? request.OperationTimeOut.Value.TotalMilliseconds.ToString() : null);

            HttpResponse response = _httpLayer.Post(httpRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.Created && response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ElasticRequestException(httpRequest, response);
            }
            
            return JsonConvert.DeserializeObject<IndexResponse>(response.Body);
        }

        public UpdateResponse Update(UpdateDocumentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "UpdateDocumentRequest is required.");
            }                

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri, request.Content);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, request.DocumentId, UpdateDocumentRequest.UPDATE_OPERATION);
            httpRequest.AddToQueryString(UpdateDocumentRequest.PARENT_ID_KEY, request.ParentId);
            httpRequest.AddToQueryString(UpdateDocumentRequest.ROUTING_KEY, request.Routing);
            httpRequest.AddToQueryString(UpdateDocumentRequest.FIELDS_KEY, request.Fields != null ? string.Join(",", request.Fields) : null);
            httpRequest.AddToQueryString(UpdateDocumentRequest.WRITE_CONSISTENCY_KEY, request.WriteConsistency.ToString(), defaultValue: UpdateDocumentRequest.WRITE_CONSISTENCY_DEFAULT.ToString());
            httpRequest.AddToQueryString(UpdateDocumentRequest.ASYNCHRONOUS_REPLICATION_KEY, request.UseAsynchronousReplication ? UpdateDocumentRequest.ASYNCHRONOUS_REPLICATION_VALUE : null);
            httpRequest.AddToQueryString(UpdateDocumentRequest.REFRESH_KEY, request.Refresh.ToString(), defaultValue: UpdateDocumentRequest.REFRESH_DEFAULT);
            httpRequest.AddToQueryString(UpdateDocumentRequest.OPERATION_TIMEOUT_KEY, request.OperationTimeOut.HasValue ? request.OperationTimeOut.Value.TotalMilliseconds.ToString() : null);

            HttpResponse response = _httpLayer.Put(httpRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ElasticRequestException(httpRequest, response);
            }

            return JsonConvert.DeserializeObject<UpdateResponse>(response.Body);
        }

        public DeleteResponse Delete(DeleteDocumentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "DeleteDocumentRequest is required.");
            }               

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, request.DocumentId);
            httpRequest.AddToQueryString(DeleteDocumentRequest.VERSION_KEY, request.Version.HasValue ? request.Version.Value.ToString() : null);
            httpRequest.AddToQueryString(DeleteDocumentRequest.PARENT_ID_KEY, request.ParentId);
            httpRequest.AddToQueryString(DeleteDocumentRequest.ROUTING_KEY, request.Routing);
            httpRequest.AddToQueryString(DeleteDocumentRequest.WRITE_CONSISTENCY_KEY, request.WriteConsistency.ToString(), defaultValue: DeleteDocumentRequest.WRITE_CONSISTENCY_DEFAULT.ToString());
            httpRequest.AddToQueryString(DeleteDocumentRequest.ASYNCHRONOUS_REPLICATION_KEY, request.UseAsynchronousReplication ? DeleteDocumentRequest.ASYNCHRONOUS_REPLICATION_VALUE : null);
            httpRequest.AddToQueryString(DeleteDocumentRequest.REFRESH_KEY, request.Refresh.ToString(), defaultValue: DeleteDocumentRequest.REFRESH_DEFAULT);
            httpRequest.AddToQueryString(DeleteDocumentRequest.OPERATION_TIMEOUT_KEY, request.OperationTimeOut.HasValue ? request.OperationTimeOut.Value.TotalMilliseconds.ToString() : null);

            HttpResponse response = _httpLayer.Delete(httpRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ElasticRequestException(httpRequest, response);
            }
            
            return JsonConvert.DeserializeObject<DeleteResponse>(response.Body);
        }

        public DeleteByQueryResponse DeleteByQuery(DeleteByQueryDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "BulkDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(_uriProvider.Uri, request.ContentQuery);
            httpRequest.ChangeUriPath(request.Index, request.DocumentType, DeleteByQueryDocumentRequest.QUERY_VALUE);

            if (request.QueryString != null)
            {
                httpRequest.AddToQueryString(QueryStringSearch.QUERY_KEY, request.QueryString.Query);
                httpRequest.AddToQueryString(QueryStringSearch.ANALYZER_KEY, request.QueryString.Analyzer);
                httpRequest.AddToQueryString(QueryStringSearch.DEFAULT_FIELD_KEY, request.QueryString.DefaultField);
                httpRequest.AddToQueryString(QueryStringSearch.DEFAULT_OPERATOR_KEY, request.QueryString.UseAndForDefaultOperator ? QueryStringSearch.AND_OPERATOR_VALUE : null);
            }

            httpRequest.AddToQueryString(DeleteByQueryDocumentRequest.ROUTING_KEY, request.Routing);
            httpRequest.AddToQueryString(DeleteByQueryDocumentRequest.CONSISTENCY_KEY, request.WriteConsistency != null ? request.WriteConsistency.ToString() : null, defaultValue: DeleteByQueryDocumentRequest.WRITE_CONSISTENCY_DEFAULT.ToString());
            httpRequest.AddToQueryString(DeleteByQueryDocumentRequest.REPLICATION_KEY, request.UseAsyncReplication ? DeleteByQueryDocumentRequest.REPLICATION_VALUE : null);

            HttpResponse response = _httpLayer.Delete(httpRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ElasticRequestException(httpRequest, response);
            }

            return JsonConvert.DeserializeObject<DeleteByQueryResponse>(response.Body);
        }
    }
}
