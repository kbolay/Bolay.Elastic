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

        private readonly Uri _bulkUri;
        private readonly IHttpLayer _httpLayer;

        public BulkRepository(IUriProvider serverUri, IHttpLayer httpLayer)
        {
            if (serverUri == null || serverUri.Uri == null)
            {
                throw new ArgumentNullException("serverUri", "Any bulk request requires the uri of the ES cluster.");
            }

            if (httpLayer == null)
            {
                throw new ArgumentNullException("httpLayer");
            }

            _bulkUri = new Uri(serverUri.Uri, _BULK_API_PATH);
            _httpLayer = httpLayer;
        }

        public BulkResponse DoBulkRequest(BulkRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            HttpRequest httpRequest = new HttpRequest(_bulkUri, request.ToString());
            HttpResponse httpResponse = _httpLayer.Post(httpRequest);
            
            return JsonConvert.DeserializeObject<BulkResponse>(httpResponse.Body);
        }
    }
}
