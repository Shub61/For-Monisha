using System.Reflection;

namespace Expressions
{
    public interface INameResolver
    {
        Type Type {get;}
        MemberInfo Get (string name);
    }
}