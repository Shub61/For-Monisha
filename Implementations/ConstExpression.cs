
using Expressions.Abstractions;
using Expressions.Helpers;
using System.Runtime.Serialization;

namespace Expressions.Implementations
{
    public class ConstExpression : Expression, IConstantExpression
    {
        public ConstExpression(ExpressionType type, object value)
                : base(type)
        {
            value = ObjectHelper.ChangeType(value, type);
        }

        public ConstExpression(object value)
                : this(FindMatchingType(value), value) {}
        
        public ConstExpression(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue(nameof(Value, Value, typeof(object)));
            base.GetObjectData(info, context);
        }

        public object Value {get;}

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

            return obj is ConstExpression other
                   && base.Equals(obj)                    
                   && Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {                
                return base.GetHashCode() * ObjectHelper.HashCodeSeed + (Value?.GetHashCode() ?? 0);
            }            
        }

        public override string ToString()
        {
            return ObjectHelper.FormatValue(Value);
        }

        public static ExpressionType FindMatchingType(object value)
        {
            if(value == null)
            {
                return ExpressionType.Any;
            }

            if(value is bool)
            {
                return ExpressionType.Bool;
            }

            if(value is DateTime)
            {
                return ExpressionType.DateTime;
            }

            if(TypeUtility.IsNumericOrNumericNullableType(value.GetType()))
            {
                return ExpressionType.Numeric;
            }
            return ExpressionType.Any;
        }
    }
}