using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public sealed class OperatorEnum : TypeSafeEnumBase<OperatorEnum>
    {
        public static readonly OperatorEnum And = new OperatorEnum("and");
        public static readonly OperatorEnum Or = new OperatorEnum("or");

        private OperatorEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
