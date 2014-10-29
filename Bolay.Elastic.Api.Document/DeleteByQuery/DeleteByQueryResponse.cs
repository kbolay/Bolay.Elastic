using Bolay.Elastic.Api.Document.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.DeleteByQuery
{
    public class DeleteByQueryResponse
    {
        [JsonProperty("ok")]
        public bool Success { get; set; }

        [JsonProperty("_indices")]
        public IEnumerable<IndexShards> Indices { get; set; }
    }
}
