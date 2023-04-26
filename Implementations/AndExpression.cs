using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class AndExpression : MultipleExpression
    {
        public AndExpression(params IExpression [] operands)
            : this((IReadOnlyCollection<IExpression>) operands)
        {        
        }

        public AndExpression(IReadOnlyCollection<IExpression> operands)
            : base(ExpressionType.Bool, operands)
        {       
            foreach(var operand in operands)
            {
                EnsureTypeSupported(operand, ExpressionType.Bool, $"All operands in And Expression must support {ExpressionType.Bool} type");
            } 
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string GetExpressionName() => "&&";         
    }
}