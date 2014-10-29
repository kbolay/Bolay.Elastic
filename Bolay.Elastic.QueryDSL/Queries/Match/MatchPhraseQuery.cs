using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-match-query.html#_phrase
    /// </summary>
    public class MatchPhraseQuery : MatchQueryBase
    {
        internal MatchPhraseQuery() : base(MatchQueryTypeEnum.Phrase) { }
        public MatchPhraseQuery(string field, string query) : base(MatchQueryTypeEnum.Phrase, field, query) { }
    }
}
