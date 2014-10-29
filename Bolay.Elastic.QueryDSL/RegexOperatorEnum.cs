using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Regex
{
    public sealed class RegexOperatorEnum : TypeSafeEnumBase<RegexOperatorEnum>
    {
        //ALL, ANYSTRING, AUTOMATON, COMPLEMENT, EMPTY, INTERSECTION, INTERVAL, or NONE
        public static readonly RegexOperatorEnum All = new RegexOperatorEnum("all");
        public static readonly RegexOperatorEnum AnyString = new RegexOperatorEnum("ANYSTRING");
        public static readonly RegexOperatorEnum Automaton = new RegexOperatorEnum("AUTOMATON");
        public static readonly RegexOperatorEnum Complement = new RegexOperatorEnum("complement");
        public static readonly RegexOperatorEnum Empty = new RegexOperatorEnum("EMPTY");
        public static readonly RegexOperatorEnum Intersection = new RegexOperatorEnum("INTERSECTION");
        public static readonly RegexOperatorEnum Interval = new RegexOperatorEnum("INTERVAL");
        public static readonly RegexOperatorEnum None = new RegexOperatorEnum("NONE");

        private RegexOperatorEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
