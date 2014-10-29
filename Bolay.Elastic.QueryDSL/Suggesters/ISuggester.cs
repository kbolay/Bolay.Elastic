using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    public interface ISuggester
    {
        string SuggestName { get; }
        string Field { get; }
        string Text { get; }
        int Size { get; }
    }
}
