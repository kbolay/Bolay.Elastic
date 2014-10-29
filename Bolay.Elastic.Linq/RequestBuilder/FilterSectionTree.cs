using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public class FilterSectionTree : RequestSectionTree
    {
        public List<FilterPiece> NonBitwiseAndFilters { get; set; }
        public List<FilterPiece> NonBitwiseOrFilters { get; set; }
        public List<FilterPiece> NonBitwiseNotFilters { get; set; }
    }
}
