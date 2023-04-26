using System.Collections;
using System.Collections.Concurrent;
using Expressions.AliasExpressions.Abstractions;

namespace Expressions.AliasExpressions.Implementations
{
    public class InstanceContainer<T, TK, TV> : IValueProvider<TK, TV>, IEnumerable<T>
    {
        private readonly IDictionary<TK, T> _data = new ConcurrentDictionary<TK, T>();
        private readonly Func<T, TK> _keyGetter;
        private readonly Func<T, TV> _valueGetter;

        public InstanceContainer(Func<T, TK> keyGetter, Func<T, TV> valueGetter)
        {
            _keyGetter = keyGetter ??  throw new ArgumentNullException(nameof(keyGetter));
            _valueGetter = valueGetter ??  throw new ArgumentNullException(nameof(valueGetter));
        }

        public TV Get(TK key)
        {
            if(_data.TryGetValue(key, out var instance))
            {
                var value = _valueGetter(instance);
                return value;
            }
            throw new ArgumentException($"Key not found: {key}");
        }

        public void Add(T instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            _data[_keyGetter(instance)] = instance;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}