using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    public sealed class BoostModeEnum : TypeSafeEnumBase<BoostModeEnum>
    {
        /// <summary>
        /// Query score and function score is multiplied.
        /// </summary>
        public static readonly BoostModeEnum Multiply = new BoostModeEnum("multiply");

        /// <summary>
        /// Only function score is used, query score is ignored.
        /// </summary>
        public static readonly BoostModeEnum Replace = new BoostModeEnum("replace");

        /// <summary>
        /// The query score and function score are added together.
        /// </summary>
        public static readonly BoostModeEnum Sum = new BoostModeEnum("sum");

        /// <summary>
        /// The query score and function score are averaged.
        /// </summary>
        public static readonly BoostModeEnum Average = new BoostModeEnum("average");

        /// <summary>
        /// The larger value between query score and function score are used.
        /// </summary>
        public static readonly BoostModeEnum Maximum = new BoostModeEnum("maximum");

        /// <summary>
        /// The smaller value between the query score and function score are used.
        /// </summary>
        public static readonly BoostModeEnum Minimum = new BoostModeEnum("minimum");

        private BoostModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
