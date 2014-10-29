using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Api.Health.Exceptions;
using Bolay.Elastic.Api.Health.Models;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health
{
    public class HealthRepository //: IHealthRepository
    {
        private readonly Uri _ClusterUri;

        public HealthRepository(IUriProvider clusterUri)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "Cluster uri is required for the Health Repository.");

            _ClusterUri = new Uri(clusterUri.Uri.GetLeftPart(UriPartial.Authority));
        }

        /// <summary>
        /// Retrieve the health of the cluster.
        /// </summary>
        /// <param name="healthRequest">The options available for health requests.</param>
        /// <returns></returns>
        //public Models.Cluster Get(Models.HealthRequest healthRequest = null)
        //{
        //    Uri clusterUri = null;
        //    if (healthRequest == null || healthRequest.IsEmpty)
        //        clusterUri = new Uri(_ClusterUri, "_cluster/health");
        //    else
        //        clusterUri = new Uri(_ClusterUri, "_cluster/health" + healthRequest.ToString());

        //    HttpResponse response = HttpRequestUtility.Get(clusterUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(clusterUri);

        //    return JsonConvert.DeserializeObject<Cluster>(response.Body);
        //}

        /// <summary>
        /// Retrieves the health of the indexes this alias is associated with.
        /// </summary>
        /// <param name="alias">The name of the alias.</param>
        /// <param name="healthRequest">The options available for health requests.</param>
        /// <returns></returns>
        //public IEnumerable<Models.Index> GetAlias(string alias, Models.HealthRequest healthRequest = null)
        //{
        //    if (healthRequest == null)
        //        healthRequest = new HealthRequest() { Level = LevelSetting.Indices };

        //    Uri requestUri = new Uri(_ClusterUri, "_cluster/health/" + alias + healthRequest.ToString());
        //    if (healthRequest.Level == LevelSetting.Cluster)
        //        throw new InvalidLevelException(requestUri, "Cluster is not a valid level for a health request on an alias.");

        //    HttpResponse response = HttpRequestUtility.Get(requestUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(requestUri);

        //    Cluster cluster = JsonConvert.DeserializeObject<Cluster>(response.Body);
        //    if (cluster == null || cluster.Indices == null || !cluster.Indices.Any())
        //        return null;

        //    return cluster.Indices;
        //}

        /// <summary>
        /// Retrieves the health of the indexes this alias is associated with.
        /// </summary>
        /// <param name="indexNames">The collection of index names for which health reports are desired.</param>
        /// <param name="healthRequest">The options available for health requests.</param>
        /// <returns></returns>
        //public IEnumerable<Models.Index> GetIndices(IEnumerable<string> indexNames, Models.HealthRequest healthRequest = null)
        //{
        //    if (healthRequest == null)
        //        healthRequest = new HealthRequest() { Level = LevelSetting.Indices };

        //    Uri requestUri = new Uri(_ClusterUri, "_cluster/health/" + string.Join(",", indexNames) + healthRequest.ToString());
        //    if (healthRequest.Level == LevelSetting.Cluster)
        //        throw new InvalidLevelException(requestUri, "Cluster is not a valid level for a health request on an alias.");

        //    HttpResponse response = HttpRequestUtility.Get(requestUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(requestUri);

        //    Cluster cluster = JsonConvert.DeserializeObject<Cluster>(response.Body);
        //    if (cluster == null || cluster.Indices == null || !cluster.Indices.Any())
        //        return null;

        //    return cluster.Indices;
        //}

        /// <summary>
        /// Retrieve the health of an index.
        /// </summary>
        /// <param name="indexName">The index name to retrieve health for.</param>
        /// <param name="healthRequest">The options available for health requests.</param>
        /// <returns></returns>
        //public Models.Index GetIndex(string indexName, Models.HealthRequest healthRequest = null)
        //{
        //    if (healthRequest == null)
        //        healthRequest = new HealthRequest() { Level = LevelSetting.Indices };

        //    Uri requestUri = new Uri(_ClusterUri, "_cluster/health/" + indexName + healthRequest.ToString());
        //    if (healthRequest.Level == LevelSetting.Cluster)
        //        throw new InvalidLevelException(requestUri, "Cluster is not a valid level for a health request on an index.");

        //    HttpResponse response = HttpRequestUtility.Get(requestUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(requestUri);

        //    Cluster cluster = JsonConvert.DeserializeObject<Cluster>(response.Body);
        //    if (cluster == null || cluster.Indices == null || !cluster.Indices.Any())
        //        return null;

        //    return cluster.Indices.FirstOrDefault(x => x.Name.Equals(indexName, StringComparison.OrdinalIgnoreCase));
        //}

        /// <summary>
        /// Retrieve the health of a shard.
        /// </summary>
        /// <param name="indexName">The name of the index to search in.</param>
        /// <param name="shard">The shard to retrieve health for.</param>
        /// <param name="healthRequest">The options available for health requests.</param>
        /// <returns></returns>
        //public Models.Shard GetShard(string indexName, string shard, Models.HealthRequest healthRequest = null)
        //{
        //    if (healthRequest == null)
        //        healthRequest = new HealthRequest() { Level = LevelSetting.Indices };

        //    Uri requestUri = new Uri(_ClusterUri, "_cluster/health/" + indexName + healthRequest.ToString());
        //    if (healthRequest.Level != LevelSetting.Shards)
        //        throw new InvalidLevelException(requestUri, "Shards is the only valid level for a health request on a shard.");

        //    HttpResponse response = HttpRequestUtility.Get(requestUri);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //        throw new ElasticRequestException(requestUri);

        //    Cluster cluster = JsonConvert.DeserializeObject<Cluster>(response.Body);
        //    if (cluster == null || cluster.Indices == null || !cluster.Indices.Any())
        //        return null;

        //    Index index = cluster.Indices.FirstOrDefault(x => x.Name.Equals(indexName, StringComparison.OrdinalIgnoreCase));
        //    if (index == null || index.Shards == null || !index.Shards.Any())
        //        return null;

        //    return index.Shards.FirstOrDefault(x => x.Id.Equals(shard, StringComparison.OrdinalIgnoreCase));
        //}
    }
}
