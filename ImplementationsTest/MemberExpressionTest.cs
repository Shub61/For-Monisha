using Expressions.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressions.ImplementationsTests
{
    [TestClass]
    public class MemberExpressionTest
    {
        
        private static readonly ExpressionType DefaultPropertyType = ExpressionType.Any;
        private const string DefaultPropertyName = "Property1";
        private const string DefaultVariableName = "Instance1";
       
        [TestMethod]

        public void NameAndTypeProperties_ReturnCorrectValues_SuppliedConstructor()
        {
            var expression = new MemberExpression(DefaultPropertyName);
            Assert.AreEqual(expression.ResultType, DefaultPropertyType);            
            Assert.AreEqual(expression.Name, DefaultPropertyName);        
        }

        [TestMethod]
        public void VariableName_ReturnsDefaultValue_IfNotSuppliedInConstructor()
        {
            var expression = new MemberExpression(DefaultPropertyName);            
            Assert.AreEqual(expression.VariableName, MemberExpression.DefaultVariableName);        
        }

        [TestMethod]        
        public void VariableName_ReturnsCorrectValue_IfSuppliedInConstructor()
        {
            var expression = new MemberExpression(DefaultPropertyName, DefaultVariableName);
            Assert.AreEqual(expression.VariableName, DefaultVariableName);            
        }
        
        [TestMethod]
        public void Equals_ReturnsTrue_If_Type_And_Name_And_VariableName_AreTheSame()
        {
            var expression1 = new MemberExpression(DefaultPropertyName, DefaultVariableName);
            var expression2 = new ConstExpression(DefaultPropertyName, DefaultVariableName);
            Assert.AreEqual(expression1, expression2);            
        }
        
        public void Equals_ReturnsTrue_If_Type_And_Name_AreTheSame()
        {
            var expression1 = new MemberExpression(DefaultPropertyName);
            var expression2 = new MemberExpression(DefaultPropertyName);
            Assert.AreEqual(expression1, expression2);            
        }

        public void GetHashCode_IsTheSame_If_Type_And_Name_And_VariableName_AreTheSame()
        {
            var expression1 = new MemberExpression(DefaultPropertyName, DefaultVariableName);
            var expression2 = new ConstExpression(DefaultPropertyName, DefaultVariableName);
            Assert.AreEqual(expression1.GetHashCode(), expression2.GetHashCode());            
        }

        public void GetHashCode_IsTheSame_If_Type_And_Name_AreTheSame()
        {
            var expression1 = new MemberExpression(DefaultPropertyName);
            var expression2 = new ConstExpression(DefaultPropertyName);
            Assert.AreEqual(expression1.GetHashCode(), expression2.GetHashCode());            
        }
    }
}