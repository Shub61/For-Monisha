using Expressions.Abstractions;

namespace Expressions.Parsers
{
    public class StringParser: IBuilder<string>
    {
        private readonly StringExpressionToPostfixExpressionParser _postfixParser;
        private readonly PostfixExpressionToExpressionConverter _converter;

        public StringParser(): this(new DefaultParserConfiguration()){}

        public StringParser(IParserConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _postfixParser = new StringExpressionToPostfixExpressionParser(configuration);
            _converter = new PostfixExpressionToExpressionConverter(configuration);
        }

        public IExpression ToExpression(string value, IExpression defaultValue = null)
        {
            if(string.IsNullOrEmpty(value) || "(empty)".Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                if(defaultValue != null)
                {
                    return defaultValue;
                }
                throw new ArgumentNullException(nameof(value));
            }
            var operands = _postfixParser.Convert(value);
            var result = _converter.Convert(operands);
            
            return result ;
        }

        public string FromExpression(IExpression expression, string defaultValue = null)
        {
            if(expression == null)
            {
                if(defaultValue != null)
                {
                    return defaultValue;
                }
                
                throw new ArgumentNullException(nameof(expression));
            }
            
            return expression.ToString();
        }
    }
}