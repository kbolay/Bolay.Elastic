using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Span
{
    public sealed class SpanQueryTypeEnum : TypeSafeEnumBase<SpanQueryTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly SpanQueryTypeEnum First = new SpanQueryTypeEnum("span_first", typeof(SpanFirstQuery));
        public static readonly SpanQueryTypeEnum Term = new SpanQueryTypeEnum("span_term", typeof(SpanTermQuery));
        public static readonly SpanQueryTypeEnum Near = new SpanQueryTypeEnum("span_near", typeof(SpanNearQuery));
        public static readonly SpanQueryTypeEnum MultiTerm = new SpanQueryTypeEnum("span_multi", typeof(SpanMultiTermQuery));
        public static readonly SpanQueryTypeEnum Not = new SpanQueryTypeEnum("span_not", typeof(SpanNotQuery));
        public static readonly SpanQueryTypeEnum Or = new SpanQueryTypeEnum("span_or", typeof(SpanOrQuery));

        private SpanQueryTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;

            _AllItems.Add(this);
        }
    }
}
