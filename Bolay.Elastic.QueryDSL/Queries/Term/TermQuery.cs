using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Term
{
    // TODO: consider breaking this into NumberTerm, DoubleTerm, DateTimeTerm, etc...

    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-term-query.html
    /// </summary>
    [JsonConverter(typeof(TermSerializer))]
    public class TermQuery : QueryBase 
    {
        public string Field { get; private set; }
        public object Value { get; private set; }

        public Double Boost { get; set; }

        public TermQuery(string field, object value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "Term query requires a field.");
            if (value == null)
                throw new ArgumentNullException("value", "Term query requires a value.");

            Field = field;
            Value = value;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
