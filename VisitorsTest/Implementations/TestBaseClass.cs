using Expressions.Visitors.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions.VisitorsTest.Implementations
{
    public class TestBaseClass
    {
        protected MathTestsClass _instance;
        protected PredicateVisitor<MathTestsClass> _visitor;

        [TestInitialize]
        public void SetUp()
        {
            _instance = new MathTestsClass
            {
                // ByteProperty1 = (byte)1,
                // ByteProperty1 = (byte)2,
                // NullableByte1 = (byte)1,
                // NullableByte2 = (byte)2,

                IntProperty1 = 1,
                IntProperty2 = 2,
                NullableInt1 = 1,
                NullableInt2 = 2,

                LongProperty1 = 1,
                LongProperty2 = 2,
                NullableLong1 = 1,
                NullableLong2 = 2,

                DecimalProperty1 = 1.5m,
                DecimalProperty2 = 4.5m,
                NullableDecimal1 = 1.5m,
                NullableDecimal2 = 4.5m,

                FloatProperty1 = 1.5f,
                FloatProperty2 = 4.5f,
                NullableFloat1 = 1.5f,
                NullableFloat2 = 4.5f,

                DoubleProperty1 = 1.5d,
                DoubleProperty2 = 4.5d,
                NullableDouble1 = 1.5d,
                NullableDouble2 = 4.5d,

                StringProperty1 = "1",
                StringProperty2 = "2",
            };

            _visitor = new PredicateVisitor<MathTestsClass>();
        }

        protected void AssertIsTrue(IExpression expression)
        {
            expression.Accept(_visitor);
            var predicate = _visitor.Get();
            Assert.IsTrue(predicate(_instance));
        }

        protected void AssertIsFalse(IExpression expression)
        {
            expression.Accept(_visitor);
            var predicate = _visitor.Get();
            Assert.IsFalse(predicate(_instance));
        }

        protected void ConvertExpressionToFunctionAndCheckCondition(IExpression expression, object result)
        {
            expression.Accept(_visitor);
            var func = System.Linq.Expressions.Expression.Lambda<Func<MathTestsClass, T>>(_visitor.Current, _visitor.Parameter).Compile();
            Assert.AreEqual(func(_instance), result);
        }

        protected object UpliftToNullable(object value)
        {
            var nullableType = typeof(Nullable<>).MakeGenericType(value.GetType());
            var constructorInfo = nullableType.GetConstructor(new [] {value.GetType()});
            return constructorInfo.Invoke(new [] {value});
        
        
        public class TestMainClass
        {
            public TestSubClass MainProperty {get; set;}
            
            public class TestSubClass 
            {
                public int IntSubProperty {get; set;}
            }
        }
    }
}