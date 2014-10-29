using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models
{
    public class AnalyzeResponse
    {
        [JsonProperty("tokens")]
        public List<AnalyzedToken> Tokens { get; set; }
    }
}
