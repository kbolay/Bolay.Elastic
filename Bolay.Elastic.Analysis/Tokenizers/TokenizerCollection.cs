using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers
{
    [JsonConverter(typeof(TokenizerCollectionSerializer))]
    internal class TokenizerCollection : List<ITokenizer>
    {
    }
}
