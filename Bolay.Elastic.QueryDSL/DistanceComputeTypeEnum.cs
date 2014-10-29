using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    /// <summary>
    /// The method of calculating the distance between the two points.
    /// </summary>
    public sealed class DistanceComputeTypeEnum : TypeSafeEnumBase<DistanceComputeTypeEnum>
    {
        /// <summary>
        /// The best precision.
        /// </summary>
        public static readonly DistanceComputeTypeEnum Arc = new DistanceComputeTypeEnum("arc");

        /// <summary>
        /// Faster than arc but less precise.
        /// </summary>
        public static readonly DistanceComputeTypeEnum SloppyArc = new DistanceComputeTypeEnum("sloppy_arc");

        /// <summary>
        /// Fastest and least accurate.
        /// </summary>
        public static readonly DistanceComputeTypeEnum Plane = new DistanceComputeTypeEnum("plane");

        private DistanceComputeTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
