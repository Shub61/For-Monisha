namespace Expressions.AliasExpressions.Evaluators
{
    
    public interface IEvaluatorFactory<TK, TV> : IEvaluator
    {
        IEvaluator GetEvaluator(Type keyType, string expression);
        IEvaluator GetEvaluator(Type keyType, IExpression expression);
    }
}