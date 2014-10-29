using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public sealed class SearchType : TypeSafeEnumBase<SearchType>
    {
        public static readonly SearchType FullText = new SearchType("FullText");
        public static readonly SearchType FullTextStartsWith = new SearchType("FullTextStartsWith");
        public static readonly SearchType FullTextContains = new SearchType("FullTextContains");
        public static readonly SearchType FullTextEndsWith = new SearchType("FullTextEndsWith");
        public static readonly SearchType FullTextWildcard = new SearchType("FullTextWildcard");
        public static readonly SearchType TokenMatch = new SearchType("TokenMatch");
        public static readonly SearchType TokenStartsWith = new SearchType("TokenStartsWith");
        public static readonly SearchType TokenContains = new SearchType("TokenContains");
        public static readonly SearchType TokenEndsWith = new SearchType("TokenEndsWith");
        public static readonly SearchType TokenWildcard = new SearchType("TokenWildcard");

        public static readonly SearchType GreaterThan = new SearchType("GreaterThan");
        public static readonly SearchType GreaterThanOrEqual = new SearchType("GreaterThanOrEqual");
        public static readonly SearchType LessThan = new SearchType("LessThan");
        public static readonly SearchType LessThanOrEqual = new SearchType("LessThanOrEqual");
        public static readonly SearchType NotEqual = new SearchType("NotEqual");

        public static readonly SearchType Sort = new SearchType("Sort");
        public static readonly SearchType Facet = new SearchType("Facet");

        private SearchType(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
