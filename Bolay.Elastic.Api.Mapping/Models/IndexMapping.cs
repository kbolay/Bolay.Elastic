using Bolay.Elastic.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<TypeMapping> Types { get; private set; }

        /// <summary>
        /// Creates the mapping for an index.
        /// </summary>
        /// <param name="indexName">Sets the name of the index this mapping is for.</param>
        /// <param name="types">Sets the mapping for the types of the index.</param>
        public IndexMapping(string indexName, IEnumerable<TypeMapping> types)
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
