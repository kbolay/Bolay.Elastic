using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.EdgeNGram
{
    public sealed class SideEnum : TypeSafeEnumBase<SideEnum>
    {
        public static readonly SideEnum Front = new SideEnum("front");
        public static readonly SideEnum Back = new SideEnum("back");

        private SideEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
