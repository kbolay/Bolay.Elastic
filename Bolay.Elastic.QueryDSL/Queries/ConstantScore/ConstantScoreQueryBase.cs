using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.ConstantScore
{
    [JsonConverter(typeof(ConstantScoreSerializer))]
    public class ConstantScoreQueryBase : QueryBase
    {
        public ISearchPiece SearchPiece { get; private set; } 
        public Double Boost { get; set; }        

        protected ConstantScoreQueryBase(ISearchPiece searchPiece)
        {
            if (!(searchPiece is IFilter || searchPiece is IQuery))
                throw new ArgumentException("ConstantScoreQuery requires a filter or query.", "searchPiece");

            SearchPiece = searchPiece;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }
    }
}
