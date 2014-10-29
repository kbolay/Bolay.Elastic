using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Models
{
    public class NodeComparison
    {
        public Int64 Size { get; set; }
        public ComparisonOperator Operator { get; set; }

        public NodeComparison(string value)
        {
            if (value.Contains(ComparisonOperator.GreaterThan.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.GreaterThan.ToString(), ""));
                Operator = ComparisonOperator.GreaterThan;
            }
            else if (value.Contains(ComparisonOperator.GreaterThan.Alternate))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.GreaterThan.Alternate, ""));
                Operator = ComparisonOperator.GreaterThan;
            }
            else if (value.Contains(ComparisonOperator.GreaterThanOrEqual.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.GreaterThanOrEqual.ToString(), ""));
                Operator = ComparisonOperator.GreaterThanOrEqual;
            }
            else if (value.Contains(ComparisonOperator.GreaterThanOrEqual.Alternate))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.GreaterThanOrEqual.Alternate, ""));
                Operator = ComparisonOperator.GreaterThanOrEqual;
            }
            else if (value.Contains(ComparisonOperator.LessThan.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.LessThan.ToString(), ""));
                Operator = ComparisonOperator.LessThan;
            }
            else if (value.Contains(ComparisonOperator.LessThan.Alternate))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.LessThan.Alternate, ""));
                Operator = ComparisonOperator.LessThan;
            }
            else if(value.Contains(ComparisonOperator.LessThanOrEqual.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.LessThanOrEqual.ToString(), ""));
                Operator = ComparisonOperator.LessThanOrEqual;
            }
            else if (value.Contains(ComparisonOperator.LessThanOrEqual.Alternate))
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.LessThanOrEqual.Alternate, ""));
                Operator = ComparisonOperator.LessThanOrEqual;
            }
            else
            {
                Size = Convert.ToInt64(value.Replace(ComparisonOperator.Equals.ToString(), ""));
                Operator = ComparisonOperator.Equals;
            }

        }
        public NodeComparison(Int64 size, ComparisonOperator comparison = null)
        {
            this.Size = size;

            if (comparison == null)
                this.Operator = ComparisonOperator.Equals;
            else
                this.Operator = comparison;
        }
    }
}
