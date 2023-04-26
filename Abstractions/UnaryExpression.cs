using Expressions.Helpers;

namespace Expressions.Abstractions
{
    public abstract class UnaryExpression : Expression, IUnaryExpression
    {
        protected UnaryExpression(ExpressionType type, IExpression operand)
        : base(type)
        {
            Operand = operand ?? throw new ArgumentNullException(nameof(operand));            
        }
        
        public IExpression Operand {get;}        

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj is UnaryExpression expression && ReferenceEquals(GetType(), expression.GetType())
                   && base.Equals(obj)                    
                   && Equals(Operand, expression.Operand);
        }

        public override int GetHashCode()
        {
            unchecked
            {                
                return base.GetHashCode() * ObjectHelper.HashCodeSeed + Operand.GetHashCode();
            }            
        }
    }
}