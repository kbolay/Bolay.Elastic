using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    public sealed class OrderingEnum : TypeSafeEnumBase<OrderingEnum>
    {
        public static readonly OrderingEnum Count = new OrderingEnum("count");
        public static readonly OrderingEnum Term = new OrderingEnum("term");
        public static readonly OrderingEnum ReverseCount = new OrderingEnum("reverse_count");
        public static readonly OrderingEnum ReverseTerm = new OrderingEnum("reverse_term");

        private OrderingEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
