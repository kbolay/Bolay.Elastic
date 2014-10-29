using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.HasParent
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-has-parent-query.html
    /// </summary>
    [JsonConverter(typeof(HasParentSerializer))]
    public class HasParentQuery : QueryBase
    {
        /// <summary>
        /// The type of parent document.
        /// </summary>
        public string ParentType { get; private set; }

        /// <summary>
        /// The method for scoring.
        /// Defaults to none.
        /// </summary>
        public ScoreTypeEnum ScoreType { get; set; }

        /// <summary>
        /// Any type of query.
        /// </summary>
        public IQuery Query { get; private set; }

        internal HasParentQuery()
        {
            ScoreType = HasParentSerializer._SCORE_TYPE_DEFAULT;
        }

        public HasParentQuery(string parentType, IQuery query) : this()
        {
            if (string.IsNullOrWhiteSpace(parentType))
                throw new ArgumentNullException("parentType", "HasParentQuery expects a child type.");
            if (query == null)
                throw new ArgumentNullException("query", "HasParentQuery expects a query.");            

            Query = query;
            ParentType = parentType;
        }
    }
}
