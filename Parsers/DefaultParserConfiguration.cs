using Expressions.Abstractions;

namespace Expressions.Parsers
{
    public class DefaultParserConfiguration : IParserConfiguration
    {
        public virtual string GetToken(ExpressionKind kind)
        {
            switch(kind)
            {
                case ExpressionKind.Abs:
                    return nameof(ExpressionKind.Abs);
                case ExpressionKind.Add:
                    return "+";
                case ExpressionKind.And:
                    return "AND";                
                case ExpressionKind.AreEqual:
                    return "==";                
                case ExpressionKind.AreNotEqual:
                    return "!=";                
                case ExpressionKind.Divide:
                    return "/";                
                case ExpressionKind.GreaterThan:
                    return ">";                
                case ExpressionKind.GreaterThanOrEqual:
                    return ">=";                
                case ExpressionKind.LessThan:
                    return "<";                
                case ExpressionKind.LessThanOrEqual:
                    return "<=";                
                case ExpressionKind.Modulo:
                    return "%";            
                case ExpressionKind.Multiply:
                    return "*";                
                case ExpressionKind.Not:
                    return "!";                
                case ExpressionKind.Or:
                    return "OR";                
                case ExpressionKind.Subtract:
                    return "-";                
                case ExpressionKind.Like:
                    return "Like";                
                case ExpressionKind.UnaryPlus:
                    return "s+";                
                case ExpressionKind.UnaryMinus:
                    return "s-";                
            }
            return null;
        }

        public virtual ExpressionKind? GetKind(string value)
        {
            switch(value)
            {
                case "+":
                    return ExpressionKind.Add;
                case "==":
                    return ExpressionKind.AreEqual;
                case "!=":
                    return ExpressionKind.AreNotEqual;
                case "/":
                    return ExpressionKind.Divide;
                case ">":
                    return ExpressionKind.GreaterThan;
                case ">=":
                    return ExpressionKind.GreaterThanOrEqual;
                case "<":
                    return ExpressionKind.LessThan;
                case "<=":
                    return ExpressionKind.LessThanOrEqual;
                case "%":
                    return ExpressionKind.Modulo;
                case "*":
                    return ExpressionKind.Multiply;
                case "!":
                    return ExpressionKind.Not;
                case "-":
                    return ExpressionKind.Subtract;
                case "s+":
                    return ExpressionKind.UnaryPlus;
                case "s-":
                    return ExpressionKind.UnaryMinus;
            }
            if(string.Equals(nameof(ExpressionKind.Abs), value, StringComparison.InvariantCultureIgnoreCase))
            {
                return ExpressionKind.Abs;
            }
            else if(string.Equals(nameof(ExpressionKind.Like), value, StringComparison.InvariantCultureIgnoreCase))
            {
                return ExpressionKind.Like;
            }
            else if(string.Equals(nameof(ExpressionKind.And), value, StringComparison.InvariantCultureIgnoreCase))
            {
                return ExpressionKind.And;
            }
            else if(string.Equals(nameof(ExpressionKind.Or), value, StringComparison.InvariantCultureIgnoreCase))
            {
                return ExpressionKind.Or;
            }
            return null;
        }

        public virtual int GetPriority(ExpressionKind kind)
        {
            switch(kind)
            {
                case ExpressionKind.UnaryMinus:
                case ExpressionKind.UnaryPlus:
                    return 0;
                case ExpressionKind.Not:
                case ExpressionKind.Abs:
                case ExpressionKind.Like:
                    return 1;
                case ExpressionKind.Divide:
                case ExpressionKind.Multiply:
                case ExpressionKind.Modulo:
                    return 2;
                case ExpressionKind.Add:
                case ExpressionKind.Subtract:
                    return 3;
                case ExpressionKind.GreaterThan:
                case ExpressionKind.GreaterThanOrEqual:
                case ExpressionKind.LessThan:
                case ExpressionKind.LessThanOrEqual:
                    return 4;
                case ExpressionKind.AreEqual:
                case ExpressionKind.AreNotEqual:
                    return 5;
                case ExpressionKind.And:
                    return 6;
                case ExpressionKind.Or:
                    return 7;                    
            }   
            throw new NotSupportedException($"Not Supoorted: {kind}");
        }

        public bool IsFuntion(ExpressionKind kind)
        {
            return kind == ExpressionKind.Abs;
        }
    }
}