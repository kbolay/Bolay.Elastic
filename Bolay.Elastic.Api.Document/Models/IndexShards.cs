using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class IndexShards
    {
        public string Index { get; set; }
        public Shards Shards { get; set; }
    }
}
