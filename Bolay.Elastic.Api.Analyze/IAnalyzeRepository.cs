using Bolay.Elastic.Api.Analyze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze
{
    public interface IAnalyzeRepository
    {
        IEnumerable<AnalyzedToken> AnalyzeText(AnalyzeRequest request);
    }
}
