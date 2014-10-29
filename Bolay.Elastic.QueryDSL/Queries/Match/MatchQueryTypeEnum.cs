using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    public sealed class MatchQueryTypeEnum : TypeSafeEnumBase<MatchQueryTypeEnum>
    {
        public static readonly MatchQueryTypeEnum Boolean = new MatchQueryTypeEnum("match");
        public static readonly MatchQueryTypeEnum Phrase = new MatchQueryTypeEnum("match_phrase");
        public static readonly MatchQueryTypeEnum PhrasePrefix = new MatchQueryTypeEnum("match_phrase_prefix");
        public static readonly MatchQueryTypeEnum Multi = new MatchQueryTypeEnum("multi_match");

        private MatchQueryTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
