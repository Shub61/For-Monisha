using System.Collections.Concurrent;
using Expressions.AliasExpressions.Abstractions;
using MsExpressions = System.Linq.Expressions;

namespace Expressions.AliasExpressions.Implementations
{
    public class SimpleValueProvider<TK, TV> : IValueProvider<TK, TV>
    {
        private readonly IDictionary<TK, TV> _data = new ConcurrentDictionary<TK, TV>();

        public TV Get(TK key)
        {
            if(_data.TryGetValue(key, out var value))
            {
                return value;
            }
            throw new ArgumentException($"Key not found: {key}");
        }

        public void Add(TK key, TV value)
        {
            _data[key] = value;
        }
    }
}