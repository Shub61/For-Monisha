
using Expressions.Abstractions;
using Expressions.Helpers;

namespace Expressions.Implementations
{
    public class MemberExpression : Expression, IMemberExpression
    {
        internal const string DefaultVariableName = "_default";
        public MemberExpression(string name, string variableName = DefaultVariableName)
                : base(ExpressionType.Any)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            VariableName = variableName;
        }

        public string Name {get;}

        public string name => throw new NotImplementedException();

        public string VariableName {get;} = DefaultVariableName;
        
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj is MemberExpression other
                   && base.Equals(obj) 
                   && string.Equals(Name, other.Name)                   
                   && string.Equals(VariableName, other.VariableName);
        }

        public override int GetHashCode()
        {
            unchecked
            {         
                int hash = base.GetHashCode() * ObjectHelper.HashCodeSeed + Name.GetHashCode();       
                return hash * ObjectHelper.HashCodeSeed + VariableName.GetHashCode();
            }            
        }

        public override string ToString()
        {
            return IsDeafultVariable() ? Name : $"{VariableName}.{Name}";
        }
        private bool IsDeafultVariable()
        {
            return string.Equals(VariableName, DefaultVariableName);
        }
    }
}