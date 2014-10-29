using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.GeoBoundingBox
{
    public sealed class ExecutionTypeEnum : TypeSafeEnumBase<ExecutionTypeEnum>
    {
        public static readonly ExecutionTypeEnum Memory = new ExecutionTypeEnum("memory");
        public static readonly ExecutionTypeEnum Indexed = new ExecutionTypeEnum("indexed");

        private ExecutionTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
