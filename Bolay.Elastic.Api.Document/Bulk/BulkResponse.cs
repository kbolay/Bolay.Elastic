using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    public class BulkResponse
    {
        public IEnumerable<AdminElasticResponse> Results { get; set; }
    }
}
