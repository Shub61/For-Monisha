using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Expressions.SerializationTest
{
    [TestClass]
    public class JsonSerializationTest
    {
        //[Test]
        public void Expression_ToString_Test()
        {
            var expression = Expression.AreEqual
            (
                Expression.Member("TestProperty"),
                Expression.Const("TestStringValue")
            );

            var settings = new JsonSerializerSettings
            {
                SerializationBinder = new TypeNameSerializationBinder(),
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                //Formatting = Formatting.Indented,
                //ContractResolver = new CamelCasePropertyNameContractResolver()

            };

            string output = JsonConvert.SerializeObject(expression, settings);
            string deserializedExpression = (IExpression) JsonConvert.DeserializeObject(output, settings);

            Assert.AreEqual(expression, deserializedExpression);
        }

        //[Test]
        public void String_ToExpression_Test()
        {
            var text = "{\"$type\": \"AreEqualExpression\", \"ResultType\":1, \"LeftOperand\":{\"$type\": \"MemberExpression\", \"Name\": \"TestProperty\"}, \"RightOperand\":{\"$type\": \"ConstExpression\", \"Value\": \"TestStringValue\"}}"; //{\"$type\": \"AreEqualExpression\", \"ResultType\":1, \"LeftOperand\":{\"$type\": \"MemberExpression\", \"Name\": \"TestProperty\", \"ResultType\":15}, \"RightOperand\":{\"$type\": \"ConstExpression\", \"Value\": \"TestStringValue\", \"ResultType\":15}}";
            var settings = new JsonSerializerSettings
            {
                SerializationBinder = new TypeNameSerializationBinder(),
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore,
                //Formatting = Formatting.Indented,
                //ContractResolver = new CamelCasePropertyNameContractResolver()
            };
            string expression = (IExpression) JsonConvert.DeserializeObject(text, settings);
        }
    }
}