using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.HasChild
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-has-child-filter.html
    /// </summary>
    [JsonConverter(typeof(HasChildSerializer))]
    public class HasChildFilter : IFilter
    {
        /// <summary>
        /// Gets the type of the child document to search in.
        /// </summary>
        public string ChildType { get; private set; }

        /// <summary>
        /// Gets the filter of the child documents.
        /// </summary>
        public IFilter Filter { get; private set; }

        /// <summary>
        /// Gets the query of the child documents.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Caching is not an option for the has_child filter.
        /// </summary>
        public bool Cache
        {
            get
            {
                return false;
            }
            set
            {
                throw new ArgumentException("Cache is not an option for the has_child filter.");
            }
        }

        /// <summary>
        /// CacheKey is not an option for the has_child filter.
        /// </summary>
        public string CacheKey
        {
            get
            {
                return null;
            }
            set
            {
                throw new ArgumentException("CacheKey is not an option for the has_child filter.");
            }
        }

        /// <summary>
        /// Gets or sets the filter name.
        /// </summary>
        public string FilterName { get; set; }

        private HasChildFilter(string childType)
        {
            if (string.IsNullOrWhiteSpace(childType))
                throw new ArgumentNullException("childType", "HasChildFilter requires a child document type.");

            ChildType = childType;
        }

        /// <summary>
        /// Create a has_child filter that searches using an filter.
        /// </summary>
        /// <param name="childType">The type of the child document.</param>
        /// <param name="filter">The filter to use for searching.</param>
        public HasChildFilter(string childType, IFilter filter)
            : this(childType)
        {
            if (filter == null)
                throw new ArgumentNullException("filter", "HasChildFilter requires a filter in this constructor.");

            Filter = filter;
        }

        /// <summary>
        /// Create a has_child filter that searches using a query.
        /// </summary>
        /// <param name="childType">The type of the child document.</param>
        /// <param name="query">The query to use for searching.</param>
        public HasChildFilter(string childType, IQuery query)
            : this(childType)
        {
            if (query == null)
                throw new ArgumentNullException("query", "HasChildFilter requires a query in this constructor.");

            Query = query;
        }
    }
}
