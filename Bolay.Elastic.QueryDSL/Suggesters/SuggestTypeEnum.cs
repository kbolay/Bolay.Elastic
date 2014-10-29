using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Suggesters.Completion;
using Bolay.Elastic.QueryDSL.Suggesters.Phrase;
using Bolay.Elastic.QueryDSL.Suggesters.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters
{
    public sealed class SuggestTypeEnum : TypeSafeEnumBase<SuggestTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly SuggestTypeEnum Term = new SuggestTypeEnum("term", typeof(TermSuggester));
        public static readonly SuggestTypeEnum Phrase = new SuggestTypeEnum("phrase", typeof(PhraseSuggester));
        public static readonly SuggestTypeEnum Completion = new SuggestTypeEnum("completion", typeof(CompletionSuggester));

        private SuggestTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}
