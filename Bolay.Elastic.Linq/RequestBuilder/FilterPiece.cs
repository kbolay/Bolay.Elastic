using Bolay.Elastic.QueryDSL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public class FilterPiece : RequestPieceBase
    {
        private IFilter _Filter { get; set; }
        public override string BuildPiece()
        {
            throw new NotImplementedException();
        }
    }
}
