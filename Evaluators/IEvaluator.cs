namespace Expressions.Evaluators
{

    public interface IEvaluator 
    {
        bool Evaluate(object obj);
    }
    public interface IEvaluator<T>
    {
        Func<T, bool> Function {get;}
        bool Evaluate(T obj);
    }
}