using Expressions.Implementations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Expressions.Converters
{
    public class AreEqualExpressionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var areEqualExpression = value as AreEqualExpression;
            if(areEqualExpression != null)
            {
                writer.WriteValue(value.ToString());
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            string s = (string) reader.Value;
            return new Version(s);
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType) => ReferenceEquals(objectType, typeof(AreEqualExpression));
        
    }
}