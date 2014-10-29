using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Normalization
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-normalization-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(NormalizationTokenFilterBaseSerializer))]
    public abstract class NormalizationTokenFilterBase : TokenFilterBase
    {
        /// <summary>
        /// Create a normalization token filter base.
        /// </summary>
        /// <param name="name">Sets the name of the filter token.</param>
        /// <param name="type">Sets the type of the normalization token filter.</param>
        public NormalizationTokenFilterBase(string name, TokenFilterTypeEnum type)
            : base(name, type)
        { }
    }
}
