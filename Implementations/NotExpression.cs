using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class NotExpression : UnaryExpression
    {
        public NotExpression(IExpression operand)
            :base(ExpressionType.Bool, operand)
        {
            EnsureTypeSupported(operand, ExpressionType.Numeric, $"Not Expression cannot be applied to operand to {operand.ResultType} result type");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Not({Operand})";
        }
    }
}