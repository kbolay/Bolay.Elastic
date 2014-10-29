using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public abstract class RequestSectionTree
    {
        public List<IRequestPiece> MustPieces { get; set; }
        public List<IRequestPiece> ShouldPieces { get; set; }
        public List<IRequestPiece> MustNotPieces { get; set; }

        public List<RequestSectionTree> MustChildren { get; set; }
        public List<RequestSectionTree> ShouldChildren { get; set; }
        public List<RequestSectionTree> MustNotChildren { get; set; }
    }
}