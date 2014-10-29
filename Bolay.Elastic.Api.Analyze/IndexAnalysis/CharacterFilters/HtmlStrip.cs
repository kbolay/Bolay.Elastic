using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters
{
    public class HtmlStrip : CharacterFilterBase
    {
        public HtmlStrip() : base(CharacterFilterEnum.HtmlStrip) { }
    }
}
