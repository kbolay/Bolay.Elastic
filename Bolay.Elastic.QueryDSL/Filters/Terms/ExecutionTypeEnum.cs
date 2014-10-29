using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Terms
{
    public sealed class ExecutionTypeEnum : TypeSafeEnumBase<ExecutionTypeEnum>
    {
        public bool CacheDefault { get; private set; }

        /// <summary>
        /// Builds a bitset for each term, and is cached.
        /// </summary>
        public static readonly ExecutionTypeEnum Plain = new ExecutionTypeEnum("plain", true);

        /// <summary>
        /// Uses fielddata cache to compare items.
        /// </summary>
        public static readonly ExecutionTypeEnum FieldData = new ExecutionTypeEnum("fielddata", false);

        /// <summary>
        /// Generates a cached term filter for each term and wraps those in a bool filter. 
        /// </summary>
        public static readonly ExecutionTypeEnum Bool = new ExecutionTypeEnum("bool", true);

        /// <summary>
        /// Generates a non cached term filter for each term and wraps those in a bool filter. 
        /// </summary>
        public static readonly ExecutionTypeEnum BoolNoCache = new ExecutionTypeEnum("bool_nocache", false);

        /// <summary>
        /// Generates a cached term filter for each term and wraps them in an and filter.
        /// </summary>
        public static readonly ExecutionTypeEnum And = new ExecutionTypeEnum("and", true);

        /// <summary>
        /// Generates a non cached term filter for each term and wraps them in an and filter.
        /// </summary>
        public static readonly ExecutionTypeEnum AndNoCache = new ExecutionTypeEnum("and_nocache", false);

        /// <summary>
        /// Generates a cached term filter for each term and wraps them in an or filter.
        /// </summary>
        public static readonly ExecutionTypeEnum Or = new ExecutionTypeEnum("or", true);

        /// <summary>
        /// Generates a cached term filter for each term and wraps them in an or filter.
        /// </summary>
        public static readonly ExecutionTypeEnum OrNoCache = new ExecutionTypeEnum("or_nocache", false);

        private ExecutionTypeEnum(string value, bool cacheDefault)
            : base(value)
        {
            CacheDefault = cacheDefault;
            _AllItems.Add(this);
        }
    }
}
