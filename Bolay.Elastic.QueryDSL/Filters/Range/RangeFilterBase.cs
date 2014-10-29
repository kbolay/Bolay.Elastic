using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Range
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-range-filter.html
    /// </summary>
    [JsonConverter(typeof(RangeSerializer))]
    public abstract class RangeFilterBase : FilterBase
    {
        private ExecutionTypeEnum _ExecutionType { get; set; }

        public abstract object GreaterThan { get; }
        public abstract object LessThan { get; }
        public abstract object GreaterThanOrEqualTo { get; }
        public abstract object LessThanOrEqualTo { get; }

        /// <summary>
        /// Gets the field to filter against.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets or sets the execution type for the range filter. If the execution type is set to index, cache is defaulted to true.
        /// </summary>
        public ExecutionTypeEnum ExecutionType 
        {
            get { return _ExecutionType; }
            set 
            {
                if (value != null && value == ExecutionTypeEnum.Index)
                {
                    Cache = RangeSerializer._INDEX_EXECUTION_CACHE_DEFAULT;
                }

                _ExecutionType = value;
            } 
        }

        /// <summary>
        /// Create a range filter base object.
        /// </summary>
        /// <param name="field">The field to search against.</param>
        public RangeFilterBase(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RangeFilter requires a field.");

            Field = field;
            Cache = RangeSerializer._CACHE_DEFAULT;
        }
    }
}
