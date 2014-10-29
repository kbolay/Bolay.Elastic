using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Prefix
{
    [JsonConverter(typeof(PrefixSerializer))]
    public class PrefixQuery : QueryBase
    {
        /// <summary>
        /// The field to search in.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// The prefix value to search for in the field.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// The boost value for the query.
        /// Defaults to 1.0.
        /// </summary>
        public Double Boost { get; set; }

        internal PrefixQuery()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
        }

        public PrefixQuery(string field, string value) : this()
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "PrefixQuery requires a field to search against.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "PrefixQuery requires a value to search for in the field.");

            Field = field;
            Value = value;
        }
    }
}
