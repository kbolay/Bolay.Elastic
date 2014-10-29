using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.Document.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    /// <summary>
    /// Update an existing document.
    /// </summary>
    public class UpdateBulkAction : BulkActionBase
    {
        /// <summary>
        /// This constructor expects a document id. Null will not be accepted.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="documentType"></param>
        /// <param name="documentId"></param>
        /// <param name="document"></param>
        public UpdateBulkAction(string index, string documentType, string documentId, UpdateContent updateContent)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "UpdateBulkAction requires an index value.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "UpdateBulkAction requires a document type.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "UpdateBulkAction requires a document id.");
            if (updateContent == null)
                throw new ArgumentNullException("document", "UpdateBulkAction requires update content.");

            base.OperationType = OperationTypeEnum.Create;
            base.Index = index;
            base.DocumentType = documentType;
            base.DocumentId = documentId;
            base.Document = updateContent;
        }
    }
}
