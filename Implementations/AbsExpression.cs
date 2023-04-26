using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class AbsExpression : UnaryExpression
    {
        public AbsExpression(IExpression operand)
            : base(ExpressionType.Numeric, operand)
        {
            EnsureTypeSupported(operand, ExpressionType.Numeric, $"Abs Expression cannot be applied to operand to {operand.ResultType} result type");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Abs({Operand})";
        }
    }
}