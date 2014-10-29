using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class Shards
    {
        /// <summary>
        /// The total number of shards hit with the request.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// The number of shards that responded successfully.
        /// </summary>
        [JsonProperty(PropertyName = "successful")]
        public int Successful { get; set; }

        /// <summary>
        /// The number of shards that failed.
        /// </summary>
        [JsonProperty(PropertyName = "failed")]
        public int failed { get; set; }
    }
}
