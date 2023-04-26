using Expressions.Visitors.Implementations;

namespace Expressions.Evaluators
{
    internal class Evaluator<T> : IEvaluator, IEvaluator<T> where T: class
    {
        private readonly Func<T, bool> _function;

        public Evaluator(Type type, IExpression expression)
            : this(type, expression, null);

        public Evaluator(Type type, IExpression expression, INameResolverContainer nameResolverContainer)
        {
            Type = type ?? throw new ArgumentNullException(nameof(Type));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            var visitor = new PredicateVisitor<T>(nameResolverContainer);
            expression.Accept(visitor);
            _function = visitor.Get();
        }

        public Func<T, bool> Function => _function;

        public Type Type {get;}

        public IExpression Expression {get;}

        bool IEvaluator.Evaluate(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(!(obj is T))
            {
                throw new ArgumentException($"Cannot evaluate the expression. Expecting parameter type: {typeof(T)} while received: {obj.GetType()}");
            }
            return Evaluate((T) obj);
        }

        public bool Evaluate(T obj)
        {
            if(obj == null)
            {
                return false;
            }            
            return _function(obj);
        }


        public override bool Equals(object obj)
        {
            return obj is Evaluator<T> other && ReferenceEquals(Type, other.Type)
                && Expression.Equals(other.Expression);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Type.GetHashCode() + Expression.GetHashCode();
            }        
        }
    }
}