namespace Expressions.VisitorsTest.Implementations
{
    [TestClass]
    public class NameResolverTests
    {
        private IEvaluatorFactory _factory;
        private TestClass _instance = new TestClass();

        [TestInitialize]
        public void SetUp()
        {
            _factory = new EvaluatorFactory();
        }

        [TestMethod]
        public void CanAccess_Property_WithoutNameOverride()
        {
            var textExpression = $"{nameof(TestClass.Property1)} == {_instance.Property1}";
            var evaluator = _factory.GetEvaluator(typeof(TestClass), textExpression);
            var result = evaluator.Evaluate(_instance);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanAccess_Property_ByIts_NameOverride()
        {
            var textExpression = $"{TestClass.PropertyNameOverride} == {_instance.Property2}";
            var evaluator = _factory.GetEvaluator(typeof(TestClass), textExpression);
            var result = evaluator.Evaluate(_instance);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanAccess_Property_ByIts_OriginalName_If_ThereIsNameOverride()
        {
            var textExpression = $"{nameof(TestClass.Property2)} == {_instance.Property2}";
            var evaluator = _factory.GetEvaluator(typeof(TestClass), textExpression);
            var result = evaluator.Evaluate(_instance);           
        }

        [TestMethod]
        public void CanAccess_Property_ByIts_RuntimeNameOverride()
        {
            var textExpression = $"{TestClass.PropertyNameRuntimeOverride} == {_instance.Property3}";
            var nameResolver = new NameResolver<TestBaseClass>().Map(TestBaseClass.PropertyNameRuntimeOverride,
                                                            i=> i.Property3);
            _factory.AddNameResolver(nameResolver);

            var evaluator = _factory.GetEvaluator(typeof(TestClass), textExpression);
            var result = evaluator.Evaluate(_instance);
            Assert.IsTrue(result);  
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NameResolver_Cannot_BeModified_AfterFirstUsage()
        {
            var textExpression = $"{TestClass.PropertyNameRuntimeOverride} == {_instance.Property3}";
            var nameResolver = new NameResolver<TestBaseClass>().Map(TestBaseClass.PropertyNameRuntimeOverride,
                                                            i=> i.Property3);
            _factory.AddNameResolver(nameResolver);
            var evaluator = _factory.GetEvaluator(typeof(TestClass), textExpression);
            nameResolver.Map(TestClass.PropertyNameRuntimeOverride, i=> i.Property2);
        }

        public class TestClass
        {
            public const string PropertyNameOverride = "CustomName";
            public const string PropertyNameRuntimeOverride = "CustomName1";

            public int Property1 {get;} = 5;
            [NameOverride(PropertyNameOverride)]
            public int Property2 {get;} = 10;
            public int Property3 {get;} = 15;
        }
    }
}