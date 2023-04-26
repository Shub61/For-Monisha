namespace Expressions
{
    public class NameAttribute: Attribute
    {
        public NameAttribute(String value){
            if(string.IsNullOrWhiteSpace(value)){
                throw new ArgumentException($"Incorrect attribute's value: {value ?? "null"}");
            }
            Value = value;            
        }

        public string Value { get; }
    }
}