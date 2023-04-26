namespace Expressions
{
    public interface IExpression
    {
        ExpressionType ResultType {get;}
        void Accept(IVisitor visitor);
    }
}