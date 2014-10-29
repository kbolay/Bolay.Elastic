using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class PreferNodeShardPreference : ShardPreferenceBase
    {
        /// <summary>
        /// Gets the node specified.
        /// </summary>
        public string Node { get; private set; }

        public PreferNodeShardPreference(string node)
            : base(ShardPreferenceTypeEnum.PreferNode)
        {
            if (string.IsNullOrWhiteSpace(node))
                throw new ArgumentNullException("node", "PreferNodeShardPreference requiers a node.");

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
