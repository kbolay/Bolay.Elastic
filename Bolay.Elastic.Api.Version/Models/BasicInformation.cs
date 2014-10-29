using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Basic.Models
{
    public class BasicInformation
    {
        /// <summary>
        /// Shows if the request was successful.
        /// </summary>
        [JsonProperty(PropertyName="ok")]
        public bool Success { get; set; }

        /// <summary>
        /// Status of the elastic server.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Name of the elastic server.
        /// </summary>
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        /// <summary>
        /// Version and build information.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        [DefaultValue(null)]
        public Version Version { get; set; }

        /// <summary>
        /// Tagline for ElasticSearch on this server.
        /// </summary>
        [JsonProperty(PropertyName = "tagline")]
        public string TagLine { get; set; }
    }
}
