
using Expressions.Abstractions;
using System.Globalization;

namespace Expressions.Parsers
{
    public class PostfixExpressionToExpressionConverter
    {
        private readonly static string [] _dateFormats = new string [] {"MM/dd/yyyy", "yyyy-MM-dd"};
        private readonly IParserConfiguration _confguration;

        public PostfixExpressionToExpressionConverter()
                : this(new DefaultParserConfiguration()) {}        
        public PostfixExpressionToExpressionConverter(IParserConfiguration configuration)
        {
            _confguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IExpression Convert(IReadOnlyCollection<string> operands)
        {
            var expressionStack = new Stack<IExpression>();
            foreach(var operand in operands)
            {
                var kind = _confguration.GetKind(operand);
                if(kind != null)
                {
                    IExpression expression = null;
                    if(IsUnaryOperation(kind.Value))
                    {
                        switch(kind.Value)
                        {
                            case ExpressionKind.Abs:
                                expression = Expression.Abs(expressionStack.Pop());
                                break;
                            case ExpressionKind.Not:
                                expression = Expression.Not(expressionStack.Pop());
                                break;
                            case ExpressionKind.UnaryPlus:
                                expression = Expression.PositiveSign(expressionStack.Pop());
                                break;
                            case ExpressionKind.UnaryMinus:
                                expression = Expression.NegativeSign(expressionStack.Pop());
                                break;
                        }
                    }
                    else
                    {
                        var y = expressionStack.Pop();
                        var x = expressionStack.Pop();
                        switch(kind.Value)
                        {
                            case ExpressionKind.Add:
                                expression = Expression.Add(x, y);
                                break;
                            case ExpressionKind.And:
                                expression = Expression.And(x, y);
                                break;
                            case ExpressionKind.AreEqual:
                                expression = Expression.AreEqual(x, y);
                                break;
                            case ExpressionKind.AreNotEqual:
                                expression = Expression.AreNotEqual(x, y);
                                break;
                            case ExpressionKind.Divide:
                                expression = Expression.Divide(x, y);
                                break;
                            case ExpressionKind.GreaterThan:
                                expression = Expression.GreaterThan(x, y);
                                break;
                            case ExpressionKind.GreaterThanOrEqual:
                                expression = Expression.GreaterThanOrEqual(x, y);
                                break;
                            case ExpressionKind.LessThan:
                                expression = Expression.LessThan(x, y);
                                break;
                            case ExpressionKind.LessThanOrEqual:
                                expression = Expression.LessThanOrEqual(x, y);
                                break;
                            case ExpressionKind.Modulo:
                                expression = Expression.Modulo(x, y);
                                break;
                            case ExpressionKind.Multiply:
                                expression = Expression.Multiply(x, y);
                                break;
                            case ExpressionKind.Or:
                                expression = Expression.Or(x, y);
                                break;
                            case ExpressionKind.Subtract:
                                expression = Expression.Subtract(x, y);
                                break;
                            case ExpressionKind.Like:
                                expression = Expression.Like(x, (IConstantExpression) y);
                                break;
                        }
                    }
                    expressionStack.Push(expression);
                }
                else if (string.Equals(operand, "null", StringComparison.InvariantCultureIgnoreCase))
                {
                    expressionStack.Push(Expression.Const(null));                    
                }
                else if (bool.TryParse(operand, out var boolValue))
                {
                    expressionStack.Push(Expression.Const(ExpressionType.Bool, boolValue));
                }
                else if (DateTime.TryParseExact(operand, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dateTimeValue))
                {
                    expressionStack.Push(Expression.Const(ExpressionType.DateTime, dateTimeValue));
                }
                else if (operand.StartsWith("\"") && operand.EndsWith("\"") || 
                        operand.StartsWith("'") && operand.EndsWith("'"))
                {
                    expressionStack.Push(Expression.Const(operand.Substring(1, operand.Length - 2)));
                }
                else if (operand.StartsWith(".") || char.IsDigit(operand[0]))
                {
                    expressionStack.Push(Expression.Const(ExpressionType.Numeric, operand));
                }
                else
                {
                    expressionStack.Push(Expression.Member(operand));
                }
            }
            return expressionStack.Pop();            
        }

        private bool IsUnaryOperation(Expression kind)
        {
            return kind == ExpressionKind.Abs
                || ExpressionKind.Not
                || ExpressionKind.UnaryPlus
                || ExpressionKind.UnaryMinus;
        }        
    }
}