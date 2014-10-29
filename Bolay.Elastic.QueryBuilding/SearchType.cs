using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryBuilding
{
    public sealed class SearchType : TypeSafeEnumBase<SearchType>
    {
        public static readonly SearchType TextEquals = new SearchType("TextEquals");
        public static readonly SearchType StartsWith = new SearchType("StartsWith");
        public static readonly SearchType TextContains = new SearchType("Contains");
        public static readonly SearchType EndsWith = new SearchType("EndsWith");
        public static readonly SearchType Wildcard = new SearchType("Wildcard");
        public static readonly SearchType TokenEquals = new SearchType("TokenEquals");
        public static readonly SearchType TokenStartsWith = new SearchType("TokenStartsWith");
        public static readonly SearchType TokenContains = new SearchType("TokenContains");
        public static readonly SearchType TokenEndsWith = new SearchType("TokenEndsWith");
        public static readonly SearchType TokenWildcard = new SearchType("TokenWildcard");
        public static readonly SearchType GreaterThan = new SearchType("GreaterThan");
        public static readonly SearchType GreaterThanOrEqualTo = new SearchType("GreaterThanOrEqualTo");
        public static readonly SearchType LessThan = new SearchType("LessThan");
        public static readonly SearchType LessThanOrEqualTo = new SearchType("LessThanOrEqualTo");
        public static readonly SearchType NotEqual = new SearchType("NotEqual");

        private SearchType(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
