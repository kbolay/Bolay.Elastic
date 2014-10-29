using Bolay.Elastic.Models;
using Bolay.Elastic.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    /// <summary>
    /// The possible query string keys that can be submitted to the health api.
    /// </summary>
    public class HealthRequest
    {
        /// <summary>
        /// Can be one of cluster, indices or shards. 
        /// Controls the details level of the health information returned. 
        /// Defaults to cluster.
        /// </summary>
        public LevelSetting Level { get; set; }

        
        /// <summary>
        /// One of green, yellow or red. Will wait (until the timeout provided)
        /// until the status of the cluster changes to the one provided. 
        /// By default, will not wait for any status.
        /// </summary>
        public StatusSetting WaitForStatus { get; set; }

        /// <summary>
        /// A number controlling to how many relocating shards to wait for. 
        /// Usually will be 0 to indicate to wait till all relocation have happened. 
        /// Defaults to not to wait.
        /// </summary>
        public int? WaitForRelocatingShards { get; set; }

        /// <summary>
        /// The request waits until the specified number N of nodes is available. 
        /// It also accepts >=N, <=N, >N and <N. Alternatively, it is possible to 
        /// use ge(N), le(N), gt(N) and lt(N) notation.
        /// </summary>
        public NodeComparison WaitForNodes { get; set; }

        /// <summary>
        /// A time based parameter controlling how long to wait if one of the 
        /// WaitForXXX are provided. Defaults to 30s.
        /// </summary>
        public TimeValue TimeOut { get; set; }

        public bool IsEmpty
        {
            get
            {
                if (Level != null) return true;
                if (WaitForStatus != null) return true;
                if (WaitForNodes != null) return true;
                if (TimeOut != null) return true;

                return false;
            }
        }

        public override string ToString()
        {
            if (IsEmpty)
                return null;

            StringBuilder builder = new StringBuilder();

            if (Level != null) { builder = HttpRequest.AddToQueryString(builder, "level", Level.ToString()); }
            if (WaitForStatus != null) { builder = HttpRequest.AddToQueryString(builder, "wait_for_status", WaitForStatus.ToString()); }
            if (WaitForRelocatingShards.HasValue) { builder = HttpRequest.AddToQueryString(builder, "wait_for_relocating_shards", WaitForRelocatingShards.ToString()); }
            if (WaitForNodes != null) { builder = HttpRequest.AddToQueryString(builder, "wait_for_nodes", WaitForNodes.ToString()); }
            if (TimeOut == null) { builder = HttpRequest.AddToQueryString(builder, "timeout", TimeOut.ToString()); }

            return builder.ToString();
        }
    }
}
