using Expressions.Abstractions;
using Expressions.Parsers;
using Expressions.Visitors.Implementations;
using System.Collections.Concurrent;
using System.Reflection;

namespace Expressions.Evaluators
{
    public class EvaluatorFactory : IEvaluatorFactory
    {
        private static readonly Lazy<IEvaluatorFactory> LazyFactory = new Lazy<IEvaluatorFactory>(() => new EvaluatorFactory(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static IEvaluatorFactory Default = LazyFactory.Value;
        private static IBuilder<string> _parser;
        private readonly ConcurrentDictionary<string, IExpression> _expressionByTextDictionary = new ConcurrentDictionary<string, IExpression>();
        private readonly ConcurrentDictionary<KeyValuePair<Type, IExpression>, IEvaluator> _evaluatorsByExpressionDictionary = new ConcurrentDictionary<KeyValuePair<Type, IExpression>, IEvaluator>();

        private readonly NameResolverContainer _nameResolverContainer = new NameResolverContainer();

        public EvaluatorFactory() : this(new DefaultParserConfiguration()) {}

        public EvaluatorFactory(IParserConfiguration parserConfiguration)
        {
            parserConfiguration = parserConfiguration ?? throw new ArgumentNullException(nameof(parserConfiguration));
            _parser = new StringParser(parserConfiguration);
        }

        public void AddNameResolver(INameResolver nameResolver)
        {
            if(nameResolver == null)
            {
                throw new ArgumentNullException(nameof(nameResolver));
            }
            _nameResolverContainer.Add(nameResolver.Type, nameResolver);
        }
        public IEvaluator GetEvaluator(Type type, string textExpression)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if(string.IsNullOrEmpty(textExpression))
            {
                throw new ArgumentNullException(nameof(textExpression));
            }

            if(!_expressionByTextDictionary.TryGetValue(textExpression, out IExpression expression))
            {
                expression = _parser.ToExpression(textExpression);
                _expressionByTextDictionary.TryAdd(textExpression, expression);
            }
            return GetEvaluator(type, expression);
        }

        public IEvaluator GetEvaluator(Type type, IExpression expression)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if(expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var key = new KeyValuePair<Type, IExpression>(type, expression);
            if(!_evaluatorsByExpressionDictionary.TryGetValue(key, out IEvaluator evaluator))
            {
                var evaluatorType = typeof(Evaluator<>).MakeGenericType(type);
                try
                {
                    evaluator = (IEvaluator) Activator.CreateInstance(evaluatorType, expression);
                }
                catch(TargetInvocationException ex)
                {
                    throw new InvalidOperationException("Cannot create evaluator.", ex.InnerException);
                }
                _evaluatorsByExpressionDictionary.TryAdd(key, evaluator);
            }
            return evaluator;
        }

    }
}