using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Basic.Models
{
    public class Version
    {
        /// <summary>
        /// The version of ElasticSearch.
        /// </summary>
        [JsonProperty(PropertyName="number")]
        public string Number { get; set; }

        /// <summary>
        /// Is the ElasticSearch build a snap shot build.
        /// </summary>
        [JsonProperty(PropertyName = "snapshot_build")]
        public bool IsSnapShotBuild { get; set; }
    }
}
