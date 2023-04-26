namespace Expressions.VisitorsTest.Implementations
{
    [TestClass]
    public class ArithmeticOperationsInternalCastTests: TestBaseClass
    {
        #region Add Expression tests includes checks against variety of supported types
        [TestMethod]
        public void Add_WithBothIntProperties_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.IntProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<int>(expression, _instance.IntProperty1 + _instance.IntProperty2);
        }

        [TestMethod]
        public void Add_WithBothLongProperties_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.LongProperty1)), new MemberExpression(nameof(MathTestsClass.LongProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<int>(expression, _instance.LongProperty1 + _instance.LongProperty2);
        }

        [TestMethod]
        public void Add_WithIntAndNullableInt_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.LongProperty1)), new MemberExpression(nameof(MathTestsClass.NullableInt2)));
            ConvertExpressionToFunctionAndCheckCondition<int?>(expression, _instance.IntProperty1 + _instance.NullableInt2);
        }

        [TestMethod]
        public void Add_WithNullableIntAndInt_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableInt1)), new MemberExpression(nameof(MathTestsClass.IntProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<int?>(expression, _instance.NullableInt1 + _instance.IntProperty2);
        }

        [TestMethod]
        public void Add_WithIntAndNullableLong_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.NullableLong2)));
            ConvertExpressionToFunctionAndCheckCondition<int?>(expression, _instance.IntProperty1 + _instance.NullableLong2);
        }

        [TestMethod]
        public void Add_WithIntAndLong_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.LongProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<int?>(expression, _instance.IntProperty1 + _instance.LongProperty2);
        }

        [TestMethod]
        public void Add_WithNullableIntAndNullableLong_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableInt1)), new MemberExpression(nameof(MathTestsClass.NullableLong2)));
            ConvertExpressionToFunctionAndCheckCondition<long?>(expression, _instance.NullableInt1 + _instance.NullableLong2);
        }

        [TestMethod]
        public void Add_WithNullableIntAndLong_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableInt1)), new MemberExpression(nameof(MathTestsClass.LongProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<long?>(expression, _instance.NullableInt1 + _instance.LongProperty2);
        }

        [TestMethod]
        public void Add_WithIntAndNullableDecimal_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.NullableDecimal2)));
            ConvertExpressionToFunctionAndCheckCondition<decimal?>(expression, _instance.IntProperty1 + _instance.NullableDecimal2);
        }

        [TestMethod]
        public void Add_WithIntAndString_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.IntProperty1)), new MemberExpression(nameof(MathTestsClass.StringProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<int>(expression, _instance.IntProperty1 + int.Parse(_instance.StringProperty2));
        }

        [TestMethod]
        public void Add_WithNullableDecimalAndString_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableDecimal1)), new ConstExpression(_instance.DecimalProperty2.ToString()));
            ConvertExpressionToFunctionAndCheckCondition<decimal?>(expression, _instance.IntProperty1 + _instance.DecimalProperty2);
        }

        [TestMethod]
        public void Add_WithNullableDoubleAndDouble_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableDouble1)), new MemberExpression(nameof(MathTestsClass.DoubleProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<double?>(expression, _instance.NullableDouble1 + _instance. DoubleProperty2);           

        }

        [TestMethod]
        public void Add_WithBothDoubleProperties_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.DoubleProperty1)), new MemberExpression(nameof(MathTestsClass.DoubleProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<double>(expression, _instance.NullableDouble1 + _instance.DoubleProperty2);          
        }

        [TestMethod]
        public void Add_WithNullableFloatAndFloat_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.NullableFloat1)), new MemberExpression(nameof(MathTestsClass.FloatProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<float?>(expression, _instance.NullableFloat1 + _instance.FloatProperty2);          
        }

        [TestMethod]
        public void Add_WithBothFloatProperties_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new AddExpression(new MemberExpression(nameof(MathTestsClass.FloatProperty1)), new MemberExpression(nameof(MathTestsClass.FloatProperty2)));
            ConvertExpressionToFunctionAndCheckCondition<float>(expression, _instance.FloatProperty1 + _instance.FloatProperty2);          
        }

        #endregion

        #region Simpliefied test with Substract/Divide/Multiply against liited set of supported types
        [TestMethod]
        public void Divide_WithNullableDecimalAndDecimal_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new DivideExpression(new MemberExpression(nameof(MathTestsClass.NullableDecimal2)), new MemberExpression(nameof(MathTestsClass.DecimalProperty1)));
            ConvertExpressionToFunctionAndCheckCondition<decimal?>(expression, _instance.NullableDecimal2 / _instance.DecimalProperty1);          
        }

        [TestMethod]
        public void Divide_WithDoubleAndInt_UsingInternalCast_ProducesExpectedResult()
        {
            var expression = new DivideExpression(new MemberExpression(nameof(MathTestsClass.DoubleProperty2)), new MemberExpression(nameof(MathTestsClass.IntProperty1)));
            ConvertExpressionToFunctionAndCheckCondition<decimal?>(expression, _instance.DoubleProperty2 / _instance.IntProperty1);          
        }
        #endregion
    }
}