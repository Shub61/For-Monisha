using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class PositiveSignExpression : UnaryExpression
    {
        public PositiveSignExpression(IExpression operand)
            :base(ExpressionType.Numeric, operand)
        {
            EnsureTypeSupported(operand, ExpressionType.Numeric, $"PositiveSignExpression operand should support {Expression.Numeric } type. Type supplied: {operand.ResultType}");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"+({Operand})";
        }
    }
}