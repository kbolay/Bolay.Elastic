using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.Icu
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-icu-plugin.html#_icu_tokenizer
    /// </summary>
    [JsonConverter(typeof(IcuTokenizerSerializer))]
    public class IcuTokenizer : TokenizerBase
    {
        public IcuTokenizer(string name) : base(name, TokenizerTypeEnum.IcuTokenizer) { }
    }
}
