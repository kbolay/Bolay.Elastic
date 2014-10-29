using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    public sealed class TermVectorEnum : TypeSafeEnumBase<TermVectorEnum>
    {
        public static readonly TermVectorEnum No = new TermVectorEnum("no");
        public static readonly TermVectorEnum Yes = new TermVectorEnum("yes");
        public static readonly TermVectorEnum WithOffsets = new TermVectorEnum("with_offsets");
        public static readonly TermVectorEnum WithPositions = new TermVectorEnum("with_positions");
        public static readonly TermVectorEnum WithPositionsOffsets = new TermVectorEnum("with_positions_offsets");

        private TermVectorEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
