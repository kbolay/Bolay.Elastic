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
    public class StateRepository //: IStateRepository
    {
        private readonly Uri _ClusterUri;

        public StateRepository(IUriProvider clusterUri)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "Cluster uri is required for the Cluster State Repository.");

            _ClusterUri = new Uri(clusterUri.Uri.GetLeftPart(UriPartial.Authority));
        }

        //public State Get(StateRequest request = null)
        //{
        //    Uri clusterUri = null;
        //    if (request == null)
        //        clusterUri = new Uri(_ClusterUri, "_cluster/state");
        //    else
        //        clusterUri = new Uri(_ClusterUri, "_cluster/state" + request.ToString());

        //    HttpResponse response = HttpRequestUtility.Get(clusterUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(clusterUri);

        //    return JsonConvert.DeserializeObject<State>(response.Body);
        //}
    }
}
