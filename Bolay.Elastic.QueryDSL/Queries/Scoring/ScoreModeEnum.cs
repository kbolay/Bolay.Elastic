using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    public sealed class ScoreModeEnum : TypeSafeEnumBase<ScoreModeEnum>
    {
        /// <summary>
        /// Scores are multiplied.
        /// </summary>
        public static readonly ScoreModeEnum Multiply = new ScoreModeEnum("multiply");

        /// <summary>
        /// Scores are added together.
        /// </summary>
        public static readonly ScoreModeEnum Sum = new ScoreModeEnum("sum");

        /// <summary>
        /// Scores are averaged.
        /// </summary>
        public static readonly ScoreModeEnum Average = new ScoreModeEnum("avg");

        /// <summary>
        /// The first function that has a matching filter is applied.
        /// </summary>
        public static readonly ScoreModeEnum First = new ScoreModeEnum("first");

        /// <summary>
        /// The maximum score from the functions is used.
        /// </summary>
        public static readonly ScoreModeEnum Maximum = new ScoreModeEnum("max");

        /// <summary>
        /// The minimum score from the functions is used.
        /// </summary>
        public static readonly ScoreModeEnum Minimum = new ScoreModeEnum("min");

        private ScoreModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
