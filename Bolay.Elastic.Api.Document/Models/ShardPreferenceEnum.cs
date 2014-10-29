using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    /// <summary>
    /// Only one of these properties may be used.
    /// </summary>
    public class ShardPreference
    {
        private const string _PRIMARY_VALUE = "_primary";
        private const string _LOCAL_VALUE = "_local";

        /// <summary>
        /// The request will only be executed on the primary shards.
        /// </summary>
        public bool UsePrimaryShards { get; set; }

        /// <summary>
        /// The request will only be executed on the locally allocated shards, if possible.
        /// </summary>
        public bool UseLocalShards { get; set; }

        /// <summary>
        /// This option is used to ensure the same shards will be used for this value, 
        /// helping with "jumping values" for hitting different shards in different 
        /// refresh states. Potentially useful values would be web session id, 
        /// or the user name.
        /// </summary>
        public string CustomShards { get; set; }

        public override string ToString()
        {
            if (UsePrimaryShards)
                return _PRIMARY_VALUE;
            else if (UseLocalShards)
                return _LOCAL_VALUE;
            else if (!string.IsNullOrWhiteSpace(CustomShards))
                return CustomShards;
            else
                return null;
        }
    }
}
