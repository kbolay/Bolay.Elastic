using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public sealed class DecayFunctionsEnum : TypeSafeEnumBase<DecayFunctionsEnum>
    {
        public static readonly DecayFunctionsEnum Gauss = new DecayFunctionsEnum("gauss");
        public static readonly DecayFunctionsEnum Exponential = new DecayFunctionsEnum("exp");
        public static readonly DecayFunctionsEnum Linear = new DecayFunctionsEnum("linear");

        private DecayFunctionsEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
