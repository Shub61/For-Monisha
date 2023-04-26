using Expressions.Abstractions;

namespace Expressions.Parsers
{
    public class StringExpressionToPostfixExpressionParser
    {
        private readonly IParserConfiguration _confguration;

        public StringExpressionToPostfixExpressionParser()
                : this(new DefaultParserConfiguration()) {}        
        public StringExpressionToPostfixExpressionParser(IParserConfiguration configuration)
        {
            _confguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IReadOnlyCollection<string> Convert(string value)
        {
            var result = new List<string>();
            var operationStack = new Stack<string>();
            int position = 0;
            var lastTokenType = TokenType.Operation;
            var pieces = SplitTextExpressionByOperationsAndWhiteSpaces(value);
            while(position < pieces.Count)
            {
                var piece = pieces[position];
                var kind = _confguration.GetKind(piece);
                if(piece == "(")
                {
                    operationStack.Push(piece);
                    lastTokenType = TokenType.Operation;
                }
                else if(piece == ")")
                {
                    while(operationStack.Any())
                    {
                        var headOperation = operationStack.Pop();
                        if(headOperation != "(")
                        {
                            result.Add(headOperation);
                        }
                        else
                        {
                            break;
                        }
                    }
                    lastTokenType = TokenType.Operation;
                }
                else if(kind != null)
                {
                    if(_confguration.IsFuntion(kind.Value))
                    {
                        operationStack.Push(piece);
                        lastTokenType = TokenType.Operand;
                    }
                    else
                    {
                        if(lastTokenType == TokenType.Operation && (kind == ExpressionKind.Add || kind == ExpressionKind.Subtract))
                        {
                            kind = kind == ExpressionKind.Add ? ExpressionKind.UnaryPlus : ExpressionKind.UnaryMinus; 
                            piece = _confguration.GetToken(kind.Value);
                        }
                        while (operationStack.Any() && operationStack.Peek() != "(")
                        {
                            var headOperation = operationStack.Pop(); 
                            var headKind = _confguration.GetKind(headOperation);
                            var headPriority = _confguration.GetPriority(headKind.Value);
                            var currentPriority = _confguration.GetPriority(kind.Value);
                            if (headPriority < currentPriority)
                            {
                                operationStack.Pop();
                                result.Add(headOperation);
                            }
                            else
                            {
                                break;
                            }                        
                        }
                        operationStack.Push(piece);
                        lastTokenType = TokenType.Operation;
                    }
                }
                else
                {
                    result.Add(piece);   
                    lastTokenType = TokenType.Operand;
                }
                position ++;
            }

            while (operationStack.Any())
            {
                result.Add(operationStack.Pop());
            }
            return result;            
        }

        private IList<string> SplitTextExpressionByOperationsAndWhiteSpaces(string value)
        {
            var tokens = new List<string>();
            var keywords = GetSupportedOperations()
                            .Union(new List<string> { "(", ")" })
                            .OrderByDescending(t=> t.Length)
                            .ToList();
            var startPosition = 0;
            var quoteSymbolMet = false;
            for(int position = 0; position < value.Length; position++)
            {
                var currentSymbol = value[position];
                if(currentSymbol == '"' || currentSymbol == '\'')
                {
                    if(quoteSymbolMet)
                    {
                        var token = value.Substring(startPosition, position - startPosition + 1);
                        tokens.Add(token);
                        startPosition = position + 1;
                    }
                    else
                    {
                        startPosition = position;
                    }
                    quoteSymbolMet = !quoteSymbolMet;
                    continue;
                }
                if(!quoteSymbolMet)
                {
                    if(char.IsWhiteSpace(currentSymbol))
                    {
                        if(position != startPosition)
                        {
                            var token = value.Substring(startPosition, position - startPosition);
                            tokens.Add(token);
                        }
                        startPosition = position + 1;
                        continue;
                    }
                    foreach (var keyword in keywords)
                    {
                        if(value.IndexOf(keyword, position, StringComparison.InvariantCultureIgnoreCase) == position)
                        {
                            if(position != startPosition)
                            {
                                var token = value.Substring(startPosition, position - startPosition);
                                tokens.Add(token);
                            }
                            tokens.Add(value.Substring(position, keyword.Length));
                            startPosition = position + keyword.Length;
                            break;
                        }                        
                    }
                }
            }
            if( startPosition < value.Length)
            {
                var token = value.Substring(startPosition);
                tokens.Add(token);
            }
            return tokens;
        }

        private IList<string> GetSupportedOperations()
        {
            return Enum.GetValues(typeof(ExpressionKind))
                        .Cast<ExpressionKind>()
                        .Select(k=> _confguration.GetToken(k))
                        .Where(v => v != null)
                        .Select(op => op.All(char.IsLetter) ? $" {op} " : op)
                        .ToList();
        }

        private enum TokenType
        {
            Operation,
            Operand,
        }
    }
}