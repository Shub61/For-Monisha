using System.Reflection;

namespace Expressions.Visitors.Implementations
{
    public class NameResolverBase : INameResolver
    {
        private readonly Lazy<ConcurrentDictionary<string, MemberInfo>> _lazyMembers;

        protected NameResolverBase (Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _lazyMembers = new Lazy<ConcurrentDictionary<string, MemberInfo>>(GetFieldAndProperties, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        
        public Type Type { get; }

        public MemberInfo Get(string name)
        {
            return DoGet(name);
        }

        protected virtual MemberInfo DoGet(string name)
        {
            if (_lazyMembers.Value.TryGetValue(name, out var result))
            {
                return result;
            }
            return null;
        }

        private ConcurrentDictionary<string, MemberInfo> GetFieldAndProperties()
        {
            var result = new ConcurrentDictionary<string, MemberInfo>();
            var properties = this.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty);
            AddMembers(properties, result);
            var fileds = this.Type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            AddMembers(fileds, result);
            return result;
        }

        private void AddMembers(IEnumerable<MemberInfo> members, ConcurrentDictionary<string, MemberInfo> registry)
        {
            foreach(var field in members)
            {
                var attribute = (NameOverrideAttribute) field.GetCustomAttribute(typeof(NameOverrideAttribute), true);
                string key = attribute?. Value ?? field.name;
                registry.TryAdd(key, field);
            }
        }

    }
}