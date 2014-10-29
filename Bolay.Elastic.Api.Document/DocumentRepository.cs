using Bolay.Elastic.Api;
using Bolay.Elastic.Api.Document.Bulk;
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
        private readonly IElasticUriProvider _ElasticCluster;
        private readonly IHttpLayer _httpLayer;

        public DocumentRepository(IElasticUriProvider clusterUriProvider, IHttpLayer httpLayer)
        {
            if (clusterUriProvider == null || clusterUriProvider.Uri == null)
                throw new ArgumentNullException("clusterUriProvider");

            _ElasticCluster = clusterUriProvider;
            _httpLayer = httpLayer;
        }

        public MultiGetResponse<T> MultiGet(MultiGetDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "MultiGetDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster));
            HttpResponse response = _httpLayer.Get(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<MultiGetResponse<T>>(response.Body);

            // TODO: is this a good enough exception to throw?
            throw new ElasticRequestException(httpRequest, response);
        }

        public GetResponse<T> Get(GetDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "GetDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster));
            HttpResponse response = _httpLayer.Get(httpRequest);

            // TODO: should i throw an exception when document is not found, but no reason is given
            // this is relevant for bad document ids and document types...
            // i should ask around
            // when the index doesn't exist we get an error body
            // when the type or id don't exist we get a "normal" response with the _exists: false
            // what if I just returned the normal unless it was a "missing index"

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if(!request.ExcludeMetaData)
                    return JsonConvert.DeserializeObject<GetResponse<T>>(response.Body);

                T document = JsonConvert.DeserializeObject<T>(response.Body);
                return new GetResponse<T>() { Document = document };
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                GetResponse<T> notFoundResponse = JsonConvert.DeserializeObject<GetResponse<T>>(response.Body);
                if (notFoundResponse != null)
                    return notFoundResponse;

                if (response.Body.Contains("IndexMissingException"))
                    throw new IndexMissingException(httpRequest, response);
            }

            throw ExceptionGenerator.GenerateException(httpRequest, response);
        }

        public DoesExistResponse DoesExist(DoesExistDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "DoesExistDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster));
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

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster), request.Document);
            HttpResponse response = _httpLayer.Post(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<IndexResponse>(response.Body);

            throw new ElasticRequestException(httpRequest, response);
        }

        public UpdateResponse Update(UpdateDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "UpdateDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster), request.Content);
            HttpResponse response = _httpLayer.Put(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<UpdateResponse>(response.Body);

            throw new ElasticRequestException(httpRequest, response);
        }

        public DeleteResponse Delete(DeleteDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "DeleteDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster));
            HttpResponse response = _httpLayer.Delete(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<DeleteResponse>(response.Body);

            throw new ElasticRequestException(httpRequest, response);
        }

        public BulkResponse BulkRequest(Bulk.BulkDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "BulkDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster), request.BuildContent());
            HttpResponse response = _httpLayer.Post(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IEnumerable<AdminElasticResponse> results = JsonConvert.DeserializeObject<IEnumerable<AdminElasticResponse>>(response.Body);

                if (results == null || !results.Any())
                    return null;

                return new BulkResponse() { Results = results };
            }

            throw new ElasticRequestException(httpRequest, response);
        }

        public DeleteByQueryResponse DeleteByQuery(DeleteByQueryDocumentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "BulkDocumentRequest is required.");

            HttpRequest httpRequest = new HttpRequest(request.BuildUri(_ElasticCluster), request.ContentQuery);
            HttpResponse response = _httpLayer.Delete(httpRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<DeleteByQueryResponse>(response.Body);
            }                

            throw new ElasticRequestException(httpRequest, response);
        }
    }
}
