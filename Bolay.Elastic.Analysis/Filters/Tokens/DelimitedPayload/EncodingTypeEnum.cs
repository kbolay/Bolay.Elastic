using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.DelimitedPayload
{
    public sealed class EncodingTypeEnum : TypeSafeEnumBase<EncodingTypeEnum>
    {
        public static readonly EncodingTypeEnum Integer = new EncodingTypeEnum("int");
        public static readonly EncodingTypeEnum Float = new EncodingTypeEnum("float");
        public static readonly EncodingTypeEnum Identity = new EncodingTypeEnum("identity");

        private EncodingTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
