using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public interface ISearchPiece : IRequestPiece
    {
        PropertyInfo ObjectProperty { get; }
        SearchType SearchType { get; }
        object Value { get; }
    }
}
