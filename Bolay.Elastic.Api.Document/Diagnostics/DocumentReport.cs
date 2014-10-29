using Bolay.Elastic.Api.Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Diagnostics
{
    public class DocumentReport
    {
        public AdminElasticResponse Result;
        public string ObjectId { get; set; }
        public Type TargetType { get; set; }
        public DocumentAction Action { get; set; }
    }

    public enum DocumentAction
    {
        Created,
        Updated,
        Deleted
    }
}
