using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class UpdateBulkAction<T> : BulkActionBase
    {
        private const string _DOC = "doc";
        private const string _RETRY_ON_CONFLICT = "retry_on_conflict";

        public override string Action
        {
            get { return "update"; }
        }

        public readonly T Document;

        public int? RetriesOnConflict { get; set; }

        public UpdateBulkAction(string index, string type, string documentId, T document)
            : base(index, type, documentId)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            Document = document;
        }

        public UpdateBulkAction(string index, string type, string documentId, T document, int retriesOnConflict)
            : this(index, type, documentId, document)
        {
            if (retriesOnConflict <= 0)
            {
                throw new ArgumentOutOfRangeException("retriesOnConflict", "Must be greater than 0.");
            }

            RetriesOnConflict = retriesOnConflict;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if(!RetriesOnConflict.HasValue)
            {
                builder.AppendLine(base.ToString());
            }
            else
            {
                builder.AppendLine(string.Format("{{\"{0}\":{{\"_index\":\"{1}\",\"_type\":\"{2}\",\"_id\":\"{3}\",\"{4}\":{5}}}}}", Action, Index, Type, DocumentId, _RETRY_ON_CONFLICT, RetriesOnConflict));
            }

            builder.Append(string.Format("{{\"{0}\":{{{1}}}", _DOC, JsonConvert.SerializeObject(Document)));
            return base.ToString();
        }
    }
}
