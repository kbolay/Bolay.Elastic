using Bolay.Elastic.Api.Bulk.Request;
using Bolay.Elastic.Api.Bulk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk
{
    public interface IBulkRepository
    {
        BulkResponse DoBulkRequest(BulkRequest bulkActions);
    }
}
