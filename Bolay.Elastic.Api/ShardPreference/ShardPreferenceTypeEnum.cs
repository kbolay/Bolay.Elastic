using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public sealed class ShardPreferenceTypeEnum : TypeSafeEnumBase<ShardPreferenceTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly ShardPreferenceTypeEnum Primary = new ShardPreferenceTypeEnum("_primary", typeof(PrimaryShardPreference));
        public static readonly ShardPreferenceTypeEnum PrimaryFirst = new ShardPreferenceTypeEnum("_primary_first", typeof(PrimaryFirstShardPreference));
        public static readonly ShardPreferenceTypeEnum Local = new ShardPreferenceTypeEnum("_local", typeof(LocalShardPreference));
        public static readonly ShardPreferenceTypeEnum OnlyNode = new ShardPreferenceTypeEnum("_only_node", typeof(OnlyNodeShardPreference));
        public static readonly ShardPreferenceTypeEnum PreferNode = new ShardPreferenceTypeEnum("_prefer_node", typeof(PreferNodeShardPreference));
        public static readonly ShardPreferenceTypeEnum ShardList = new ShardPreferenceTypeEnum("_shards", typeof(ShardListShardPreference));
        public static readonly ShardPreferenceTypeEnum Custom = new ShardPreferenceTypeEnum("custom", typeof(CustomShardPreference));

        private ShardPreferenceTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
