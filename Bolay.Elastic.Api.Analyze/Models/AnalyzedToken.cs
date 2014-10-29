using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models
{
    public class AnalyzedToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("start_offset")]
        public Int64 StartOffset { get; set; }

        [JsonProperty("end_offset")]
        public Int64 EndOffset { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("position")]
        public Int64 Position { get; set; }
    }
}
