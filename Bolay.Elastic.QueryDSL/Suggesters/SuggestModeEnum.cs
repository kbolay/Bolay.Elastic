using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    public sealed class SuggestModeEnum : TypeSafeEnumBase<SuggestModeEnum>
    {
        public static readonly SuggestModeEnum Missing = new SuggestModeEnum("missing");
        public static readonly SuggestModeEnum Popular = new SuggestModeEnum("popular");
        public static readonly SuggestModeEnum Always = new SuggestModeEnum("always");

        private SuggestModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
