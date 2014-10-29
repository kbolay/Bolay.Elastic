using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public class RequestParts
    {
        public List<FacetPiece> FacetPieces { get; set; }
        public List<SortPiece> SortPieces { get; set; }
        public List<SelectPiece> SelectPieces { get; set; }
        public List<SelectPiece> ScriptFields { get; set; }
        public QuerySectionTree QueryPieces { get; set; }
        public FilterSectionTree FilterPieces { get; set; }
    }
}
