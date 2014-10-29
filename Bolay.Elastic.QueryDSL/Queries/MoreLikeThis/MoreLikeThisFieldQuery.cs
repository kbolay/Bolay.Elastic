using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MoreLikeThis
{
    public class MoreLikeThisFieldQuery : MoreLikeThisBase
    {
        internal MoreLikeThisFieldQuery() : base() { }

        public MoreLikeThisFieldQuery(string field, string query)
            : base(new List<string>() { field }, query)
        { }
    }
}
