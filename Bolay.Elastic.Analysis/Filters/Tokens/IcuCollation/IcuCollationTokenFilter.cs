using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.IcuCollation
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-icu-plugin.html#icu-collation
    /// </summary>
    [JsonConverter(typeof(IcuCollationTokenFilterSerializer))]
    public class IcuCollationTokenFilter : TokenFilterBase
    {
        // TODO: Fill in

        public IcuCollationTokenFilter(string name) : base(name, TokenFilterTypeEnum.IcuCollation) { }
    }
}
