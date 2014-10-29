using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Terms
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-terms-filter.html
    /// </summary>
    [JsonConverter(typeof(TermsSerializer))]
    public class TermsFilter : FilterBase
    {
        /// <summary>
        /// Gets the field to search against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the specified terms to search for.
        /// </summary>
        public IEnumerable<object> Terms { get; private set;}

        /// <summary>
        /// Gets or sets the execution type for the terms query. Only used when specifying the terms.
        /// Defaults to plain.
        /// </summary>
        public ExecutionTypeEnum ExecutionType { get; set; }

        /// <summary>
        /// Gets the index that contains the document to retrieve terms from.
        /// </summary>
        public string Index { get; private set; }

        /// <summary>
        /// Gets the document type that contains the document to retrieve terms from.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// Gets the document id to retrieve the terms from.
        /// </summary>
        public string DocumentId { get; private set; }

        /// <summary>
        /// Gets the path to the property in the document to retrieve the terms from.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the specific routing needed to retrieve the document that terms will be retrieved from.
        /// </summary>
        public string Routing { get; set; }

        private TermsFilter(string field)
        { 
            if(string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "TermsFilter requires a field.");

            Field = field;
            ExecutionType = TermsSerializer._EXECUTION_DEFAULT;
            Cache = TermsSerializer._EXECUTION_DEFAULT.CacheDefault;
        }

        public TermsFilter(string field, IEnumerable<object> terms)
            : this(field)
        {
            if (terms == null || terms.All(x => x == null))
                throw new ArgumentNullException("terms", "TermsFilter requires terms in this constructor.");

            Terms = terms.Where(x => x != null);
            ExecutionType = TermsSerializer._EXECUTION_DEFAULT;
            Cache = TermsSerializer._EXECUTION_DEFAULT.CacheDefault;
        }

        public TermsFilter(string field, string index, string documentType, string documentId, string path)
            : this(field)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "TermsFilter requires an index in this contructor.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "TermsFilter requires a document type in this contructor.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "TermsFilter requires a document id in this contructor.");
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "TermsFilter requires path in this contructor.");

            Index = index;
            DocumentType = documentType;
            DocumentId = documentId;
            Path = path;
            Cache = TermsSerializer._INDEXED_TERMS_CACHE_DEFAULT;
        }
    }
}
