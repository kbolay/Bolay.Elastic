using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public sealed class DecompounderEnum : TypeSafeEnumBase<DecompounderEnum>
    {
        public static readonly DecompounderEnum Dictionary = new DecompounderEnum("dictionary_decompounder");
        public static readonly DecompounderEnum Hyphenation = new DecompounderEnum("hyphenation_decompounder");

        private DecompounderEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
