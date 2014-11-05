using Bolay.Elastic.Linq.Interfaces;
using Bolay.Elastic.Linq.RequestBuilder;
using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.Visitors
{
    public class WhereVisitor : ExpressionVisitor
    {
        internal const string _ANY = "Any";

        private string _PropertyPath { get; set; }
        private MethodCallExpression _Method { get; set; }
        private IPropertyMappingResolver _PropertyMappingResolver { get; set; }
        private DocumentPropertyBase _CurrentPropertyMapping { get; set; }
        
        //private string GetFullyQualifiedPropertyMethodName(Expression exp)
        //{
        //    if (_Method != null && _Method.Method.Name == "Any")
        //    {
        //        var name = _Method.Arguments[0].ToString();
        //        string[] split = name.ToString().Split(new char[] { '.' });
        //        string newString = string.Empty;
        //        for (int i = 1; i < split.Length; i++)
        //            newString += split[i] + ".";
        //        newString = newString.TrimEnd(new char[] { '.' });

        //        return GetName(newString + "." + exp.ToString().Split(new char[] { '.' })[1]);
        //    }
        //    else if (string.IsNullOrEmpty(this.methodName))
        //    {
        //        return GetFullyQualifiedPropertyConstantName(exp);
        //        //return GetName(exp.ToString());
        //    }
        //    else
        //    {
        //        string[] split = this.methodName.ToString().Split(new char[] { '.' });
        //        string newString = string.Empty;
        //        for (int i = 1; i < split.Length; i++)
        //            newString += split[i] + ".";
        //        newString = newString.TrimEnd(new char[] { '.' });

        //        newString = newString.Insert(0, _PropertyPath + ".");

        //        return GetName(newString + "." + exp.ToString().Split(new char[] { '.' })[1]);
        //    }
        //}
        //private string GetFullyQualifiedPropertyConstantName(Expression exp)
        //{
        //    string[] split = exp.ToString().Split(new char[] { '.' });
        //    string newString = string.Empty;
        //    for (int i = 1; i < split.Length; i++)
        //        newString += split[i] + ".";
        //    newString = newString.TrimEnd(new char[] { '.' });

        //    return GetName(newString);
        //}
        //private object GetMemberAccessValue(System.Linq.Expressions.MemberExpression exp)
        //{
        //    if (exp.Expression is System.Linq.Expressions.ConstantExpression)
        //        return (exp.Member as System.Reflection.FieldInfo).GetValue((exp.Expression as System.Linq.Expressions.ConstantExpression).Value);
        //    throw new MemberExpressionValueException("Unable to complete search.", "Unable to determine value for member expression object.");
        //}

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            // add the root parameter to the list.  The root lambda always gets evaluated first.
            if (string.IsNullOrEmpty(_PropertyPath))
                _PropertyPath = node.Parameters[0].Type.Name;


            return base.VisitLambda<T>(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            _Method = m;

            if (m.Method.Name == "Any")
            {
                Visit(m.Arguments[1]);
            }
            else if (m.Method.Name == "StartsWith")
            {
                //methodName = m.Method.Name;
                _CurrentPropertyMapping = m.Object.GetMappingProperty(_PropertyMappingResolver);
                Visit(m.Arguments[0]);
            }
            else if (m.Method.Name == "Contains")
            {
                //methodName = m.Method.Name;
                _CurrentPropertyMapping = m.Object.GetMappingProperty(_PropertyMappingResolver);
                Visit(m.Arguments[0]);
            }
            else if (m.Method.Name == "EndsWith")
            {
                //methodName = m.Method.Name;
                _CurrentPropertyMapping = m.Object.GetMappingProperty(_PropertyMappingResolver);
                Visit(m.Arguments[0]);
            }
            else
            {
                throw new Exception(string.Format("Unable to complete search. The method '{0}' is not supported", m.Method.Name));
                //throw new ExpressionMethodNotSupportedException("Unable to complete search.", string.Format("The method '{0}' is not supported", m.Method.Name));
            }
                

            _Method = null;
            return m;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            object rightHandleValue = null;
            if (node.Right is System.Linq.Expressions.MemberExpression)
            {
                rightHandleValue = (node.Right as System.Linq.Expressions.MemberExpression).GetMemberValue();
            }
            else if (node.Left is System.Linq.Expressions.MethodCallExpression)
            {
                //aparently Contains/StartsWith/ect. can get caught here, so in the expection we will hande it like normal
                MethodCallExpression methodCall = node.Left as MethodCallExpression;
                try
                {
                    //var supportedMethod = (methodCall.Object as ConstantExpression).Value as IElasticSupportedMethod;
                    //elasticQueryParts = supportedMethod.GenerateQueryParts(ExpressionTree, elasticQueryParts);

                    return node;
                }
                catch
                {
                    rightHandleValue = node.Right.ToString();
                }
            }
            else
            {
                rightHandleValue = node.Right.ToString();
            }

            DocumentPropertyBase propertyMapping = null;
            string queryValue = rightHandleValue.ToString().Trim(new char[] { '\"', '\'' }).ToLower();
            SearchType searchType = null;

            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:

                    //if (ExpressionTree == null)
                    //    ExpressionTree = new ExpressionTreeMap(GroupOperand.AND, NodeDirection.Left);
                    //else
                    //    ExpressionTree.Push(new ExpressionTreeMap(GroupOperand.AND, NodeDirection.Left));

                    //Visit(node.Left);

                    //if (ExpressionTree.Child != null)
                    //{
                    //    ExpressionTree.Pop();
                    //    ExpressionTree.Push(new ExpressionTreeMap(GroupOperand.AND, NodeDirection.Right));
                    //}
                    //else
                    //{
                    //    ExpressionTree = new ExpressionTreeMap(GroupOperand.AND, NodeDirection.Right);
                    //}

                    //Visit(node.Right);

                    //ExpressionTree.Pop();

                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:

                    //if (ExpressionTree == null)
                    //    ExpressionTree = new ExpressionTreeMap(GroupOperand.OR, NodeDirection.Left);
                    //else
                    //    ExpressionTree.Push(new ExpressionTreeMap(GroupOperand.OR, NodeDirection.Left));

                    //Visit(node.Left);

                    //if (ExpressionTree.Child != null)
                    //{
                    //    ExpressionTree.Pop();
                    //    ExpressionTree.Push(new ExpressionTreeMap(GroupOperand.OR, NodeDirection.Right));
                    //}
                    //else
                    //{
                    //    ExpressionTree = new ExpressionTreeMap(GroupOperand.OR, NodeDirection.Right);
                    //}


                    //Visit(node.Right);

                    //ExpressionTree.Pop();

                    break;
                case ExpressionType.Equal:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.FullText;
                    break;
                case ExpressionType.NotEqual:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.NotEqual;
                    break;
                case ExpressionType.LessThan:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.LessThan;
                    break;
                case ExpressionType.LessThanOrEqual:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.LessThanOrEqual;
                    break;
                case ExpressionType.GreaterThan:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.GreaterThan;
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    propertyMapping = node.Left.GetMappingProperty(_Method, _PropertyMappingResolver, _PropertyPath);
                    searchType = SearchType.GreaterThanOrEqual;
                    break;
                default:
                    throw new Exception("Unable to complete search. Unable to determine operation being requested.");
                    break;
            }

            //if (propertyMapping == null) // && !string.IsNullOrWhiteSpace(queryValue)) it's okay to compare to empty string GAPI 2091
            //    elasticQueryParts.AddToBinaryExpressionTree(ExpressionTree, propertyMapping, queryValue, searchType);

            return node;
        }
        protected override Expression VisitBlock(BlockExpression node)
        {
            return base.VisitBlock(node);
        }
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            return base.VisitCatchBlock(node);
        }
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            //SearchType? searchType = null;
            string searchValue = node.ToString().Trim(new char[] { '\"', '\'' }).ToLower();
            bool isFullText = (searchValue.Contains(" ") || searchValue.Contains("-")) ? true : false;

            // TODO: Currently we are making assumptions based on our current elastic index.  This should be driven more dynamically in the future.
            //if (methodName == "StartsWith")
            //{
            //    if (isFullText)
            //        searchType = SearchType.FullTextStartsWith;
            //    else
            //    {
            //        if (searchValue.EndsWith(QueryParser.QueryWildcard) && !searchValue.EndsWith(QueryParser.EscapedWildcard))
            //            searchType = SearchType.TokenStartsWith;
            //        else
            //            searchType = SearchType.TokenMatch;
            //    }
            //}
            //else if (methodName == "EndsWith")
            //{
            //    if (isFullText)
            //        searchType = SearchType.FullTextEndsWith;
            //    else
            //        searchType = SearchType.TokenEndsWith;
            //}
            //else if (methodName == "Contains")
            //{
            //    bool isInnerWildcard = false;
            //    if (!searchValue.StartsWith(QueryParser.QueryWildcard) && QueryParser.ContainsWildcard(searchValue))
            //        isInnerWildcard = true;

            //    if (isFullText && isInnerWildcard)
            //        searchType = SearchType.FullTextWildcard;
            //    else if (isFullText)
            //        searchType = SearchType.FullTextContains;
            //    else if (isInnerWildcard)
            //        searchType = SearchType.TokenWildcard;
            //    else
            //        searchType = SearchType.TokenContains;
            //}
            //else
            //    throw new ExpressionMethodNotSupportedException("Unable to complete search.", methodName + "not supported.");

            //elasticQueryParts.AddToBinaryExpressionTree(ExpressionTree, _CurrentPropertyMapping, searchValue, searchType);

            //this.methodName = string.Empty;
            return base.VisitConstant(node);
        }
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            return base.VisitDebugInfo(node);
        }
        protected override Expression VisitDefault(DefaultExpression node)
        {
            return base.VisitDefault(node);
        }
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            return base.VisitDynamic(node);
        }
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            return base.VisitElementInit(node);
        }
        protected override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }
        protected override Expression VisitGoto(GotoExpression node)
        {
            return base.VisitGoto(node);
        }
        protected override Expression VisitIndex(IndexExpression node)
        {
            return base.VisitIndex(node);
        }
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            return base.VisitInvocation(node);
        }
        protected override Expression VisitLabel(LabelExpression node)
        {
            return base.VisitLabel(node);
        }
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            return base.VisitLabelTarget(node);
        }
        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }
        protected override Expression VisitLoop(LoopExpression node)
        {
            return base.VisitLoop(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            return base.VisitMember(node);
        }
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return base.VisitMemberAssignment(node);
        }
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return base.VisitMemberBinding(node);
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return base.VisitMemberListBinding(node);
        }
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }
        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            return base.VisitSwitch(node);
        }
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return base.VisitRuntimeVariables(node);
        }
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            return base.VisitSwitchCase(node);
        }
        protected override Expression VisitTry(TryExpression node)
        {
            return base.VisitTry(node);
        }
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            return base.VisitTypeBinary(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }
    }
}
