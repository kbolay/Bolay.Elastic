using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public sealed class RewriteMethodsEnum : TypeSafeEnumBase<RewriteMethodsEnum>
    {
        public static readonly RewriteMethodsEnum ConstantScoreAuto = new RewriteMethodsEnum("constant_score_auto");
        public static readonly RewriteMethodsEnum ScoringBoolean = new RewriteMethodsEnum("scoring_boolean");
        public static readonly RewriteMethodsEnum ConstantScoreBoolean = new RewriteMethodsEnum("constant_score_boolean");
        public static readonly RewriteMethodsEnum ConstantScoreFilter = new RewriteMethodsEnum("constant_score_filter");
        public static readonly RewriteMethodsEnum TopTermsN = new RewriteMethodsEnum("top_terms_N");
        public static readonly RewriteMethodsEnum TopTermsBoostN = new RewriteMethodsEnum("top_terms_boost_N");

        private RewriteMethodsEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
