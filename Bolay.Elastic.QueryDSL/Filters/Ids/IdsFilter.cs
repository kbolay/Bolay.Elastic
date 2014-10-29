using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Ids
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-ids-filter.html
    /// </summary>
    [JsonConverter(typeof(IdsSerializer))]
    public class IdsFilter : FilterBase
    {
        /// <summary>
        /// Gets or sets the types of documents to look for the specified ids in.
        /// </summary>
        public IEnumerable<string> Types { get; set; }

        /// <summary>
        /// Gets the _ids to search for.
        /// </summary>
        public IEnumerable<string> DocumentIds { get; private set; }

        /// <summary>
        /// Create an ids filter.
        /// </summary>
        /// <param name="documentIds">Set the document ids to search for.</param>
        public IdsFilter(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("documentIds", "IdsFilter requires at least one document id.");

            DocumentIds = documentIds.Where(x => !string.IsNullOrWhiteSpace(x));
            Cache = IdsSerializer._CACHE_DEFAULT;
        }
    }
}