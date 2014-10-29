using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    /// <summary>
    /// Determines how the sorting algorithm will handle arrays of values.
    /// </summary>
    public sealed class SortModeEnum : TypeSafeEnumBase<SortModeEnum>
    {
        /// <summary>
        /// Use the minimum value in the array to order the documents.
        /// </summary>
        public static readonly SortModeEnum Minimum = new SortModeEnum("min");

        /// <summary>
        /// Use the maximum value in the array to order the documents.
        /// </summary>
        public static readonly SortModeEnum Maximum = new SortModeEnum("max");
        
        /// <summary>
        /// Use the sum of the values in the array to order the documents.
        /// </summary>
        public static readonly SortModeEnum Sum = new SortModeEnum("sum");

        /// <summary>
        /// Use the average of the values in the array to order the documents.
        /// </summary>
        public static readonly SortModeEnum Average = new SortModeEnum("avg");

        private SortModeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
