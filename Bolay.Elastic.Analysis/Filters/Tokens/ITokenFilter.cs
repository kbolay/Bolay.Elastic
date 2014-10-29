using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens
{
    public interface ITokenFilter : IAnalysisVersion
    {
        string Name { get; }
        TokenFilterTypeEnum Type { get; }
    }
}
