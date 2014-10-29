using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    public sealed class StoreSettingEnum : TypeSafeEnumBase<StoreSettingEnum>
    {
        public static readonly StoreSettingEnum Yes = new StoreSettingEnum("yes");
        public static readonly StoreSettingEnum No = new StoreSettingEnum("no");

        private StoreSettingEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
