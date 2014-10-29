using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.Laplace;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.LinearInterpolation;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.StupidBackoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-suggesters-phrase.html#_smoothing_models
    /// </summary>
    public sealed class SmoothingTypeEnum : TypeSafeEnumBase<SmoothingTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly SmoothingTypeEnum StupidBackoff = new SmoothingTypeEnum("studpid_backoff", typeof(StupidBackoffSmoothing));
        public static readonly SmoothingTypeEnum Laplace = new SmoothingTypeEnum("laplace", typeof(LaplaceSmoothing));
        public static readonly SmoothingTypeEnum LinearInterpolation = new SmoothingTypeEnum("linear_interpolation", typeof(LinearInterpolationSmoothing));

        private SmoothingTypeEnum(string value, Type implementationType)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
