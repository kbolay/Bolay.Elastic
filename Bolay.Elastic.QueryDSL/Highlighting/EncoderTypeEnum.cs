using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    public sealed class EncoderTypeEnum : TypeSafeEnumBase<EncoderTypeEnum>
    {
        public static readonly EncoderTypeEnum NoEncoding = new EncoderTypeEnum("default");
        public static readonly EncoderTypeEnum Html = new EncoderTypeEnum("html");

        private EncoderTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
