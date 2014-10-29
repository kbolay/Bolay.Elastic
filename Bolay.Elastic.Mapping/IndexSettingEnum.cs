using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    public sealed class IndexSettingEnum : TypeSafeEnumBase<IndexSettingEnum>
    {
        //TODO: Determine if my enumeration pattern would be better suited with attributes controlling many of these things

        public static readonly IndexSettingEnum Analyzed = new IndexSettingEnum("analyzed");
        public static readonly IndexSettingEnum NotAnalyzed = new IndexSettingEnum("not_analyzed");
        public static readonly IndexSettingEnum No = new IndexSettingEnum("no");

        private IndexSettingEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
