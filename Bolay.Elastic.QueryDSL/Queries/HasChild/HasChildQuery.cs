using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.HasChild
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-has-child-filter.html
    /// </summary>
    [JsonConverter(typeof(HasChildSerializer))]
    public class HasChildQuery : QueryBase
    {
        /// <summary>
        /// The type of child document.
        /// </summary>
        public string ChildType { get; private set; }

        /// <summary>
        /// The method for scoring.
        /// Defaults to none.
        /// </summary>
        public ScoreTypeEnum ScoreType { get; set; }

        /// <summary>
        /// Any type of query.
        /// </summary>
        public IQuery Query { get; private set; }

        internal HasChildQuery()
        {
            ScoreType = HasChildSerializer._SCORE_TYPE_DEFAULT;
        }

        /// <summary>
        /// Create a has_child query.
        /// </summary>
        /// <param name="childType">The type of the child document.</param>
        /// <param name="query">The query to use for searching.</param>
        public HasChildQuery(string childType, IQuery query) : this()
        {
            if (string.IsNullOrWhiteSpace(childType))
                throw new ArgumentNullException("childType", "HasChildQuery expects a child type.");
            if (query == null)
                throw new ArgumentNullException("query", "HasChildQuery expects a query.");            

            Query = query;
            ChildType = childType;
        }
    }
}
