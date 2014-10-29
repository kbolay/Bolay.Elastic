using Bolay.Elastic.QueryDSL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.HasParent
{
    [JsonConverter(typeof(HasParentSerializer))]
    public class HasParentFilter : IFilter
    {
        /// <summary>
        /// Cache is not an option for the has_parent filter.
        /// </summary>
        public bool Cache
        {
            get
            {
                return false;
            }
            set
            {
                throw new ArgumentException("Cache is not an option for the has_parent filter.");
            }
        }

        /// <summary>
        /// CacheKey is not an option for the has_parent filter.
        /// </summary>
        public string CacheKey
        {
            get
            {
                return null;
            }
            set
            {
                throw new ArgumentException("CacheKey is not an option from the has_parent filter.");
            }
        }

        /// <summary>
        /// Gets the parent type of the document.
        /// </summary>
        public string ParentType { get; private set; }

        /// <summary>
        /// Gets the filter to search against the parent type.
        /// </summary>
        public IFilter Filter { get; private set; }

        /// <summary>
        /// Gets the query to search against the parent type.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Gets or sets the filter name.
        /// </summary>
        public string FilterName { get; set; }

        private HasParentFilter(string parentType)
        {
            if (string.IsNullOrWhiteSpace(parentType))
                throw new ArgumentNullException("parentType", "HasParentFilter requires a parent document type.");

            ParentType = parentType;
        }

        /// <summary>
        /// Create a has_parent filter that searches using an filter.
        /// </summary>
        /// <param name="childType">The type of the parent document.</param>
        /// <param name="filter">The filter to use for searching.</param>
        public HasParentFilter(string parentType, IFilter filter)
            : this(parentType)
        {
            if (filter == null)
                throw new ArgumentNullException("filter", "HasParentFilter requires a filter in this constructor.");

            Filter = filter;
        }

        /// <summary>
        /// Create a has_parent filter that searches using a query.
        /// </summary>
        /// <param name="parentType">The type of the parent document.</param>
        /// <param name="query">The query to use for searching.</param>
        public HasParentFilter(string parentType, IQuery query)
            : this(parentType)
        {
            if (query == null)
                throw new ArgumentNullException("query", "HasParentFilter requires a query in this constructor.");

            Query = query;
        }
    }
}
