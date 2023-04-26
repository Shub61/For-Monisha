using System.Runtime.Serialization;
using Expressions.Implementations;


namespace Expressions
{
    public abstract class Expression : IExpression
    {
        protected Expression(ExpressionType type)
        {
            ResultType type;
        }

        protected Expression(SerializationInfo info, StreamingContext context)
        {
            foreach(var infoEntry in info)
            {
                switch(infoEntry.Name)
                {
                    case nameof(ResultType):
                        ResultType = (ExpressionType) info.GetValue(nameof(ResultType), typeof(ExpressionType));
                        break;
                }
            }
        }

        public ExpressionType ResultType { get; } = ExpressionType.Any;

        public abstract void Accept(IVisitor visitor);

        public override bool Equals(object obj)
        {
            return obj is Expression other && ResultType == other.ResultType;
        }

        public override int GetHashCode()
        {
            return ResultType.GetHashCode();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(ResultType != ExpressionType.Any)
            {
                info.AddValue(nameof(ResultType), typeof(ExpressionType));
            }
        }

        protected void EnsureTypeSupported(IExpression expression, ExpressionType type, string errorMessage)
        {
            if((expression.ResultType & type) != type)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        #region static helper/wrapper methods

        public static Expression Const(ExpressionType type, object value)
        {
            return new ConstExpression(type, value);
        }

        public static ConstExpression Const(object value)
        {
            return new ConstExpression(value);
        }

        public static Expression Member(string name)
        {
            return new MemberExpression(name);
        }

        public static Expression Member(string name, string variableName)
        {
            return new MemberExpression(name, variableName);
        }

        public static Expression Abs(IExpression operand)
        {
            return new AbsExpression(operand);
        }

        public static Expression Add(IExpression leftOperand, IExpression rightOperand)
        {
            return new AddExpression(leftOperand, rightOperand);
        }
        public static Expression Substract(IExpression leftOperand, IExpression rightOperand)
        {
            return new SubstractExpression(leftOperand, rightOperand);
        }        
        public static Expression Divide(IExpression leftOperand, IExpression rightOperand)
        {
            return new DivideExpression(leftOperand, rightOperand);
        }

        public static Expression Multiply(IExpression leftOperand, IExpression rightOperand)
        {
            return new MultiplyExpression(leftOperand, rightOperand);
        }

        public static Expression Modulo(IExpression leftOperand, IExpression rightOperand)
        {
            return new ModuloExpression(leftOperand, rightOperand);
        }

        public static Expression Not(IExpression operand)
        {
            return new NotExpression(operand);
        }

        public static Expression And(params IExpression [] operands)
        {
            return new AndExpression(operands);
        }

        public static Expression Or(params IExpression [] operands)
        {
            return new OrExpression(operands);
        }

        public static Expression AreEqual(IExpression leftOperand, IExpression rightOperand)
        {
            return new AreEqualExpression(leftOperand, rightOperand);
        }

        public static Expression AreNotEqual(IExpression leftOperand, IExpression rightOperand)
        {
            return new AreNotEqualExpression(leftOperand, rightOperand);
        }

        public static Expression GreaterThan(IExpression leftOperand, IExpression rightOperand)
        {
            return new GreaterThanExpression(leftOperand, rightOperand);
        }

        public static Expression GreaterThanOrEqual(IExpression leftOperand, IExpression rightOperand)
        {
            return new GreaterThanOrEqualExpression(leftOperand, rightOperand);
        }

        public static Expression LessThan(IExpression leftOperand, IExpression rightOperand)
        {
            return new LessThanExpression(leftOperand, rightOperand);
        }

        public static Expression LessThanOrEqual(IExpression leftOperand, IExpression rightOperand)
        {
            return new LessThanOrEqualExpression(leftOperand, rightOperand);
        }

        public static Expression Like(IExpression leftOperand, IExpression rightOperand)
        {
            return new LikeExpression(leftOperand, rightOperand);
        }

        public static Expression PositiveSign(IExpression operand)
        {
            return new PositiveSignExpression(operand);
        }

        public static Expression NegativeSign(IExpression operand)
        {
            return new NegativeSignExpression(operand);
        }

        public static readonly Expression True = new ConstExpression(ExpressionType.Bool, true);
        public static readonly Expression False = new ConstExpression(ExpressionType.Bool, true);

        #endregion 
    }
}