using Expressions.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Expressions.ParserTests
{
    [TestClass]
    public class PostfixExpressionToExpressionTreeConverterTest
    {
        private PostfixExpressionToExpressionConverter _converter;
        
        [TestInitialize]
        public void SetUp()
        {
            _converter = new PostfixExpressionToExpressionConverter();                        
        }

        [TestMethod]
        public void ConversionTest1()
        {            
            var operands = new List<string> {"A", "B", "C", "/", "+"}; // A+B/C
            var expected = Expression.And
            (
                Expression.Member("A"),
                Expression.Divide(Expression.Member("B"), Expression.Member("C"))
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest2()
        {            
            var operands = new List<string> {"A", "B", "*", "C", "-"}; // A*B-C
            var expected = Expression.Subtract
            (                
                Expression.Multiply(Expression.Member("A"), Expression.Member("B")),
                Expression.Member("C")
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest3()
        {            
            var operands = new List<string> {"A", "3", "-", "0", ">=", "B", "0", "<=", "OR"}; // (A-3) >= 0 OR B <=0
            var expected = Expression.Or
            (                
                Expression.GreaterThanOrEqual
                (
                    Expression.Subtract(Expression.Member("A"), Expression.Const(ExpressionType.Numeric, 3d)),
                    Expression.Const(ExpressionType.Numeric, 0d)
                ),
                Expression.LessThanOrEqual(Expression.Member("B"), Expression.Const(ExpressionType.Numeric, 0d))                
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest4()
        {            
            var operands = new List<string> {"B", "0", "<", "A", "3", "-", "0", ">", "AND"}; // B < 0 AND (A - 3) > 0
            var expected = Expression.And
            (     
                Expression.LessThan(Expression.Member("B"), Expression.Const(ExpressionType.Numeric, 0d)),                           
                Expression.GreaterThan
                (
                    Expression.Subtract(Expression.Member("A"), Expression.Const(ExpressionType.Numeric, 3d)),
                    Expression.Const(ExpressionType.Numeric, 0d)
                )               
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        } 

        [TestMethod]
        public void ConversionTest5()
        {            
            var operands = new List<string> {"A", "B", "%", "1", "=="}; // A % B == 1
            var expected = Expression.AreEqual
            (                
                Expression.Modulo(Expression.Member("A"), Expression.Member("B")),
                Expression.Const(ExpressionType.Numeric, 1d)
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }      
        
        [TestMethod]
        public void ConversionTest6()
        {            
            var operands = new List<string> {"A", "B", "%", "1", "!=", "!"}; // !(A % B != 1)
            var expected = Expression.Not
            (        
                Expression.AreNotEqual
                (
                    Expression.Modulo(Expression.Member("A"), Expression.Member("B")),
                    Expression.Const(ExpressionType.Numeric, 1d)
                )                        
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest7()
        {            
            var operands = new List<string> {"A", "B", "-", "ABS", "5", ">"}; // ABS(A-B) > 5
            var expected = Expression.GreaterThan
            (        
                Expression.Abs
                (
                    Expression.Subtract(Expression.Member("A"), Expression.Member("B"))                    
                ),
                Expression.Const(ExpressionType.Numeric, 5d)                        
            );
            var result = _converter.Convert(operands);
            Assert.AreEqual(result, expected);
        }
    }
}