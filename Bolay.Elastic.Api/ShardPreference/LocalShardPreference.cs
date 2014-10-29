using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public class LocalShardPreference : ShardPreferenceBase
    {
        public LocalShardPreference() : base(ShardPreferenceTypeEnum.Local) { }
    }
}
