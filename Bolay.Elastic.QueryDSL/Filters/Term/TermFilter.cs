using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Term
{
    [JsonConverter(typeof(TermSerializer))]
    public class TermFilter : FilterBase
    {
        public string Field { get; private set; }
        public object Value { get; set; }

        public TermFilter(string field, object value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "Term filter requires a field.");
            if (value == null)
                throw new ArgumentNullException("value", "Term filter requires a value.");

            Field = field;
            Value = value;

            Cache = TermSerializer._CACHE_DEFAULT;
        }        
    }
}
