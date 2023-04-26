using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class LikeExpression : BinaryExpression
    {
        public LikeExpression(IExpression leftOperand, IExpression rightOperand)
            :base(ExpressionType.Bool, leftOperand, rightOperand)
        {   
            EnsureTypeSupported(rightOperand, ExpressionType.String, $"LikeExpression right operand should support {ExpressionType.String} type. Type supplied: {rightOperand.ResultType}");     
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "$({LeftOperand} {GetExpressionName()} {RightOperand}";
        }

        protected override string GetExpressionName() => "Like";        
    }


}