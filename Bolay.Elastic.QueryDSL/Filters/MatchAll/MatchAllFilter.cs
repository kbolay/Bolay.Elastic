using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.MatchAll
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-match-all-filter.html
    /// </summary>
    [JsonConverter(typeof(MatchAllSerializer))]
    public class MatchAllFilter : FilterBase
    {
        public MatchAllFilter()
        {
            Cache = MatchAllSerializer._CACHE_DEFAULT;
        }
    }
}
