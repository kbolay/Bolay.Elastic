using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Range
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-range-filter.html#_execution
    /// </summary>
    public sealed class ExecutionTypeEnum : TypeSafeEnumBase<ExecutionTypeEnum>
    {
        /// <summary>
        /// Usually a good option for smaller ranges.
        /// </summary>
        public static readonly ExecutionTypeEnum Index = new ExecutionTypeEnum("index");

        /// <summary>
        /// Usually a good option for larger ranges. The fielddata option uses more memory.
        /// </summary>
        public static readonly ExecutionTypeEnum FieldData = new ExecutionTypeEnum("fielddata");

        private ExecutionTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
