using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.MultiGet
{
    public class MultiGetRequestedDocument
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_type")]
        public string DocumentType { get; set; }

        [JsonProperty("_id")]
        public string DocumentId { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Not specifying the index by usings this constructor
        /// means that the index must be provided to the MultiGetDocumentRequest.
        /// </summary>
        /// <param name="documentType">The _type of the document.</param>
        /// <param name="documentId">The _id of the document.</param>
        public MultiGetRequestedDocument(string documentType, string documentId) : this(null, documentType, documentId) { }

        /// <summary>
        /// Specifying the index here means that you can specify different indexes
        /// to search in for the different documents.
        /// </summary>
        /// <param name="index">The _index of the document. If a null of empty string value is used
        /// the index will be expected in the MultiGetDocumentRequest.</param>
        /// <param name="documentType">The _type of the document.</param>
        /// <param name="documentId">The _id of the document.</param>
        public MultiGetRequestedDocument(string index, string documentType, string documentId)
        {
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "MultiGetRequestedDocuments require a document type.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "MultiGetRequestedDocuments require a document id.");

            Index = index;
            DocumentType = documentType;
            DocumentId = documentId;
        }
    }
}
