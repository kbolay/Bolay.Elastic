using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public interface IRequestPiece
    {
        DocumentPropertyBase MappingProperty { get; }
        string BuildPiece();
    }
}
