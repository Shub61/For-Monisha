namespace Expressions.Evaluators
{
    
    public interface IEvaluatorFactory
    {
        void AddNameResolver(INameResolver nameResolver);
        IEvaluator GetEvaluator(Type type, string expression);
        IEvaluator GetEvaluator(Type type, IExpression expression);
    }
}