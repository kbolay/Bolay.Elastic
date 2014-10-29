using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class IndexBulkAction<T> : BulkActionBase where T : class
    {
        public override string Action
        {
            get { return "index"; }
        }

        public readonly T Document;

        public IndexBulkAction(string index, string type, T document)
            : base(index, type)
        {
            if (document == default(T))
            {
                throw new ArgumentNullException("document");
            }

            Document = document;
        }

        public IndexBulkAction(string index, string type, string documentId, T document)
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
            builder.Append(JsonConvert.SerializeObject(Document));

            return builder.ToString();
        }
    }
}
