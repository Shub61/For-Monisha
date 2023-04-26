namespace Expressions.Abstractions
{
    public interface IMultipleExpression
    {
        IReadOnlyCollection<IExpression> Operands {get;}
    }
}