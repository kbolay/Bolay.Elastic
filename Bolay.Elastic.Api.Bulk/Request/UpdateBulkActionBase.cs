using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    [JsonConverter(typeof(UpdateBulkActionBaseSerializer))]
    public abstract class UpdateBulkActionBase : BulkActionBase
    {
        internal const string RETRY_ON_CONFLICT = "retry_on_conflict";

        /// <summary>
        /// Gets or sets the number of attempts allowed to update the document in the case of a version conflict.
        /// </summary>
        public int? RetriesOnConflict { get; set; }

        public UpdateBulkActionBase(string index, string type, string documentId)
            : base(index, type, documentId)
        {}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
