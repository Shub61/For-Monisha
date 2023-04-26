using Expressions.Implementations;
using System.Collections.Concurrent;
using System.Reflection;

namespace Expressions.Visitors.Implementations
{
    public class NameResolver<T> : NameResolverBase
    {
        private const int False = 0;
        private const int True = 1;

        private readonly ConcurrentDictionary<string, MemberInfo> _rules = new ConcurrentDictionary<string, MemberInfo>();
        private int _isFrozen = False;

        public NameResolver(): base(typeof(T)){}

        public NameResolver<T> Map<TV> (string name, Expressions<Func<T, TV>> expression)
        {
            if(IsFrozen)
            {
                throw new InvalidOperationException($"Name resolver of type '{Type}' cannot be modified after it has been used to resolve data");                
            }
            var memberInfo = (expression.Body as MemberExpression)?.Member;
            if(!(memberInfo is PropertyInfo || memberInfo is FieldInfo))
            {
                throw new InvalidOperationException("The lmbda expression should point to a valid property or field");                
            }
            _rules.TryAdd(name, memberInfo);
            return this;
        }

        public bool IsFrozen => Interlocked.CompareExchange(ref _isFrozen, True, True) == True;

        public void Freeze()
        {
            Interlocked.Exchange(ref _isFrozen, True);
        }

        protected override MemberInfo DoGet(string name)
        {
            Freeze();
            if(_rules.TryGetValue(name, out var result))
            {
                return result;
            }

            return base.DoGet(name);
        }
    }
}