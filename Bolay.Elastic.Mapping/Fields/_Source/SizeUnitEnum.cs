using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Source
{
    public sealed class SizeUnitEnum : TypeSafeEnumBase<SizeUnitEnum>
    {
        public static readonly SizeUnitEnum Byte = new SizeUnitEnum("b");
        public static readonly SizeUnitEnum Kilobyte = new SizeUnitEnum("kb");

        private SizeUnitEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
