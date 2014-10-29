using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MoreLikeThis
{
    public class MoreLikeThisQuery : MoreLikeThisBase
    {
        internal MoreLikeThisQuery() : base() { }

        /// <summary>
        /// Create a more like this query without fields.
        /// </summary>
        /// <param name="query"></param>
        public MoreLikeThisQuery(string query) : base(query) { }

        /// <summary>
        /// Create a more like this query with fields.
        /// </summary>
        /// <param name="fields">The fields to search against.</param>
        /// <param name="query">The value for the like_this property.</param>
        public MoreLikeThisQuery(IEnumerable<string> fields, string query) : base(fields, query) { }
    }
}
