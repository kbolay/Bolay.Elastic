using Bolay.Elastic.Api.Bulk.Request;
using Bolay.Elastic.Api.Bulk.Response;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk
{
    public class BulkRepository : IBulkRepository
    {
        private const string _BULK_API_PATH = "_bulk";

        private readonly Uri _clusterUri;
        private readonly IHttpLayer _httpLayer;

        public BulkRepository(IUriProvider clusterUri, IHttpLayer httpLayer)
        {
            if (clusterUri == null || clusterUri.Uri == null)
            {
                throw new ArgumentNullException("serverUri", "Any bulk request requires the uri of the ES cluster.");
            }

            if (httpLayer == null)
            {
                throw new ArgumentNullException("httpLayer");
            }

            _clusterUri = clusterUri.Uri;
            _httpLayer = httpLayer;
        }

        public BulkResponse DoBulkRequest(BulkRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            HttpRequest httpRequest = new HttpRequest(_clusterUri, request.ToString());
            httpRequest.ChangeUriPath(_BULK_API_PATH);

            httpRequest.AddToQueryString(BulkRequest.REFRESH, request.Refresh.ToString(), defaultValue: false.ToString());
            httpRequest.AddToQueryString(
                BulkRequest.WRITE_CONSISTENCY,
                request.WriteConsistency != null ? request.WriteConsistency.ToString() : BulkRequest.WRITE_CONSISTENCY_DEFAULT.ToString(), 
                defaultValue: BulkRequest.WRITE_CONSISTENCY_DEFAULT.ToString());

            HttpResponse httpResponse = _httpLayer.Post(httpRequest);
            
            return JsonConvert.DeserializeObject<BulkResponse>(httpResponse.Body);
        }
    }
}
