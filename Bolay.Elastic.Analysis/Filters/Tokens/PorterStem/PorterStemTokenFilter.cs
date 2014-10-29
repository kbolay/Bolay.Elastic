using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.PorterStem
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-porterstem-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(PorterStemTokenFilterSerializer))]
    public class PorterStemTokenFilter : TokenFilterBase
    {
        public PorterStemTokenFilter(string name) : base(name, TokenFilterTypeEnum.PorterStem) { }
    }
}
