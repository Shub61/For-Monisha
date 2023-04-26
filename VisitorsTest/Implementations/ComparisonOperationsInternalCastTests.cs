namespace Expressions.VisitorsTest.Implementations
{
    [TestClass]
    public class ComparisonOperationsInternalCastTests: TestBaseClass
    {
        [TestMethod]
        public void Equals_WithBothIntProperties_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.IntProperty1)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }

        [TestMethod]
        public void Add_WithBothIntProperties_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.IntProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }

        [TestMethod]
        public void Equals_WithIntAndConstString_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new ConstExpression("1"));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }
        
        [TestMethod]
        public void Equals_WithIntAndConstString_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new ConstExpression("2"));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }

        [TestMethod]
        public void Equals_WithIntAndConstInt_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new ConstExpression(1));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }

        [TestMethod]
        public void Equals_WithIntAndConstInt_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new ConstExpression(2));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }

        [TestMethod]
        public void Equals_WithConstString_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new ConstExpression("1"), new MemberExpression(nameof(MathTestsClass.IntProperty1)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }

        [TestMethod]
        public void Equals_WithConstString_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new ConstExpression("2"), new MemberExpression(nameof(MathTestsClass.IntProperty1)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }

        [TestMethod]
        public void Equals_WithConstStringAndNullableInt_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new ConstExpression("1"), new MemberExpression(nameof(MathTestsClass.NullableInt1)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }

        [TestMethod]
        public void Equals_WithConstStringAndNullableInt_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new ConstExpression("2"), new MemberExpression(nameof(MathTestsClass.NullableInt1)));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }

        [TestMethod]
        public void Equals_WithNullableIntAndConstString_AndSameValues_UsingInternalCast_ReturnsTrue()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.NullableInt1)), new ConstExpression("1"));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, true);
        }

        [TestMethod]
        public void Equals_WithNullableIntAndConstString_AndDifferentValues_UsingInternalCast_ReturnsFalse()
        {
            var expression = new AreEqualExpression(new MemberExpression(nameof(MathTestsClass.NullableInt1)), new ConstExpression("2"));
            ConvertExpressionToFunctionAndCheckCondition<bool>(expression, false);
        }
    }
}