using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens
{
    [JsonConverter(typeof(TokenFilterCollectionSerializer))]
    internal class TokenFilterCollection : List<ITokenFilter>
    {
    }
}
