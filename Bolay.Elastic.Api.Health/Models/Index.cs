using Bolay.Elastic.Api.Basic.Models;
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
    /// Index health information.
    /// </summary>
    public class Index
    {
        /// <summary>
        /// The name of the index.
        /// </summary>
        [JsonIgnore]
        public string Name { get; set; }

        /// <summary>
        /// The status of the index.
        /// </summary>
        [JsonProperty(PropertyName="status")]
        [JsonConverter(typeof(TypeSafeEnumSerializer))]
        public StatusSetting Status { get; set; }

        /// <summary>
        /// The number of shards in the index.
        /// </summary>
        [JsonProperty(PropertyName="number_of_shards")]
        public int NumberOfShards { get; set; }

        /// <summary>
        /// The number of replica shards in the index.
        /// </summary>
        [JsonProperty(PropertyName = "number_of_replicas")]
        public int NumberOfReplicas { get; set; }

        /// <summary>
        /// The number of active primary shards in the index.
        /// </summary>
        [JsonProperty(PropertyName = "active_primary_shards")]
        public int ActivePrimaryShards { get; set; }

        /// <summary>
        /// The number of active shards in the index.
        /// </summary>
        [JsonProperty(PropertyName = "active_shards")]
        public int ActiveShards { get; set; }

        /// <summary>
        /// The number of shards currently relocating in the index.
        /// </summary>
        [JsonProperty(PropertyName = "relocating_shards")]
        public int RelocatingShards { get; set; }

        /// <summary>
        /// The number of shards currently initializing in the index.
        /// </summary>
        [JsonProperty(PropertyName = "initializing_shards")]
        public int InitializingShards { get; set; }

        /// <summary>
        /// The number of shards currently unassigned in the index.
        /// </summary>
        [JsonProperty(PropertyName="unassigned_shards")]
        public int UnassignedShards { get; set; }

        /// <summary>
        /// The shards of the index.
        /// </summary>
        [JsonProperty(PropertyName="shards")]
        [JsonConverter(typeof(ShardListConverter))]
        [DefaultValue(null)]
        public List<Shard> Shards { get; set; }
    }
}
