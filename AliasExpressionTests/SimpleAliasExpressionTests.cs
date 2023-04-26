namespace Expressions.AliasExpressionTests
{
    [TestClass]
    public class SimpleAliasExpressionTests : TestBase
    {
        [TestMethod]        
        public void NumericExpression_AsExpression_IsProperlyCalculated_BasingOnSufficientInputDataProvided()
        {
            var expression = _parser.ToExpression(_textExpression);
            expression.Accept(_visitor);
            var function = _visitor.Get();

            var result = function(_provider);
            Assert.IsTrue(Math.Abs(result - 116.2762) <= _delta);
        }

        [TestMethod]        
        public void NumericExpression_AsExpression_IsProperlyCalculated_BasingOnSufficientInputDataProvided_WithAlternativeProvider()
        {
            var expression = _parser.ToExpression(_textExpression);
            expression.Accept(_visitor);
            var function = _visitor.Get();

            var result = function(_provider);
            Assert.IsTrue(Math.Abs(result - 116.2762) <= _delta);
        }

        [TestMethod]        
        public void NumericExpression_ThrowsException_IfInputDataLackInfo()
        {
            var expression = _parser.ToExpression(_textExpression + "Something * 123");
            expression.Accept(_visitor);
            var function = _visitor.Get();
            
            Assert.ThrowsException<ArgumentException>(() => function(_provider));
        }
    }
}