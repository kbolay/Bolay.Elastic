using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    public sealed class LevelSetting : TypeSafeEnumBase<LevelSetting>
    {
        public static readonly LevelSetting Cluster = new LevelSetting("cluster");
        public static readonly LevelSetting Indices = new LevelSetting("indices");
        public static readonly LevelSetting Shards = new LevelSetting("shards");

        private LevelSetting(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
