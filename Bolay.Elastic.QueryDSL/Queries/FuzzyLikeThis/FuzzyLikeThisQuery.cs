using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-flt-query.html
    /// </summary>    
    public class FuzzyLikeThisQuery : FuzzyLikeThisBase
    {
        internal FuzzyLikeThisQuery() : base() { }

        /// <summary>
        /// Create a fuzzy like this query.
        /// </summary>
        /// <param name="fields">The fields to search against.</param>
        /// <param name="query">The value for the like_this property.</param>
        public FuzzyLikeThisQuery(IEnumerable<string> fields, string query) : base(fields, query) { }
    }
}
