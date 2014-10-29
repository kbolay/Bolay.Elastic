using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    public sealed class ScoreFunctionEnum : TypeSafeEnumBase<ScoreFunctionEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly ScoreFunctionEnum Script = new ScoreFunctionEnum("script_score", typeof(ScriptScoreFunction));
        public static readonly ScoreFunctionEnum BoostFactor = new ScoreFunctionEnum("boost_factor", typeof(BoostFactorFunction));
        public static readonly ScoreFunctionEnum Random = new ScoreFunctionEnum("random_score", typeof(RandomScoreFunction));
        public static readonly ScoreFunctionEnum NormalDecay = new ScoreFunctionEnum("gauss", typeof(DecayFunction));
        public static readonly ScoreFunctionEnum ExponentialDecay = new ScoreFunctionEnum("exp", typeof(DecayFunction));
        public static readonly ScoreFunctionEnum LinearDecay = new ScoreFunctionEnum("linear", typeof(DecayFunction));

        private ScoreFunctionEnum(string value, Type implementationType) : base(value)
        {
            ImplementationType = implementationType;

            _AllItems.Add(this);
        }
    }
}
