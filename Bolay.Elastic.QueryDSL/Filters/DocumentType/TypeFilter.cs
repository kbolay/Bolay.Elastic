using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.DocumentType
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-type-filter.html
    /// </summary>
    [JsonConverter(typeof(TypeSerializer))]
    public class TypeFilter : IFilter
    {
        /// <summary>
        /// Gets the type of document that will match the filter.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// Gets the _cache value for the filter. Do not attempt to set the _cache value for this filter.
        /// </summary>
        public bool Cache
        {
            get
            {
                return TypeSerializer._CACHE_DEFAULT;
            }
            set
            {
                throw new ArgumentException("TypeFilter does not allow setting the _cache value.");
            }
        }

        /// <summary>
        /// Gets the _cache_key value for the filter. Do not attempt to set the _cache_key value for this filter.
        /// </summary>
        public string CacheKey
        {
            get
            {
                return null;
            }
            set
            {
                throw new ArgumentException("TypeFilter does not allow setting the _cache_key value.");
            }
        }

        /// <summary>
        /// Gets or sets the filter name.
        /// </summary>
        public string FilterName { get; set; }

        public TypeFilter(string documentType)
        {
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "TypeFilter requires a document type to look for.");

            DocumentType = documentType;
        }
    }
}
