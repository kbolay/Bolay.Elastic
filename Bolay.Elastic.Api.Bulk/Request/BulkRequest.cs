using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
    /// </summary>
    public class BulkRequest
    {
        internal const string WRITE_CONSISTENCY = "consistency";
        internal const string REFRESH = "refresh";

        /// <summary>
        /// Gets the bulk actions.
        /// </summary>
        public readonly IEnumerable<BulkActionBase> Actions;

        /// <summary>
        /// Gets or sets the shard consistency required for this operation. Defaults to quorum.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-consistency
        /// </summary>
        public WriteConsistencyEnum WriteConsistency { get; set; }

        /// <summary>
        /// Gets or sets whether the shards updated by the bulk request will be updated immediately. 
        /// This can result in a higher overhead cost, so only use this is the natural process is too slow.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html#bulk-refresh
        /// </summary>
        public bool Refresh { get; set; }

        /// <summary>
        /// Create a bulk request.
        /// </summary>
        /// <param name="bulkActions"></param>
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
