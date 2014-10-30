using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class DeleteBulkAction : BulkActionBase
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        public override string Action
        {
            get { return "delete"; }
        }

        /// <summary>
        /// Create a delete bulk action.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        public DeleteBulkAction(string index, string type, string documentId)
            : base(index, type, documentId)
        { }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            return builder.ToString();
        }
    }
}
