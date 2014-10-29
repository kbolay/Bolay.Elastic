using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Fuzzy
{
    [JsonConverter(typeof(FuzzySerializer))]
    public abstract class FuzzyQueryBase : QueryBase
    {
        public string Field { get; set; }
        public Double Boost { get; set; }
        public int PrefixLength { get; set; }
        public int MaximumExpansions { get; set; }

        public abstract object Value { get; }
        public abstract object Fuzziness { get; }

        internal FuzzyQueryBase()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
            PrefixLength = FuzzySerializer._PREFIX_LENGTH_DEFAULT;
            MaximumExpansions = FuzzySerializer._MAXIMUM_EXPANSIONS_DEFAULT;
        }

        public FuzzyQueryBase(string field) : this()
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "FuzzyQueryBase requires field.");            

            Field = field;            
        }        
    }
}
