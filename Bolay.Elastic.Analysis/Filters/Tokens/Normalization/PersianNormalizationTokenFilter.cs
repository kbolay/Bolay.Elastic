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
    public class PersianNormalizationTokenFilter : NormalizationTokenFilterBase
    {
        // TODO: make this http://lucene.apache.org/core/4_3_1/analyzers-common/org/apache/lucene/analysis/fa/PersianNormalizer.html
        /// <summary>
        /// Creates a persian normalization token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public PersianNormalizationTokenFilter(string name) : base(name, TokenFilterTypeEnum.PersianNormalization) { }
    }
}
