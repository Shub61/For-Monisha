namespace Expressions.Abstractions
{
    public interface IBuilder<T> where T: class
    {
        IExpression ToExpression(T value, IExpression defaultValue = null);
        T FromExpression(IExpression expression, T defaultValue = null);
    }
}