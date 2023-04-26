namespace Expressions.Visitors.Implementations
{
    public class PredicateVisitor<T> : VisitorBase, IResultProvider<Func<T, bool>> where T: class
    {
        public override MsExpression.ParameterExpression Parameter {get;} => MsExpression.Expression.Parameter(typeof(T), MemberExpression.DefaultVaribaleName);
        public readonly INameResolverContainer _nameResolverContainer;

        public PredicateVisitor(INameResolverContainer nameResolverContainer)
        {
            _nameResolverContainer = nameResolverContainer ?? throw new ArgumentNullException(nameof(nameResolverContainer));
        }

        public PredicateVisitor() : this(new NameResolverContainer()) {}

        public Func<T, bool> Get()
        {
            return MsExression.Expression.Lambda<Func<T, bool>>(Current, Parameter).Compile();
        }

        protected internal override MsExpressons.Expression VisitMemberExpression(MemberExpression expression)
        {
            MsExpressions.Expression current = Parameter;
            var members = expression.Name.Split('.');
            foreach(var memeber in members)
            {
                var nameResolver = _nameResolverContainer.Get(current.Type);
                var info = nameResolver.Get(member);
                if(info == null)
                {
                    throw new ArgumentException($"Member '{member}' is not defined in type: {current.Type}");
                }
                if(info is PropertyInfo propertyInfo)
                {
                    current = MsExpressions.Expression.Property(current, propertyInfo);
                }
                else
                {
                    current = MsExpressions.Expression.Field(current, (FieldInfo) info);
                }
            }
            return current;
        }
    }
}