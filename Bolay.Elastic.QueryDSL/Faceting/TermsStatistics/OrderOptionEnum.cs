using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    public sealed class OrderOptionEnum : TypeSafeEnumBase<OrderOptionEnum>
    {
        public static readonly OrderOptionEnum Term = new OrderOptionEnum("term");
        public static readonly OrderOptionEnum ReverseTerm = new OrderOptionEnum("reverse_term");
        public static readonly OrderOptionEnum Count = new OrderOptionEnum("count");
        public static readonly OrderOptionEnum ReverseCount = new OrderOptionEnum("reverse_count");
        public static readonly OrderOptionEnum Total = new OrderOptionEnum("total");
        public static readonly OrderOptionEnum ReverseTotal = new OrderOptionEnum("reverse_total");
        public static readonly OrderOptionEnum Minimum = new OrderOptionEnum("min");
        public static readonly OrderOptionEnum ReverseMinimum = new OrderOptionEnum("reverse_min");
        public static readonly OrderOptionEnum Maximum = new OrderOptionEnum("max");
        public static readonly OrderOptionEnum ReverseMaximum = new OrderOptionEnum("reverse_max");
        public static readonly OrderOptionEnum Average = new OrderOptionEnum("mean");
        public static readonly OrderOptionEnum ReverseAverage = new OrderOptionEnum("reverse_mean");

        private OrderOptionEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
