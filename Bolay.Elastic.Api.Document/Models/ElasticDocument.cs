using Bolay.Elastic.Api.Document.Get;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class ElasticDocument<T>
    {
        /// <summary>
        /// The index of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_index")]
        public string Index { get; set; }

        /// <summary>
        /// The type of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_type")]
        public string Type { get; set; }


        /// <summary>
        /// The identifier of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        /// <summary>
        /// The version of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_version")]
        public int Version { get; set; }

        /// <summary>
        /// Shows whether the document exists or not.
        /// </summary>
        [JsonProperty(PropertyName = "found")]
        public bool Found { get; set; }

        /// <summary>
        /// Shows the score the document received based on the query.
        /// </summary>
        [JsonProperty(PropertyName = "_score")]
        public Double Score { get; set; }

        [JsonIgnore]
        private T _Document { get; set; }

        /// <summary>
        /// The actual document.
        /// </summary>
        [JsonProperty(PropertyName = "_source")]
        public T Document { get { return _Document; } set { _Document = value; } }

        [JsonConverter(typeof(FieldSerializer))]
        [JsonProperty(PropertyName = "fields")]
        protected T Fields { get { return _Document; } set { _Document = value; } }
    }
}
