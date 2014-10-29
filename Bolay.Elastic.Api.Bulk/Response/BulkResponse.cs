using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Response
{
    public class BulkResponse
    {
        [JsonProperty("took")]
        public Int64 TotalMilliseconds { get; set; }

        [JsonProperty("errors")]
        public bool HadErrors { get; set; }

        [JsonProperty("items")]
        public IEnumerable<ActionResponse> Items { get; set; }
    }
}
