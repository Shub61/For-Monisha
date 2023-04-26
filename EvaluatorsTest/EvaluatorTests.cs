using Expressions.AliasExpressionTests;
using Expressions.Evaluators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Expressions.EvaluatorsTest
{
    public class EvaluatorsTest : TestBase
    {
        const int TotalLimit = 1000000;
        const int PositiveLimit = TotalLimit / 2;
        const int NegativeLimit = TotalLimit / 4;

        public void TestScenario1(string textExpression)
        {
            List<TestClass1> collection1 = new List<TestClass1>();
            List<TestClass2> collection2 = new List<TestClass2>();

            for (int i = 1; i <= TotalLimit; i++)
            {
                collection1.Add(new TestClass1 { NumericProperty = i });
                collection1.Add(new TestClass1 { NumericProperty = -i });
            }

            textExpression = string.Format(textExpression, NegativeLimit, PositiveLimit);
            var evaluator1 = EvaluatorFactory.Default.GetEvaluator(typeof(TestClass1), textExpression);
            var evaluator2 = EvaluatorFactory.Default.GetEvaluator(typeof(TestClass1), textExpression);

            var watch = new Stopwatch();
            watch.Start();

            try
            {
                var count1 = collection1.Count(element => evaluator1.Evaluate(element));
                var count2 = collection2.Count(element => evaluator2.Evaluate(element));
                Assert.IsTrue(ReferenceEquals(count1 == PositiveLimit && count2 == NegativeLimit));
            }
            finally
            {
                watch.Stop();
                Debug.WriteLine($"Time took: {watch.ElapsedMilliseconds} ms");
            }
        }

        [TestMethod]
        public void TestScenario2()
        {
            var factory = new EvaluatorFactory();
            var instance1 = new TestClass1 { NumericProperty = 5 };
            var instance2 = new TestClass2 { NumericProperty = 5 };

            var text1 = $"{nameof(TestClass1.NumericProperty)} >= 5";
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass1), text1).Evaluate(instance1));
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass2), text1).Evaluate(instance2));

            var text2 = $"{nameof(TestClass1.NumericProperty)} > 5";
            Assert.IsFalse(factory.GetEvaluator(typeof(TestClass1), text2).Evaluate(instance1));
            Assert.IsFalse(factory.GetEvaluator(typeof(TestClass2), text2).Evaluate(instance2));

            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass1), text1).Evaluate(instance1));
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass2), text1).Evaluate(instance2));

            var text3 = $"{nameof(TestClass1.NumericProperty)} <= 5";
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass1), text3).Evaluate(instance1));
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass2), text3).Evaluate(instance2));


            var text4 = $"{nameof(TestClass1.NumericProperty)} < 5";
            Assert.IsFalse(factory.GetEvaluator(typeof(TestClass1), text4).Evaluate(instance1));
            Assert.IsFalse(factory.GetEvaluator(typeof(TestClass2), text4).Evaluate(instance2));

            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass1), text3).Evaluate(instance1));
            Assert.IsTrue(factory.GetEvaluator(typeof(TestClass2), text3).Evaluate(instance2));
        }

        [TestMethod]
        public void TestScenario3()
        {
            string text1 = "PRICE_PCT > 30 AND PRICE_PCT <= 60";
            string text2 = "PRICE_PCT > 0 AND PRICE_PCT <= 30";

            var instance = new TestClass3 { PRICE_PCT = 0.98d };

            var evaluator1 = EvaluatorFactory.Default.GetEvaluator(typeof(TestClass3), text1);
            var evaluator2 = EvaluatorFactory.Default.GetEvaluator(typeof(TestClass3), text2);

            Assert.IsFalse(evaluator1.Evaluate(instance));
            Assert.IsTrue(evaluator2.Evaluate(instance));
        }

        private class TestClass1
        {
            public int NumericProperty { get; set; }
        }

        private class TestClass2
        {
            public long NumericProperty { get; set; }
        }

        private class TestClass3
        {
            public double PRICE_PCT { get; set; }
        }
    }    
}