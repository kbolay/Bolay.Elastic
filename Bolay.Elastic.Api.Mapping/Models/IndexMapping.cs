using Bolay.Elastic.Mapping.Types.RootObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Models
{
    [JsonConverter(typeof(IndexMappingSerializer))]
    public class IndexMapping
    {
        /// <summary>
        /// Gets the name of the index.
        /// </summary>
        public string IndexName { get; private set; }

        /// <summary>
        /// Gets the mapping for the types of documents in the index.
        /// </summary>
        public IEnumerable<RootObjectProperty> Types { get; private set; }

        /// <summary>
        /// Creates the mapping for an index.
        /// </summary>
        /// <param name="indexName">Sets the name of the index this mapping is for.</param>
        /// <param name="types">Sets the mapping for the types of the index.</param>
        public IndexMapping(string indexName, IEnumerable<RootObjectProperty> types)
        {
            if (string.IsNullOrWhiteSpace(indexName))
                throw new ArgumentNullException("indexName", "IndexMapping requires an index name.");

            if (types == null || types.All(x => x == null))
                throw new ArgumentNullException("types", "IndexMapping requires at least one type.");

            IndexName = indexName;
            Types = types.Where(x => x != null);
        }
    }
}
