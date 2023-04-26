using Expressions.Abstractions;

namespace Expressions.Implementations
{
    public class OrExpression : MultipleExpression
    {
        public OrExpression(params IExpression [] operands)
            : this((IReadOnlyCollection<IExpression>) operands)
        {        
        }

        public OrExpression(IReadOnlyCollection<IExpression> operands)
            : base(ExpressionType.Bool, operands)
        {       
            foreach(var operand in operands)
            {
                EnsureTypeSupported(operand, ExpressionType.Bool, $"All operands in Or Expression must support {ExpressionType.Bool} type");
            } 
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string GetExpressionName() => "||";         
    }
}