namespace Expressions.Abstractions
{
    public abstract class ArithmeticExpression : BinaryExpression
    {
        protected ArithmeticExpression(IExpression leftOperand, IExpression rightOperand)
        : base(ExpressionType.Numeric, leftOperand, rightOperand)
        {
            EnsureTypeSupported(leftOperand, ExpressionType.Numeric, $"ArithmeticExpression left operand should support {ExpressionType.Numeric} type. Type supplied: {leftOperand.ResultType}");
            EnsureTypeSupported(rightOperand, ExpressionType.Numeric, $"ArithmeticExpression right operand should support {ExpressionType.Numeric} type. Type supplied: {rightOperand.ResultType}");
        }

        public override string? ToString()
        {
            return $"({LeftOperand} {GetExpressionName()} {RightOperand})";
        }        
    }
}