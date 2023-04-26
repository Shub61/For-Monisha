using Expressions.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions.ParserTests
{
    [TestClass]
    public class StringParserTests
    {
        private StringParser _parser;
        
        [TestInitialize]
        public void SetUp()
        {
            _parser = new StringParser();                        
        }

        [TestMethod]
        public void ConversionTest0()
        {
            var statement = "A == \\BBB\" AND ABS(CUSIP) > 5";
            var expected = Expression.And
            (
                Expression.AreEqual
                (
                    Expression.Member("A"),
                    Expression.Const("BBB")
                ),
                Expression.GreaterThan
                (
                    Expression.Abs(Expression.Member("CUSIP")),
                    Expression.Const(5d)
                )
            );
            var result = _parser.ToExpression(statement);

            Assert.AreEqual(result, expected);
        }
        
        [TestMethod]
        public void ConversionTest1()
        {
            var statement = "BOOK_CD == \"KR10\" AND CUSIP LIKE(\"CC12\")";
            var expected = Expression.And
            (
                Expression.AreEqual
                (
                    Expression.Member("BOOK_CD"),
                    Expression.Const("KR10")
                ),
                Expression.Like
                (
                    Expression.Member("CUSIP"),
                    Expression.Const("CC12")
                )
            );
            var result = _parser.ToExpression(statement);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest2()
        {
            var statement = "PRICE_PCT==null AND BOOK_CD>3";
            var expected = Expression.And
            (
                Expression.AreEqual
                (
                    Expression.Member("PRICE_PCT"),
                    Expression.Const(null)
                ),
                Expression.GreaterThan
                (
                    Expression.Member("BOOK_CD"),
                    Expression.Const(3d)
                )
            );
            var result = _parser.ToExpression(statement);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest3()
        {
            var statement = "A==\"Some Text\"";
            var expected = Expression.AreEqual
            (                
                Expression.Member("A"),
                Expression.Const("Some Text")                              
            );
            var result = _parser.ToExpression(statement);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ConversionTest4()
        {
            var statement = "ISSUER_COUNTRY == \"US\" AND(PRODUCT_TYPE == \"CORPDBT\" OR PRODUCT_TYPE == \"GLOBHYLD\") AND RATING_GRADE == \"IG\"";
            var expected = Expression.And
            (
                Expression.AreEqual
                (
                    Expression.Member("ISSUER_COUNTRY"),
                    Expression.Const("US")
                ),
                Expression.And
                (
                    Expression.Or
                    (
                        Expression.AreEqual
                        (
                            Expression.Member("PRODUCT_TYPE"),
                            Expression.Const("CORPDBT")
                        ),
                        Expression.AreEqual
                        (
                            Expression.Member("PRODUCT_TYPE"),
                            Expression.Const("GLOBHYLD")
                        )
                    ),
                    Expression.AreEqual
                    (
                        Expression.Member("RATING_GRADE"),
                        Expression.Const("IG")
                    )
                )
            );
            var result = _parser.ToExpression(statement);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Parser_UsesDefaultValue_IfInputString_IsNull()
        {
            var expression = _parser.ToExpression(null, Expression.True);
            Assert.AreEqual(expression, Expression.True);
        }

        [TestMethod]
        public void Parser_UsesDefaultValue_IfInputString_IsEmpty()
        {
            var expression = _parser.ToExpression(string.Empty, Expression.True);
            Assert.AreEqual(expression, Expression.True);
        }
    }
}