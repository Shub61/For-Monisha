using Expressions.AliasExpressions.Abstractions;
using MsExpressions = System.Linq.Expressions;

namespace Expressions.AliasExpressions.Implementations
{
    public class NumericVisitor<TK> : VisitorBase, IResultProvider<Func<IValueProvider<TK, double>, double>>
    {
        private static readonly MethodInfo ProviderGetMethodInfo = typeof(IValueProvider<TK, double).GetMethod(nameof(IValueProvider<TK, double>.Get), new Type[] { typeof(TK)});
        public override MsExpressions.ParameterExpression Parameter {get;} = MsExpressions.Expression.Parameter(typeof(IValueProvider<TK, double), MemeberExpression.DefaultVariableName);

        public Func<IValueProvider<TK, double>, double> Get()
        {
            return MsExpressions.Expression.Lambda<Func<IValueProvider<TK, double>, double>>(Current, Parameter).Compile();
        }

        protected internal override MsExpressions.Expression VisitMemberExpression(MemberExpression expression)
        {
            MsExpressions.Expression current = Parameter;
            current = MsExpressions.Expression.Call(Parameter, ProviderGetMethodInfo, MsExpressions.Expression.Constant(expression.Name));
            return current;
        }
        
    }
}