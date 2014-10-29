using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public sealed class SortOrderEnum : TypeSafeEnumBase<SortOrderEnum>
    {
        public static readonly SortOrderEnum Ascending = new SortOrderEnum("asc");
        public static readonly SortOrderEnum Descending = new SortOrderEnum("desc");

        private SortOrderEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
