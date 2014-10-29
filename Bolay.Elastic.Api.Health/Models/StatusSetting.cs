using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    public sealed class StatusSetting : TypeSafeEnumBase<StatusSetting>
    {
        public static readonly StatusSetting Green = new StatusSetting("green");
        public static readonly StatusSetting Yellow = new StatusSetting("yellow");
        public static readonly StatusSetting Red = new StatusSetting("red");

        private StatusSetting(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
