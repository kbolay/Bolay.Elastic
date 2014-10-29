using Bolay.Elastic.Api.Analyze.Exceptions;
using Bolay.Elastic.Api.Analyze.Models;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze
{
    public class AnalyzeRepository : IAnalyzeRepository
    {
        private readonly Uri _ClusterUri;
        private readonly IHttpLayer _httpLayer;

        public AnalyzeRepository(IUriProvider clusterUri, IHttpLayer httpLayer)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "The analyze repository requires a uri provider for the elastic cluster.");

            _ClusterUri = clusterUri.Uri;
            _httpLayer = httpLayer;
        }

        public IEnumerable<AnalyzedToken> AnalyzeText(AnalyzeRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string endPath = "_analyze" + request.ToString();

            Uri analyzeUri = null;
            if(!string.IsNullOrWhiteSpace(request.Index))
                analyzeUri = new Uri(_ClusterUri, request.Index + "/" + endPath);
            else
                analyzeUri = new Uri(_ClusterUri, endPath);

            HttpResponse response = _httpLayer.Get(new HttpRequest(analyzeUri));
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new AnalyzeRequestException(analyzeUri);

            AnalyzeResponse result = JsonConvert.DeserializeObject<AnalyzeResponse>(response.Body);
            return result.Tokens;
        }
    }
}
