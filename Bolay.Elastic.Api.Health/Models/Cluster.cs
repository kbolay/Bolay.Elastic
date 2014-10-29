using Bolay.Elastic.Api.Health.Serialization;
using Bolay.Elastic.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    /// <summary>
    /// Cluster health information.
    /// </summary>
    public class Cluster
    {
        /// <summary>
        /// The name of the cluster.
        /// </summary>
        [JsonProperty(PropertyName="cluster_name")]
        public string ClusterName { get; set; }

        /// <summary>
        /// Red status indicates that the specific shard is not allocated in the cluster.
        /// Yellow means that the primary shard is allocated but replicas are not.
        /// Green means that all shards are allocated.
        /// An index's status depends on the worst shard.
        /// A cluster's status depends on the worst index.
        /// </summary>
        [JsonProperty(PropertyName="status")]
        [JsonConverter(typeof(TypeSafeEnumSerializer))]
        public StatusSetting Status { get; set; }

        /// <summary>
        /// Did the health request time out?
        /// </summary>
        [JsonProperty(PropertyName="timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// Number of nodes checked by the health request.
        /// </summary>
        [JsonProperty(PropertyName="number_of_nodes")]
        public int NumberOfNodes { get; set; }

        /// <summary>
        /// Number of data nodes checked by the health request.
        /// </summary>
        [JsonProperty(PropertyName="number_of_data_nodes")]
        public int NumberOfDataNodes { get; set; }

        /// <summary>
        /// Number of active primary shards.
        /// </summary>
        [JsonProperty(PropertyName="active_primary_shards")]
        public int ActivePrimaryShards { get; set; }

        /// <summary>
        /// Number of active shards.
        /// </summary>
        [JsonProperty(PropertyName="active_shards")]
        public int ActiveShards { get; set; }

        /// <summary>
        /// Number of shards in the process of relocating.
        /// </summary>
        [JsonProperty(PropertyName="relocating_shards")]
        public int RelocatingShards { get; set; }

        /// <summary>
        /// Number of shards currently initializing.
        /// </summary>
        [JsonProperty(PropertyName="initializing_shards")]
        public int InitializingShards { get; set; }

        /// <summary>
        /// Number of unassigned shards.
        /// </summary>
        [JsonProperty(PropertyName="unassigned_shards")]
        public int UnassignedShards { get; set; }

        /// <summary>
        /// The indices of the cluster.
        /// </summary>
        [JsonProperty(PropertyName = "indices")]
        [JsonConverter(typeof(IndexListConverter))]
        [DefaultValue(null)]
        public List<Index> Indices { get; set; }
    }
}
