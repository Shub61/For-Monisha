using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class NegativeSignExpression : UnaryExpression
    {
        public NegativeSignExpression(IExpression operand)
            :base(ExpressionType.Numeric, operand)
        {
            EnsureTypeSupported(operand, ExpressionType.Numeric, $"NegativeSignExpression operand should support {ExpressionType.Numeric } type. Type supplied: {operand.ResultType}");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"-({Operand})";
        }
    }
}