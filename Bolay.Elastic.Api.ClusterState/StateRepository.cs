using Bolay.Elastic;
using Bolay.Elastic.Api.ClusterState;
using Bolay.Elastic.Api.ClusterState.Models;
using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState
{
    public class StateRepository : IStateRepository
    {
        private readonly IUriProvider _clusterUri;
        private readonly IHttpLayer _httpLayer;

        public StateRepository(IUriProvider clusterUri, IHttpLayer httpLayer)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "Cluster uri is required for the Cluster State Repository.");

            _clusterUri = clusterUri;
            _httpLayer = httpLayer;
        }
        public State Get(StateRequest request = null)
        {
            HttpRequest httpRequest = new HttpRequest(_clusterUri.Uri);
            httpRequest.ChangeUriPath("_cluster", "state");

            if(request != null)
            {
                httpRequest.AddToQueryString(StateRequest.LOCAL, request.Local.ToString(), defaultValue: false.ToString());
                httpRequest.AddToQueryString(StateRequest.FILTER_BLOCKS, request.FilterBlocks.ToString(), defaultValue: false.ToString());
                httpRequest.AddToQueryString(StateRequest.FILTER_INDICES, request.FilterIndices.ToString(), defaultValue: false.ToString());
                httpRequest.AddToQueryString(StateRequest.FILTER_METADATA, request.FilterMetaData.ToString(), defaultValue: false.ToString());
                httpRequest.AddToQueryString(StateRequest.FILTER_NODES, request.FilterNodes.ToString(), defaultValue: false.ToString());
                httpRequest.AddToQueryString(StateRequest.FILTER_ROUTING_TABLE, request.FilterRouting.ToString(), defaultValue: false.ToString());
            }

            HttpResponse response = _httpLayer.Get(httpRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ElasticRequestException(httpRequest, response);
            }

            return JsonConvert.DeserializeObject<State>(response.Body);
        }
    }
}
