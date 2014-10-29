using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    public class QueryBase : IQuery
    {
        /// <summary>
        /// Gets or sets the _name of the query.
        /// </summary>
        public string QueryName { get; set; }
    }
}
