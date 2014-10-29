using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    /// <summary>
    /// Will create or update a document in the index.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexBulkAction<T> : BulkActionBase
    {
        /// <summary>
        /// This constructor does not require a document id.
        /// If a document id is not supplied the document id will be randomly assigned by ES.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="documentType"></param>
        /// <param name="document"></param>
        public IndexBulkAction(string index, string documentType, T document)
            : this(index, documentType, null, document)
        { }

        /// <summary>
        /// This constructor expects a document id. Null will be accepted, 
        /// but a null document id will cause ES to generate an id for the document.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="documentType"></param>
        /// <param name="documentId"></param>
        /// <param name="document"></param>
        public IndexBulkAction(string index, string documentType, string documentId, T document)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "IndexBulkAction requires an index value.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "IndexBulkAction requires a document type.");
            if (document == null)
                throw new ArgumentNullException("document", "IndexBulkAction requires a document to index.");

            base.OperationType = OperationTypeEnum.Index;
            base.Index = index;
            base.DocumentType = documentType;
            base.DocumentId = documentId;
            base.Document = document;
        }
    }
}
