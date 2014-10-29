using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class BulkRequest
    {
        public readonly IEnumerable<BulkActionBase> Actions;

        public BulkRequest(IEnumerable<BulkActionBase> bulkActions)
        {
            if (bulkActions == null || !bulkActions.Any())
            {
                throw new ArgumentNullException("bulkActions", "Bulk Actions are required for a bulk request.");
            }

            Actions = bulkActions;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (BulkActionBase action in Actions)
            {
                builder.AppendLine(action.ToString());
            }

            return builder.ToString();
        }
    }
}
