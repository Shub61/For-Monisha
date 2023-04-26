namespace Expressions
{
    public interface IResultProvider<T>
    {        
        void Reset();
        T Get();
    }
}