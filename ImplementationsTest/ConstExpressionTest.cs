using Expressions.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions.ImplementationsTests
{
    [TestClass]
    public class ConstExpressionTest
    {
        [DataTestMethod]
        [DataRow(ExpressionType.Numeric, 123)]
        [DataRow(ExpressionType.Numeric, 123L)]
        [DataRow(ExpressionType.Numeric, 123.123f)]
        [DataRow(ExpressionType.Numeric, 123.123d)]

        public void IsProperlyCreated_When_TypeAndValueSpecified(ExpressionType type, object value)
        {
            var expression = new ConstExpression(type, value);
            Assert.AreEqual(expression.ResultType, type);
            Assert.AreEqual(expression.Value, type);            
        }

        [DataTestMethod]
        [DataRow(ExpressionType.Numeric, "123", typeof(double))]
        [DataRow(ExpressionType.Numeric, "123.123", typeof(double))]

        public void Value_IsCoverted_ToNumeric(ExpressionType type, object value, Type convertedType)
        {
            var expression = new ConstExpression(type, value);
            Assert.AreEqual(expression.ResultType, type);
            Assert.AreEqual(expression.Value.GetType(), convertedType);
            Assert.AreEqual(expression.Value, Convert.ChangeType(value, convertedType));        
        }

        [TestMethod]
        public void IsProperlyCreated_When_DateTime_TypeAndValueSpecified()
        {
            var value = DateTime.Now;
            var type = ExpressionType.DateTime;
            var expression = new ConstExpression(type, value);
            Assert.AreEqual(expression.ResultType, type);
            Assert.AreEqual(expression.Value, value);        
        }

        [DataTestMethod]
        [DataRow(123, ExpressionType.Numeric)]
        [DataRow(123L, ExpressionType.Numeric)]
        [DataRow(123.123f, ExpressionType.Numeric)]
        [DataRow(123.123d, ExpressionType.Numeric)]
        [DataRow("123", ExpressionType.Any)]

        
        public void IsProperlyCreated_When_JustValueSpecified(object value, ExpressionType expressionType)
        {
            var expression = new ConstExpression(value);
            Assert.AreEqual(expression.ResultType, expressionType);
            Assert.AreEqual(expression.Value, value);
        }

        [DataTestMethod]
        [DataRow(ExpressionType.Numeric, 123, ExpressionType.Numeric, 123)]
        [DataRow(ExpressionType.String, "123.123", ExpressionType.String, "123.123")]

        public void EqualsMethod_ReturnsTrue_When_SameTypeAndValueSpecified(ExpressionType type1, object value1, ExpressionType type2, object value2)
        {
            var expression1 = new ConstExpression(type1, value1);
            var expression2 = new ConstExpression(type2, value2);
            Assert.IsTrue(expression1.Equals(expression2));            
        }
        
        [DataTestMethod]
        [DataRow(ExpressionType.Numeric, 123, ExpressionType.Any, 123)]
        [DataRow(ExpressionType.String, 123, ExpressionType.Numeric, 127)]
        [DataRow(ExpressionType.String, "123.123", ExpressionType.Numeric, 123.123)]
        [DataRow(ExpressionType.String, "123.123", ExpressionType.String, "12.3123")]

        public void EqualsMethod_ReturnsFalse_When_DifferentTypeOrValueSpecified(ExpressionType type1, object value1, ExpressionType type2, object value2)
        {
            var expression1 = new ConstExpression(type1, value1);
            var expression2 = new ConstExpression(type2, value2);
            Assert.IsFalse(expression1.Equals(expression2));            
        }

        [DataTestMethod]
        [DataRow(ExpressionType.Numeric, 123, ExpressionType.Numeric, 123)]
        [DataRow(ExpressionType.Bool, true, ExpressionType.Bool, true)]        
        [DataRow(ExpressionType.String, "123.123", ExpressionType.String, "12.3123")]

        public void GetHashCode_ReturnsSameValue_When_SameTypeAndValueSpecified(ExpressionType type1, object value1, ExpressionType type2, object value2)
        {
            var expression1 = new ConstExpression(type1, value1);
            var expression2 = new ConstExpression(type2, value2);
            Assert.AreEqual(expression1.GetHashCode(), expression2.GetHashCode());            
        }
    }
}