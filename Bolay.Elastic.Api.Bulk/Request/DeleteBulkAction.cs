using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class DeleteBulkAction : BulkActionBase
    {
        public override string Action
        {
            get { return "delete"; }
        }

        public DeleteBulkAction(string index, string type, string documentId)
            : base(index, type, documentId)
        { }
    }
}
