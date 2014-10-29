using Bolay.Elastic.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    /// <summary>
    /// Shard health information
    /// </summary>
    public class Shard
    {
        /// <summary>
        /// The assigned number of the shard.
        /// </summary>
        [JsonIgnore]
        public string Id { get; set; }

        /// <summary>
        /// The status of the index.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(TypeSafeEnumSerializer))]
        public StatusSetting Status { get; set; }

        /// <summary>
        /// Is the primary copy of this shard active.
        /// </summary>
        [JsonProperty(PropertyName="primary_active")]
        public bool PrimaryActive { get; set; }

        /// <summary>
        /// Total number of active copies of this shard.
        /// </summary>
        [JsonProperty(PropertyName="active_shards")]
        public int ActiveShards { get; set; }

        /// <summary>
        /// Number of copies of this shard currently relocating.
        /// </summary>
        [JsonProperty(PropertyName = "relocating_shards")]
        public int RelocatingShards { get; set; }

        /// <summary>
        /// Number of copies of this shard currently initializing.
        /// </summary>
        [JsonProperty(PropertyName = "initializing_shards")]
        public int InitializingShards { get; set; }

        /// <summary>
        /// Number of copies of this shard currently unassigned.
        /// </summary>
        [JsonProperty(PropertyName="unassigned_shards")]
        public int UnassignedShards { get; set; }
    }
}
