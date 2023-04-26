using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class ModuloExpression : ArithmeticExpression
    {
        public ModuloExpression(IExpression leftOperand, IExpression rightOperand)
            : base(leftOperand, rightOperand)
        {        
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string GetExpressionName() => "%";        
    }
}