using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    public sealed class MissingTypeEnum : TypeSafeEnumBase<MissingTypeEnum>
    {
        public static readonly MissingTypeEnum First = new MissingTypeEnum("_first");
        public static readonly MissingTypeEnum Last = new MissingTypeEnum("_last");

        private MissingTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
