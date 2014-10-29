using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-queries.html
    /// </summary>
    [JsonConverter(typeof(QuerySerializer))]
    public interface IQuery : ISearchPiece
    {
        string QueryName { get; }
    }
}
