using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MatchAll
{
    [JsonConverter(typeof(MatchAllSerializer))]
    public class MatchAllQuery : QueryBase
    {
        public Double Boost { get; set; }

        public MatchAllQuery()
        {
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
