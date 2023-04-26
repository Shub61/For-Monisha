namespace Expressions.Abstractions
{
    public abstract class ComparisonExpression : BinaryExpression
    {
        protected ComparisonExpression(IExpression leftOperand, IExpression rightOperand)
        : base(ExpressionType.Bool, leftOperand, rightOperand)
        {
            
        }

        public override string? ToString()
        {
            return $"({LeftOperand} {GetExpressionName()} {RightOperand})";
        }        
    }
}