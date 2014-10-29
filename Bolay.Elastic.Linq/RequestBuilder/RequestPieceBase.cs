using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Mapping.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public abstract class RequestPieceBase : IRequestPiece
    {
        public DocumentPropertyBase MappingProperty { get; set; }
        public abstract string BuildPiece();
    }
}
