using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Range
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-range-query.html
    /// </summary>
    [JsonConverter(typeof(RangeSerializer))]
    public abstract class RangeQueryBase : QueryBase
    {
        public abstract object GreaterThan { get; }
        public abstract object LessThan { get; }
        public abstract object GreaterThanOrEqualTo { get; }
        public abstract object LessThanOrEqualTo { get; }

        public string Field { get; private set; }
        public Double Boost { get; set; }

        internal RangeQueryBase()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
        }

        /// <summary>
        /// Create a range query base object.
        /// </summary>
        /// <param name="field">The field to search against.</param>
        public RangeQueryBase(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "RangeQuery requires a field.");

            Field = field;
        }
    }
}
