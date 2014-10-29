using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Models
{
    public sealed class SizeUnit : TypeSafeEnumBase<SizeUnit>
    {
        public static readonly SizeUnit Byte = new SizeUnit("b");
        public static readonly SizeUnit Kilobyte = new SizeUnit("kb");

        private SizeUnit(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
