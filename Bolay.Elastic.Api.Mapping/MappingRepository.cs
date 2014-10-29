using Bolay.Elastic.Api.Mapping.Exceptions;
using Bolay.Elastic.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Interfaces;
using Bolay.Elastic.Mapping.Types.RootObject;
using Bolay.Elastic.Api.Settings;
using Bolay.Elastic.Analysis;
using Bolay.Elastic.Mapping;

namespace Bolay.Elastic.Api.Mapping
{
    public class MappingRepository : IMappingRepository
    {
        private const string _MAPPING = "_mapping";

        private readonly Uri _ClusterUri;
        private readonly ISettingsRepository _SettingsRepository;
        private readonly IHttpLayer _httpLayer;

        public MappingRepository(IUriProvider clusterUri, ISettingsRepository settingsRepository, IHttpLayer httpLayer)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "The mapping repository requires a uri provider for the elastic cluster.");

            _ClusterUri = clusterUri.Uri;
            _httpLayer = httpLayer;
            _SettingsRepository = settingsRepository;
        }

        public IEnumerable<IndexMapping> GetClusterMapping()
        {
            Uri mappingUri = new Uri(_ClusterUri, _MAPPING);
            HttpResponse response = _httpLayer.Get(new HttpRequest(mappingUri));
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new MappingRequestException(mappingUri);

            IndexMappingCollection.PopulateIndicesAnalysis(_SettingsRepository.Get());
            return JsonConvert.DeserializeObject<IndexMappingCollection>(response.Body);
        }

        public IEnumerable<IndexMapping> GetAliasMapping(string alias)
        {
            Uri mappingUri = new Uri(_ClusterUri, alias + "/" + _MAPPING);
            HttpResponse response = _httpLayer.Get(new HttpRequest(mappingUri));
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new MappingRequestException(mappingUri);

            IndexMappingCollection.PopulateIndicesAnalysis(_SettingsRepository.GetByAlias(alias));
            return JsonConvert.DeserializeObject<IndexMappingCollection>(response.Body);
        }

        public IndexMapping GetIndexMapping(string indexName)
        {
            Uri mappingUri = new Uri(_ClusterUri, indexName + "/" + _MAPPING);
            HttpResponse response = _httpLayer.Get(new HttpRequest(mappingUri));
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new MappingRequestException(mappingUri);

            IndexMappingCollection.PopulateIndicesAnalysis(_SettingsRepository.GetByIndex(indexName));
            return JsonConvert.DeserializeObject<IndexMapping>(response.Body);
        }

        public RootObjectProperty GetIndexTypeMapping(string indexName, string type)
        {
            Uri mappingUri = new Uri(_ClusterUri, indexName + "/" + type + "/" + _MAPPING);
            HttpResponse response = _httpLayer.Get(new HttpRequest(mappingUri));
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new MappingRequestException(mappingUri);

            IndexMappingCollection.PopulateIndicesAnalysis(_SettingsRepository.GetByIndex(indexName));
            PropertyAnalyzer.PopulateIndexAnalyzers(IndexMappingCollection.GetIndexAnalysis(indexName));
            return JsonConvert.DeserializeObject<RootObjectProperty>(response.Body);
        }
    }
}
