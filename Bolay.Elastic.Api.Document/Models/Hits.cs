using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class Hits<T>
    {
        /// <summary>
        /// The total number of results matching the query.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public Int64 Total { get; set; }

        /// <summary>
        /// The maximum score of any of the returned documents.
        /// </summary>
        [JsonProperty(PropertyName = "max_score")]
        public Double MaxScore { get; set; }

        /// <summary>
        /// The collection of documents.
        /// </summary>
        [JsonProperty(PropertyName = "hits")]
        public List<ElasticDocument<T>> Documents { get; set; }
    }
}
