using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class PrimaryFirstShardPreference : ShardPreferenceBase
    {
        public PrimaryFirstShardPreference() : base(ShardPreferenceTypeEnum.PrimaryFirst) { }
    }
}
