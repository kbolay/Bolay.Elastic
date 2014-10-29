using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2ElasticSearch
{
    public interface IQueryProviderAdapter : IQueryProvider
    {
        string GetQueryText(Expression expression);
        object ExecuteQuery(Expression expression);
    }
}
