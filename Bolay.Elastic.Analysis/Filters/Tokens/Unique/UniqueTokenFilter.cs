using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Unique
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-unique-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(UniqueTokenFilterSerializer))]
    public class UniqueTokenFilter : TokenFilterBase
    {
        internal const bool _ONLY_ON_SAME_POSITION_DEFAULT = false;

        /// <summary>
        /// Gets or sets the only_on_same_position boolean value.
        /// </summary>
        public bool OnlyOnSamePosition { get; set; }

        /// <summary>
        /// Creates a unique token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public UniqueTokenFilter(string name) 
            : base(name, TokenFilterTypeEnum.Unique)
        {
            OnlyOnSamePosition = _ONLY_ON_SAME_POSITION_DEFAULT;
        }
    }
}
