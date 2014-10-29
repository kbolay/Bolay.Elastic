using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Elision : TokenFilterBase
    {
        [JsonProperty("articles")]
        [DefaultValue(default(List<string>))]
        public List<string> Articles { get; set; }

        public Elision() : base(TokenFilterEnum.Elision) { }
    }
}
