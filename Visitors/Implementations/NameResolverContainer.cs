namespace Expressions.Visitors.Implementations
{
    internal class NameResolverContainer : INameResolverContainer
    {
        private readonly ConcurrentDictionary<Type, INameResolver> _defaultNameResolvers = new ConcurrentDictionary<Type, INameResolver>();
        private readonly ConcurrentDictionary<Type, INameResolver> _nameResolvers = new ConcurrentDictionary<Type, INameResolver>();

        public void Add(Type type, INameResolver nameResolver)
        {
            _nameResolvers.TryAdd(type, nameResolver);
        }

        public void Remove(Type type)
        {
            _nameResolvers.TryRemove(type, out _);
        }

        public INameResolver Get(Type type)
        {
            if(! _nameResolvers.TryGetValue(type, out var nameResolver))
            {
                if(! _defaultNameResolvers.TryGetValue(type, out nameResolver))
                {
                    nameResolver = new DefaultNameResolver(type);
                    _defaultNameResolvers.TryAdd(type, nameResolver);
                }
            }
            return nameResolver;
        }
    }
}