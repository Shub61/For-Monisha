namespace Expressions.AliasExpressions.Evaluators
{

    public interface IEvaluator {}
    public interface IEvaluator<TK, TV> : IEvaluator
    {
        TV Evaluate(IValueProvider<TK, TV> provider);
    }
}