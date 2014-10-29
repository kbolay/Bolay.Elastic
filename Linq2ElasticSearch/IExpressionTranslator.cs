using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq2ElasticSearch
{
    public interface IExpressionTranslator
    {
        string Translate(Expression expression);
    }
}
