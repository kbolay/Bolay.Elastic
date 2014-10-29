using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-filters.html
    /// </summary>
    [JsonConverter(typeof(FilterSerializer))]
    public interface IFilter : ISearchPiece
    {
        bool Cache { get; set; }
        string CacheKey { get; set;  }
        string FilterName { get; set; }
    }
}
