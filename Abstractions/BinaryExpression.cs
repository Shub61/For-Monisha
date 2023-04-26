using Expressions.Helpers;

namespace Expressions.Abstractions
{
    public abstract class BinaryExpression : Expression, IBinaryExpression
    {
        protected BinaryExpression(ExpressionType type, IExpression leftOperand, IExpression rightOperand)
        : base(type)
        {
            LeftOperand = leftOperand ?? throw new ArgumentNullException(nameof(leftOperand));
            RightOperand = rightOperand ?? throw new ArgumentNullException(nameof(rightOperand));
        }
        
        public IExpression LeftOperand {get;}
        public IExpression RightOperand {get;}

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj is BinaryExpression expression && ReferenceEquals(GetType(), expression.GetType())
                   && base.Equals(obj) 
                   && EqualityComparer<IExpression>.Default.Equals(LeftOperand, expression.LeftOperand) 
                   && EqualityComparer<IExpression>.Default.Equals(RightOperand, expression.RightOperand);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode() * ObjectHelper.HashCodeSeed  + LeftOperand.GetHashCode();
                return hash * ObjectHelper.HashCodeSeed + RightOperand.GetHashCode();
            }            
        }

        public override string? ToString()
        {
            return $"{GetExpressionName()} ({LeftOperand}, {RightOperand})";
        }

        protected abstract string GetExpressionName();
    }
}