using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Search
{
    public sealed class SearchTypeEnum : TypeSafeEnumBase<SearchTypeEnum>
    {
        /// <summary>
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-search-type.html#query-and-fetch
        /// </summary>
        public static readonly SearchTypeEnum QueryAndFetch = new SearchTypeEnum("query_and_fetch");

        /// <summary>
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-search-type.html#query-then-fetch
        /// </summary>
        public static readonly SearchTypeEnum QueryThenFetch = new SearchTypeEnum("query_then_fetch");

        /// <summary>
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-search-type.html#dfs-query-and-fetch
        /// </summary>
        public static readonly SearchTypeEnum DfsQueryAndFetch = new SearchTypeEnum("dfs_query_and_fetch");

        /// <summary>
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-search-type.html#dfs-query-then-fetch
        /// </summary>
        public static readonly SearchTypeEnum DfsQueryThenFetch = new SearchTypeEnum("dfs_query_then_fetch");

        private SearchTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
