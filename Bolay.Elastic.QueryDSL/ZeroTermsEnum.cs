using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public class ZeroTermsEnum : TypeSafeEnumBase<ZeroTermsEnum>
    {
        public static readonly ZeroTermsEnum AllTerms = new ZeroTermsEnum("all");
        public static readonly ZeroTermsEnum None = new ZeroTermsEnum("none");

        private ZeroTermsEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
