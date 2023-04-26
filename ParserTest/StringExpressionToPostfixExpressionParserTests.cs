using Expressions.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions.ParserTests
{
    [TestClass]
    public class StringExpressionToPostfixExpressionParserTests
    {
        private StringExpressionToPostfixExpressionParser _parser;
        
        [TestInitialize]
        public void SetUp()
        {
            _parser = new StringExpressionToPostfixExpressionParser();                        
        }

        [TestMethod]
        public void ConversionTest1()
        {
            var expression = "A == \"US\"";
            var expected = new List<string> {"A", "B", "C", "/", "+"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void ConversionTest2()
        {
            var expression = "A * B - C";
            var expected = new List<string> {"A", "B", "*", "C", "-"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void ConversionTest3()
        {
            var expression = "(A - 3) >= 0 OR B <= 0";
            var expected = new List<string> {"A", "3", "-", "0", ">=", "B", "0", "<=", "OR"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
        
        [TestMethod]
        public void ConversionTest4()
        {
            var expression = "B < 0 AND ( A-3 ) > 0";
            var expected = new List<string> {"B", "0", "<", "0", "A", "3", "-", "0", ">", "AND"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
        
        [TestMethod]
        public void ConversionTest5()
        {
            var expression = "A % B == 1";
            var expected = new List<string> {"A", "B", "%", "1", "=="};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
        
        [TestMethod]
        public void ConversionTest6()
        {
            var expression = "!(A % B != 1)";
            var expected = new List<string> {"A", "B", "%", "1", "!=", "!"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
        

        [TestMethod]
        public void ConversionTest7()
        {
            var expression = "ABS(A-B) > 5";
            var expected = new List<string> {"A", "B", "-", "5", ">"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
        
        [TestMethod]
        public void Unary_PlusMinus_Support_ConversionTest8()
        {
            var expression = "-+-5 - AAA";
            var expected = new List<string> {"5", "s-", "s+", "AAA", "-"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void Unary_PlusMinus_Support_ConversionTest9()
        {
            var expression = "-5+--AAA";
            var expected = new List<string> {"5", "s-", "AAA", "s-", "s-", "+"};
            var result = _parser.Convert(expression);

            Assert.IsTrue(expected.SequenceEqual(result));
        }
    }
}