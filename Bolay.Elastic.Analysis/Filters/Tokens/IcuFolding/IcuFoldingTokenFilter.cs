using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.IcuFolding
{
    [JsonConverter(typeof(IcuFoldingTokenFilterSerializer))]
    public class IcuFoldingTokenFilter : TokenFilterBase
    {
        /// <summary>
        /// Gets or sets the unicode set filter.
        /// </summary>
        public string UnicodeSetFilter { get; set; }

        /// <summary>
        /// Create an icu folding token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public IcuFoldingTokenFilter(string name) : base(name, TokenFilterTypeEnum.IcuFolding) { }
    }
}
