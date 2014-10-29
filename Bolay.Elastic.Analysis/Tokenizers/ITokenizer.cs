using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    [JsonConverter(typeof(TokenizerSerializer))]
    public interface ITokenizer : IAnalysisVersion
    {
        string Name { get; }
        TokenizerTypeEnum Type { get; }
    }
}
