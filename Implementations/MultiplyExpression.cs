using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class MultiplyExpression : ArithmeticExpression
    {
        public MultiplyExpression(IExpression leftOperand, IExpression rightOperand)
            : base(leftOperand, rightOperand)
        {        
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string GetExpressionName() => "*";
        
    }
}