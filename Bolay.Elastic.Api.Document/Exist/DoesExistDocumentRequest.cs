using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Exist
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
    /// </summary>
    public class DoesExistDocumentRequest : DocumentRequestBase
    {
        internal const string EXCLUDE_METADATA_VALUE = "_source";

        /// <summary>
        /// Exclude metadata that normally surrounds the _source document.
        /// </summary>
        public bool ExcludeMetaData { get; set; }

        /// <summary>
        /// Create a GET document request.
        /// </summary>
        /// <param name="index">The index or alias of the ES cluster containing the document. Required.</param>
        /// <param name="documentId">The _id of the ES document. Required.</param>
        /// <param name="documentType">
        /// The type of document. If _all is used the first document 
        /// matching the documentId will be returned, without regard to type.
        /// </param>
        public DoesExistDocumentRequest(string index, string documentId, string documentType = "_all")
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "The index value must be populated.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "The documentId value must be populated.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "The documentType value must be populated.");

            Index = index;
            DocumentId = documentId;
            DocumentType = documentType;
        }
    }
}
