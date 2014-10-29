using Bolay.Elastic.Api.Bulk.Request;
using Bolay.Elastic.Api.Bulk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Models
{
    public class BulkActionFailure
    {
        public readonly BulkActionBase Action;
        public readonly ActionResponse Response;

        public BulkActionFailure(BulkActionBase action, ActionResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            Action = action;
            Response = response;
        }
    }
}
