namespace Expressions
{
    public interface INameResolverContainer
    {        
        INameResolver Get (Type type);
    }
}