using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    public class DeleteBulkAction : BulkActionBase
    {
        /// <summary>
        /// This constructor expects a document id. Null will not be accepted.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="documentType"></param>
        /// <param name="documentId"></param>
        /// <param name="document"></param>
        public DeleteBulkAction(string index, string documentType, string documentId)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "UpdateBulkAction requires an index value.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "UpdateBulkAction requires a document type.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "UpdateBulkAction requires a document id.");

            base.OperationType = OperationTypeEnum.Create;
            base.Index = index;
            base.DocumentType = documentType;
            base.DocumentId = documentId;
            base.Document = null;
        }
    }
}
