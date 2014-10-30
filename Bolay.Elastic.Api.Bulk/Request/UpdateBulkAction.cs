using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-update
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonConverter(typeof(UpdateBulkActionSerializer))]
    public class UpdateBulkAction<T> : BulkActionBase
    {
        internal const string RETRY_ON_CONFLICT = "_retry_on_conflict";
        internal const string PARTIAL_DOCUMENT = "doc";
        internal const string UPSERT = "upsert";
        internal const string DOC_AS_UPSERT = "doc_as_upsert";

        /// <summary>
        /// Gets the action.
        /// </summary>
        public override string Action
        {
            get { return "update"; }
        }

        /// <summary>
        /// Gets or sets the number of attempts allowed to update the document in the case of a version conflict.
        /// </summary>
        public int? RetriesOnConflict { get; set; }

        /// <summary>
        /// Gets the document.
        /// </summary>
        public readonly T Document;

        /// <summary>
        /// Gets the update script.
        /// </summary>
        public readonly Script UpdateScript;

        /// <summary>
        /// Gets the document to index if the document does not exist.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html#upserts
        /// </summary>
        public readonly T UpsertDocument;

        /// <summary>
        /// Gets whether to create the document if it does not exist.
        /// </summary>
        public readonly bool IsUpsert;

        /// <summary>
        /// Creates an update bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        /// <param name="document">Sets the document.</param>
        public UpdateBulkAction(string index, string type, string documentId, T document, bool isUpsert = false)
            : base(index, type, documentId)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            Document = document;
            IsUpsert = isUpsert;
        }

        /// <summary>
        /// Create an update bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        /// <param name="document">Sets the document.</param>
        /// <param name="upsertDocument">Sets the upsert document.</param>
        public UpdateBulkAction(string index, string type, string documentId, T document, T upsertDocument)
            : this(index, type, documentId, document)
        {
            if (upsertDocument == null)
            {
                throw new ArgumentNullException("upsertDocument");
            }

            UpsertDocument = upsertDocument;
        }

        /// <summary>
        /// Create an update bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        /// <param name="updateScript">Sets the update script.</param>
        public UpdateBulkAction(string index, string type, string documentId, Script updateScript)
            : base(index, type, documentId)
        {
            if (updateScript == null)
            {
                throw new ArgumentNullException("updateScript");
            }

            UpdateScript = updateScript;
        }

        /// <summary>
        /// Create an update bulk action request.
        /// </summary>
        /// <param name="index">Sets the index.</param>
        /// <param name="type">Sets the type.</param>
        /// <param name="documentId">Sets the document id.</param>
        /// <param name="updateScript">Sets the update script.</param>
        /// <param name="upsertDocument">Sets the upsert document.</param>
        public UpdateBulkAction(string index, string type, string documentId, Script updateScript, T upsertDocument)
            : this(index, type, documentId, updateScript)
        {
            if (upsertDocument == null)
            {
                throw new ArgumentNullException("upsertDocument");
            }

            UpsertDocument = upsertDocument;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(base.ToString());

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(PARTIAL_DOCUMENT, Document);
            UpdateScript.Serialize(fieldDict);
            fieldDict.AddObject(UPSERT, UpsertDocument);
            fieldDict.AddObject(DOC_AS_UPSERT, IsUpsert, false);

            builder.AppendLine(JsonConvert.SerializeObject(fieldDict));
            return builder.ToString();
        }
    }
}
