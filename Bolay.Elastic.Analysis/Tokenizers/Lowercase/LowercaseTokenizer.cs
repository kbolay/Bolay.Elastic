using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Lowercase
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-lowercase-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(LowercaseTokenizerSerializer))]
    public class LowercaseTokenizer : TokenizerBase
    {
        public LowercaseTokenizer(string name) : base(name, TokenizerTypeEnum.Lowercase) { }
    }
}
