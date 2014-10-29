using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public abstract class BulkActionBase
    {
        public abstract string Action { get; }

        public readonly string Index;
        public readonly string Type;
        public readonly string DocumentId;

        protected BulkActionBase(string index, string type) 
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("index");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("type");
            }

            Index = index;
            Type = type;
        }

        protected BulkActionBase(string index, string type, string documentId)
            : this(index, type)
        {
            if (string.IsNullOrWhiteSpace(documentId))
            {
                throw new ArgumentNullException("documentId");
            }

            DocumentId = documentId;
        }

        public override string ToString()
        {
            string result = null;
            if(string.IsNullOrWhiteSpace(DocumentId))
            {
                result = string.Format("{{\"{0}\":{{\"_index\":\"{1}\",\"_type\":\"{2}\"}}}}", Action, Index, Type);
            }
            else
            {
                result = string.Format("{{\"{0}\":{{\"_index\":\"{1}\",\"_type\":\"{2}\",\"_id\":\"{3}\"}}}}", Action, Index, Type, DocumentId);
            }

            return result;
        }
    }
}
