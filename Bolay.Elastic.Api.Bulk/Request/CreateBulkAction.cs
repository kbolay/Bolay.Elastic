using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class CreateBulkAction<T>  : BulkActionBase where T : class
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        public override string Action
        {
            get { return "create"; }
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        public readonly T Document;

        /// <summary>
        /// Create a create bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="document">Sets the document.</param>
        public CreateBulkAction(string index, string type, T document)
            : base(index, type)
        {
            if (document == default(T))
            {
                throw new ArgumentNullException("document");
            }

            Document = document;
        }

        /// <summary>
        /// Create a create bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        /// <param name="document">Sets the document.</param>
        public CreateBulkAction(string index, string type, string documentId, T document)
            : base(index, type, documentId)
        {
            if (document == default(T))
            {
                throw new ArgumentNullException("document");
            }

            Document = document;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine(JsonConvert.SerializeObject(Document));

            return builder.ToString();
        }
    }
}
