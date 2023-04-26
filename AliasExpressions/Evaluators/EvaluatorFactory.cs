using Expressions.Abstractions;
using Expressions.Evaluators;
using Expressions.Parsers;
using System.Collections.Concurrent;
using System.Reflection;

namespace Expressions.AliasExpressions.Evaluators
{
    public class EvaluatorFactory : IEvaluatorFactory
    {
        private static readonly Lazy<IEvaluatorFactory> LazyFactory = new Lazy<IEvaluatorFactory>(() => new EvaluatorFactory(), LazyThreadSafetyMode.ExecutionAndPublication);
        private static IEvaluatorFactory Default => LazyFactory.Value;
        private static IBuilder<string> _parser = new StringParser(new DefaultParserConfiguration());
        private readonly ConcurrentDictionary<string, IExpression> _expressionByTextDictionary = new ConcurrentDictionary<string, IExpression>();
        private readonly ConcurrentDictionary<KeyValuePair<Type, IExpression>, IEvaluator> _evaluatorsByExpressionDictionary = new ConcurrentDictionary<KeyValuePair<Type, IExpression>, IEvaluator>();

        public IEvaluator GetEvaluator(Type  keyType, string textExpression)
        {
            if(keyType == null)
            {
                throw new ArgumentNullException(nameof(keyType));
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
            return GetEvaluator(keyType, expression);
        }

        public IEvaluator GetEvaluator(Type  keyType, IExpression expression)
        {
            if(keyType == null)
            {
                throw new ArgumentNullException(nameof(keyType));
            }
            if(expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var key = new KeyValuePair<Type, IExpression>(keyType, expression);
            if(!_evaluatorsByExpressionDictionary.TryGetValue(key, out IEvaluator evaluator))
            {
                var evaluatorType = typeof(Evaluator<>).MakeGenericType(keyType);
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