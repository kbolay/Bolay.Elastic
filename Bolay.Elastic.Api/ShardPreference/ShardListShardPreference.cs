using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class ShardListShardPreference : ShardPreferenceBase
    {
        /// <summary>
        /// Gets the shards to prefer.
        /// </summary>
        public IEnumerable<string> Shards { get; private set; }

        public ShardListShardPreference(IEnumerable<string> shards) 
            : base(ShardPreferenceTypeEnum.ShardList)
        {
            if(shards == null || shards.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("shards", "ShardListShardPreference requires at least one shard.");

            Shards = shards.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PreferenceType.ToString());
            builder.Append(_PREFERENCE_VALUE_DELIMITER);
            builder.Append(string.Join(_MULTI_VALUE_DELIMITER, Shards));

            return builder.ToString();
        }
    }
}
