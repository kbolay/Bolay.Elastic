using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class ArabicNormalization : TokenFilterBase
    {
        public ArabicNormalization() : base(TokenFilterEnum.ArabicNormalization) { }
    }

    public class PersianNormalization : TokenFilterBase
    {
        public PersianNormalization() : base(TokenFilterEnum.PersianNormalization) { }
    }
}
