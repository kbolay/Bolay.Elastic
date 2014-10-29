using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Nested
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-nested-filter.html
    /// </summary>
    [JsonConverter(typeof(NestedSerializer))]
    public class NestedFilter : FilterBase
    {
        /// <summary>
        /// Gets the path to the nested object.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the filter to use when searching against the nested documents.
        /// </summary>
        public IFilter Filter { get; private set; }

        /// <summary>
        /// Gets or sets whether to perform the block join or not.
        /// Defaults to true.
        /// </summary>
        public bool Join { get; set; }

        /// <summary>
        /// Get or set the name of the cache. Only used if actually caching the filter.
        /// </summary>
        public string CacheName { get; set; }

        internal NestedFilter()
        {
            Join = NestedSerializer._JOIN_DEFAULT;
        }

        public NestedFilter(string path, IFilter filter)
            : this()
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "NestedFilter requires a path to the nested object.");
            if (filter == null)
                throw new ArgumentNullException("filter", "NestedFilter requires a filter.");

            Path = path;
            Filter = filter;
        }
    }
}
