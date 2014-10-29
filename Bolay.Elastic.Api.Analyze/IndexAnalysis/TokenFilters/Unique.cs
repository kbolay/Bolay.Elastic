using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Unique : TokenFilterBase
    {
        [JsonProperty("only_on_same_position")]
        [DefaultValue(default(bool))]
        public bool OnlyOnSamePosition { get; set; }

        public Unique() : base(TokenFilterEnum.Unique) { }
    }
}
