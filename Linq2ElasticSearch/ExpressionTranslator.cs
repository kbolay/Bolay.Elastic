using Bolay.Elastic.Mapping.Types;
using Bolay.Elastic.QueryBuilding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch
{
    public class ExpressionTranslator : ExpressionVisitor, IExpressionTranslator
    {
        // any "in-between" objects that need to persist between expression and string form query need to be here
        private SearchTreeNode _CurrentExpressionTree { get; set; }
        private bool _IsCurrentDirectionRight { get; set; }
        private readonly Dictionary<string, DocumentPropertyBase> _DocumentAnalysisDictionary;

        public ExpressionTranslator(Dictionary<string, DocumentPropertyBase> documentAnalysisDictionary)
        {
            _DocumentAnalysisDictionary = documentAnalysisDictionary;
        }

        public string Translate(Expression expression)
        {
            string result = null;
            // initialize any "in-between" objects

            // populate "in-betweeen" objects by analyzing the expression
            this.Visit(expression);

            // execute on "in-between" objects to generate the string
            if (_CurrentExpressionTree != null)
            {
                result = _CurrentExpressionTree.ToString();
            }

            return result;
        }

        private void AddSearchTreeNode(ExpressionType expressionType)
        {
            if (_CurrentExpressionTree != null)
            {
                if (!_IsCurrentDirectionRight)
                {
                    _CurrentExpressionTree.LeftNode = new SearchTreeNode(_CurrentExpressionTree, expressionType);
                    _CurrentExpressionTree = _CurrentExpressionTree.LeftNode;
                }
                else
                {
                    _CurrentExpressionTree.RightNode = new SearchTreeNode(_CurrentExpressionTree, expressionType);
                    _CurrentExpressionTree = _CurrentExpressionTree.RightNode;
                }
            }
            else
            {
                _CurrentExpressionTree = new SearchTreeNode(expressionType);
            }            
        }

        private void AddSearchPiece(SearchPiece piece)
        {
            if (!_IsCurrentDirectionRight)
            {
                _CurrentExpressionTree.LeftPiece = piece;
            }
            else
            {
                _CurrentExpressionTree.RightPiece = piece;
            }
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                this.Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
                return m;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            throw new NotImplementedException();
            //switch (u.NodeType)
            //{
            //    case ExpressionType.Not:
            //        sb.Append(" NOT ");
            //        this.Visit(u.Operand);
            //        break;
            //    default:
            //        throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            //}
            //return u;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {


            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    // create a new search tree node and make it the current one
                    AddSearchTreeNode(node.NodeType);

                    // visit left side of expression
                    _IsCurrentDirectionRight = false;
                    Visit(node.Left);

                    // visit right side of expression
                    _IsCurrentDirectionRight = true;
                    Visit(node.Right);

                    // make sure to go back up to the top level of the expression tree
                    if(_CurrentExpressionTree.Parent != null)
                    {
                        _CurrentExpressionTree = _CurrentExpressionTree.Parent;
                    }                    
                    break;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    //AddSearchPiece()
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", node.NodeType));
            }

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
                // assume constant nodes w/ IQueryables are table references
                //sb.Append("SELECT * FROM ");
                //sb.Append(q.ElementType.Name);
            }
            else if (c.Value == null)
            {
                //sb.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        //sb.Append(((bool)c.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        //sb.Append("'");
                        //sb.Append(c.Value);
                        //sb.Append("'");
                        break;
                    case TypeCode.Object:
                        //throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
                    default:
                        //sb.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                //sb.Append(m.Member.Name);
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
    }
}
