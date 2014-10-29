using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public abstract class TokenizerBase
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty("type")]
        [DefaultValue(default(string))]
        public string Type { get; private set; }

        public TokenizerBase(TokenizerEnum type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Type = type.ToString();
        }
    }
}
