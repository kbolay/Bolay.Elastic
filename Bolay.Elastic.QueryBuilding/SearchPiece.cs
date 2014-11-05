using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryBuilding
{
    public class SearchPiece
    {
        public DocumentPropertyBase Property { get; private set; }
        public object SearchValue { get; set; }
        public ExpressionType Operator { get; private set; }

        public SearchPiece(DocumentPropertyBase property, object searchValue, ExpressionType expressionOperator)
        {
            Property = property;
            SearchValue = searchValue;
            Operator = expressionOperator;
        }
    }
}
