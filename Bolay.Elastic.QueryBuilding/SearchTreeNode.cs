using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryBuilding
{
    public class SearchTreeNode
    {
        public ExpressionType Operand { get; private set; }
        public SearchTreeNode LeftNode { get; set; }
        public SearchTreeNode RightNode { get; set; }
        public SearchPiece LeftPiece { get; set; }
        public SearchPiece RightPiece { get; set; }
        public SearchTreeNode Parent { get; private set; }

        public SearchTreeNode(ExpressionType operand = ExpressionType.And) 
        { 
            Operand = operand;
        }
        public SearchTreeNode(SearchTreeNode parent, ExpressionType operand)
            : this(operand)
        {
            Parent = parent;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
