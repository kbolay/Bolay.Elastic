using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public abstract class AnalyzerBase
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("type")]
        [DefaultValue(default(string))]
        public string Type { get; private set; }

        [JsonProperty("alias")]
        [DefaultValue(default(List<string>))]
        public List<string> Aliases { get; set; }

        public AnalyzerBase(AnalyzerEnum type)
        {
            if(type == null)
                throw new ArgumentNullException("type");

            Type = type.ToString();
        }
    }
}
