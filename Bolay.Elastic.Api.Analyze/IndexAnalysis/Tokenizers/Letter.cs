using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class Letter : TokenizerBase
    {
        public Letter() : base(TokenizerEnum.Letter) { }
    }
}