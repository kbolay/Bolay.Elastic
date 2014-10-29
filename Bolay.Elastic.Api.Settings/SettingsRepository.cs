using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Api.Settings.Models;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings
{
    public class SettingsRepository : ISettingsRepository
    {
        private const string _SETTINGS = "_settings";

        private readonly Uri _ClusterUri;
        private readonly IHttpLayer _httpLayer;

        public SettingsRepository(IUriProvider clusterUri, IHttpLayer httpLayer)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "Cluster uri is required for the Settings Repository.");

            _ClusterUri = new Uri(clusterUri.Uri.GetLeftPart(UriPartial.Authority));
            _httpLayer = httpLayer;
        }

        /// <summary>
        /// Gets the index settings for the indices of the cluster.
        /// </summary>
        /// <returns>Collection of index settings.</returns>
        public IEnumerable<Models.Settings> Get()
        {
            Uri clusterUri = new Uri(_ClusterUri, _SETTINGS);
            HttpRequest request = new HttpRequest(clusterUri.ToString());
            HttpResponse response = _httpLayer.Get(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ElasticRequestException(request, response);

            return JsonConvert.DeserializeObject<SettingsCollection>(response.Body);
        }

        public IEnumerable<Models.Settings> GetByAlias(string alias)
        { 
            if(string.IsNullOrWhiteSpace(alias))
                throw new ArgumentNullException("alias", "SettingsRepository.GetAnalysis requires an alias.");

            Uri clusterUri = new Uri(_ClusterUri, alias + "/" + _SETTINGS);
            HttpRequest request = new HttpRequest(clusterUri.ToString());
            HttpResponse response = _httpLayer.Get(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ElasticRequestException(request, response);

            return JsonConvert.DeserializeObject<SettingsCollection>(response.Body);
        }

        /// <summary>
        /// Gets the settings for an index.
        /// </summary>
        /// <param name="indexName">The index to retrieve the settings for.</param>
        /// <returns>The settings of an index.</returns>
        public Models.Settings GetByIndex(string indexName)
        {
            if (string.IsNullOrWhiteSpace(indexName))
                throw new ArgumentNullException("indexName", "SettingsRepository.GetAnalysis requires an index name.");

            Uri clusterUri = new Uri(_ClusterUri, indexName + "/" + _SETTINGS);
            HttpRequest request = new HttpRequest(clusterUri.ToString());
            HttpResponse response = _httpLayer.Get(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ElasticRequestException(request, response);



            return JsonConvert.DeserializeObject<Models.Settings>(response.Body);
        }
    }
}
