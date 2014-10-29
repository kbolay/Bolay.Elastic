using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-match-query.html#_match_phrase_prefix
    /// </summary>
    public class MatchPhrasePrefixQuery : MatchQueryBase
    {
        internal MatchPhrasePrefixQuery() : base(MatchQueryTypeEnum.PhrasePrefix) { }
        public MatchPhrasePrefixQuery(string field, string query) : base(MatchQueryTypeEnum.PhrasePrefix, field, query) { }
    }
}
