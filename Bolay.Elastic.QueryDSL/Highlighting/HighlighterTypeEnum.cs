using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    public class HighlighterTypeEnum : TypeSafeEnumBase<HighlighterTypeEnum>
    {
        public static readonly HighlighterTypeEnum Plain = new HighlighterTypeEnum("plain");
        public static readonly HighlighterTypeEnum Postings = new HighlighterTypeEnum("postings");
        public static readonly HighlighterTypeEnum FastVector = new HighlighterTypeEnum("fvh");

        private HighlighterTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
