using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.String
{
    public sealed class NormLoadingEnum : TypeSafeEnumBase<NormLoadingEnum>
    {
        public static readonly NormLoadingEnum Lazy = new NormLoadingEnum("lazy");
        public static readonly NormLoadingEnum Eager = new NormLoadingEnum("eager");

        private NormLoadingEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
