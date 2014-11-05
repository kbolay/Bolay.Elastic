using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Mapping;
using System.Collections.Generic;

namespace Bolay.Elastic.Api.Mapping
{
    public interface IMappingRepository
    {
        /// <summary>
        /// Get the mapping of the indexes of the cluster.
        /// </summary>
        /// <returns>A collection of index mappings.</returns>
        IEnumerable<IndexMapping> GetClusterMapping();

        /// <summary>
        /// Get the mapping of the indexes of the alias.
        /// </summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>A collection of index mappings.</returns>
        IEnumerable<IndexMapping> GetAliasMapping(string alias);

        /// <summary>
        /// Get the mapping of an index.
        /// </summary>
        /// <param name="indexName">The name of the index.</param>
        /// <returns>An index mapping.</returns>
        IndexMapping GetIndexMapping(string indexName);

        /// <summary>
        /// Get the index type mapping.
        /// </summary>
        /// <param name="indexName">The name of the index or alias.</param>
        /// <param name="type">The name of the type.</param>
        /// <returns>The mapping of the type.</returns>
        TypeMapping GetIndexTypeMapping(string indexName, string type);
    }
}
