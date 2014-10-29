using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch.Evaluation
{
    /// <summary>
    /// Performs bottom-up analysis to determine which nodes can possibly
    /// be part of an evaluated sub-tree.
    /// http://blogs.msdn.com/b/mattwar/archive/2007/08/01/linq-building-an-iqueryable-provider-part-iii.aspx
    /// </summary>
    internal class Nominator : ExpressionVisitor
    {
        private Func<Expression, bool> _CanBeEvaluated { get; set; }
        private HashSet<Expression> _Candidates { get; set; }
        private bool _CannotBeEvaluated { get; set; }

        internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
        {
            _CanBeEvaluated = fnCanBeEvaluated;
        }

        internal HashSet<Expression> Nominate(Expression expression)
        {
            _Candidates = new HashSet<Expression>();
            Visit(expression);
            return _Candidates;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null)
            {
                bool saveCannotBeEvaluated = _CannotBeEvaluated;
                _CannotBeEvaluated = false;
                base.Visit(expression);
                if (!_CannotBeEvaluated)
                {
                    if (_CanBeEvaluated(expression))
                    {
                        _Candidates.Add(expression);
                    }
                    else
                    {
                        _CannotBeEvaluated = true;
                    }
                }
                _CannotBeEvaluated |= saveCannotBeEvaluated;
            }
            return expression;
        }
    }
}
