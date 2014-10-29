using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Terms
{
    public sealed class ExecutionTypeEnum : TypeSafeEnumBase<ExecutionTypeEnum>
    {
        public static readonly ExecutionTypeEnum Map = new ExecutionTypeEnum("map");
        public static readonly ExecutionTypeEnum Ordinals = new ExecutionTypeEnum("ordinals");

        private ExecutionTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
