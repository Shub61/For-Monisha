namespace Expressions.AliasExpressionTests
{
    public class TestBase
    {
        protected const double _delta = 0.00001d;
        protected const string _textExpression = "0.37 * RTX + 94.2062 + 0.37 * CARR + 0.18 * OTIS";
        protected IValueProvider<string, double> _provider;
        protected IValueProvider<string, double> _alternativeProvider;
        protected IBuilder<string, double> _parser;
        protected NumericVisitor<string> _visitor;

        [TestInitialize]
        public void Init()
        {
            _provider = new InstanceContainer<TestClass, string, double>(i => i.ID, i=> i.Value)
            {
                new TestClass {IDictionary = "RTX", Value = 1d},
                new TestClass {IDictionary = "CARR", Value = 10d},
                new TestClass {IDictionary = "OTIS", Value = 100d},
            };

            _parser = new StringParser (new DefaultParserConfiguration());
            _visitor = new NumericVisitor<string>();

            var provided = new SimpleValueProvider<string, double>();
            provided.Add("RTX", 1d);
            provided.Add("CARR", 10d);
            provided.Add("OTIS", 100d);
            _alternativeProvider = provided;
        }

        internal class TestClass
        {
            public string ID {get; set;}
            public double Value {get; set;}
        }
    }
}