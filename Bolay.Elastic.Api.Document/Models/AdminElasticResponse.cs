using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class AdminElasticResponse
    {
        /// <summary>
        /// Shows whether the administrative operation was successful.
        /// </summary>
        //[JsonProperty(PropertyName="ok")]
        //public bool Success { get; set; }

        /// <summary>
        /// The index of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_index")]
        public string Index { get; set; }

        /// <summary>
        /// The type of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_type")]
        public string DocumentType { get; set; }

        /// <summary>
        /// The identifier of the document.
        /// </summary>
        [JsonProperty(PropertyName = "_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// The version of the document.
        /// Value is populated on a Create or Update.
        /// </summary>
        [JsonProperty(PropertyName = "_version")]
        [DefaultValue(null)]
        public Int64? Version { get; set; }  // in create/update of document in index

        /// <summary>
        /// Shows whether the document was found.
        /// Value is populated on a Delete.
        /// </summary>
        [JsonProperty(PropertyName = "found")]
        [DefaultValue(null)]
        public bool? Found { get; set; } // in response to a DELETE
    }
}
