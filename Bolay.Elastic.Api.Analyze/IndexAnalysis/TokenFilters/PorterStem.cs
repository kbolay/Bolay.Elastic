using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    /// <summary>
    /// Note, the input to the stemming filter must 
    /// already be in lower case, so you will need 
    /// to use Lower Case Token Filter or Lower Case 
    /// Tokenizer farther down the Tokenizer chain 
    /// in order for this to work properly!. For example, 
    /// when using custom analyzer, make sure the lowercase 
    /// filter comes before the porterStem filter in the list of filters.
    /// </summary>
    public class PorterStem : TokenFilterBase
    {
        public PorterStem() : base(TokenFilterEnum.PorterStem) { }
    }
}
