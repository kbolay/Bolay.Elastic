using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class CustomShardPreference : ShardPreferenceBase
    {
        /// <summary>
        /// Gets the custom preference value.
        /// </summary>
        public string CustomPreference { get; private set; }

        /// <summary>
        /// Create a custom shard preference value.
        /// </summary>
        /// <param name="customPreference">Sets the custom shard preference value.</param>
        public CustomShardPreference(string customPreference)
            : base(ShardPreferenceTypeEnum.Custom)
        {
            if (string.IsNullOrWhiteSpace(customPreference))
                throw new ArgumentNullException("customPreference", "CustomShardPreference requires a custom preference value.");

            CustomPreference = customPreference;
        }

        public override string ToString()
        {
            return CustomPreference;
        }
    }
}
