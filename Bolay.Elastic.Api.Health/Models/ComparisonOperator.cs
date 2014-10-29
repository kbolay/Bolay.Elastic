using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    public sealed class ComparisonOperator : TypeSafeEnumBase<ComparisonOperator>
    {
        public string Alternate { get; set; }

        public static readonly ComparisonOperator Equals = new ComparisonOperator("=", null);
        public static readonly ComparisonOperator GreaterThan = new ComparisonOperator("gt", ">");
        public static readonly ComparisonOperator GreaterThanOrEqual = new ComparisonOperator("ge", ">=");
        public static readonly ComparisonOperator LessThan = new ComparisonOperator("lt", "<");
        public static readonly ComparisonOperator LessThanOrEqual = new ComparisonOperator("le", "<=");

        private ComparisonOperator(string value, string alternate)
            : base(value)
        {
            this.Alternate = alternate;
            _AllItems.Add(this);
        }
    }
}
