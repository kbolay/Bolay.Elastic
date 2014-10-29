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
    public class ArabicNormalizationTokenFilter : NormalizationTokenFilterBase
    {
        // TODO: I think i need to implement http://lucene.apache.org/core/4_3_1/analyzers-common/org/apache/lucene/analysis/ar/ArabicNormalizer.html
        /// <summary>
        /// Creates an arabic normalization token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public ArabicNormalizationTokenFilter(string name) : base(name, TokenFilterTypeEnum.ArabicNormalization) { }
    }
}
