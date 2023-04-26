namespace Expressions.Abstractions
{
    public interface IConstantExpression : IExpression
    {
        object Value {get;}
    }
}