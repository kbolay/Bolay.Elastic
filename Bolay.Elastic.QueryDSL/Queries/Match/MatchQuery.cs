using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-match-query.html#_boolean
    /// </summary>
    public class MatchQuery : MatchQueryBase
    {
        internal MatchQuery() : base(MatchQueryTypeEnum.Boolean) { }
        public MatchQuery(string field, string query) : base(MatchQueryTypeEnum.Boolean, field, query) { }
    }
}
