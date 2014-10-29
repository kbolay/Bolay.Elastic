using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Suggestions
{
    /// <summary>
    /// This is a class to hold the results of a suggest request.
    /// </summary>
    [JsonConverter(typeof(SuggestionCollectionSerializer))]
    public class SuggestionCollection : List<Suggestion>
    {
    }
}
