using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    public sealed class DynamicSettingEnum : TypeSafeEnumBase<DynamicSettingEnum>
    {
        public object RealValue { get; private set; }

        public static readonly DynamicSettingEnum True = new DynamicSettingEnum("true", true);
        public static readonly DynamicSettingEnum False = new DynamicSettingEnum("false", false);
        public static readonly DynamicSettingEnum Strict = new DynamicSettingEnum("strict", "strict");

        private DynamicSettingEnum(string value, object realValue) : base(value) 
        {
            RealValue = realValue;
            _AllItems.Add(this);
        }
    }
}
