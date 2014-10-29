using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class OnlyNodeShardPreference : ShardPreferenceBase
    {
        /// <summary>
        /// Gets the node specified.
        /// </summary>
        public string Node { get; private set; }

        public OnlyNodeShardPreference(string node)
            : base(ShardPreferenceTypeEnum.OnlyNode)
        {
            if (string.IsNullOrWhiteSpace(node))
                throw new ArgumentNullException("node", "OnlyNodeShardPreference requiers a node.");

            Node = node;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PreferenceType.ToString());
            builder.Append(_PREFERENCE_VALUE_DELIMITER);
            builder.Append(Node);

            return builder.ToString();
        }
    }
}
