using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class SearchElasticResponse<T>
    {
        /// <summary>
        /// The number of milliseconds ElasticSearch spent gathering the documents.
        /// </summary>
        [JsonProperty(PropertyName = "took")]
        public Int64 TotalMilliseconds { get; set; }

        /// <summary>
        /// Shows whether the request timed out.
        /// </summary>
        [JsonProperty(PropertyName = "timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// Metadata about the request on the shards.
        /// </summary>
        [JsonProperty(PropertyName = "_shards")]
        public Shards Shards { get; set; }

        /// <summary>
        /// An envelope for request metadata and the results.
        /// </summary>
        [JsonProperty(PropertyName = "hits")]
        public Hits<T> Hits { get; set; }
    }
}
