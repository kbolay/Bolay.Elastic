using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Ids
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-ids-query.html
    /// </summary>
    [JsonConverter(typeof(IdsSerializer))]
    public class IdsQuery : QueryBase
    {
        /// <summary>
        /// The type or types to search for ids in. This value is not required.
        /// </summary>
        public IEnumerable<string> DocumentTypes { get; set; }

        /// <summary>
        /// The ids of the documents you want to return.
        /// </summary>
        public IEnumerable<string> DocumentIds { get; private set; }

        public IdsQuery(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("documentIds", "IdsQuery requires at least one document id.");

            DocumentIds = documentIds;
        }
    }
}
