using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties
{
    public sealed class SimilarityAlgorithmEnum : TypeSafeEnumBase<SimilarityAlgorithmEnum>
    {
        public static readonly SimilarityAlgorithmEnum Default = new SimilarityAlgorithmEnum("default");
        public static readonly SimilarityAlgorithmEnum BM25 = new SimilarityAlgorithmEnum("BM25");

        private SimilarityAlgorithmEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
