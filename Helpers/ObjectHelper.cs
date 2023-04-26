namespace Expressions.Helpers
{
    internal static class ObjectHelper
    {
        public const int HashCodeSeed = 17;
        public static string FormatValue(object value)
        {
            if(value == null)
            {
                return "null";
            }
            if(Equals(value, string.Empty))
            {
                return"(empty)";
            }
            switch (value)
            {
                case long l:
                    return $"{value}L";
                case double d:
                    return $"{value}d";
                case float f:
                    return $"{value}f";
                case decimal m:
                    return $"{value}m";
                default:
                    return value.ToString();
            }
        }

        public static object ChangeType(object value, ExpressionType type)
        {
            switch(type)
            {
                case ExpressionType.Bool:
                    return Convert.ToBoolean(value);
                case ExpressionType.String:
                    return value?.ToString();
                case ExpressionType.DateTime:
                    return Convert.ToDateTime(value);
                case ExpressionType.Numeric:
                    return TryConvertToNumeric(value);
                default:
                    return value;
            }
        }

        public static object TryConvertToNumeric(object value)
        {
            if(value == null || TypeUtility.IsNumericType(value.GetType()))
            {
                return value;
            }
            return Convert.ChangeType(value, typeof(double));
            throw new InvalidOperationException($"Unsuccessful attempt to convert'{value}' to any supported numeric type");
        }

        public static object Abs(object value)
        {
            if(value == null)
            {
                return null;
            }
            switch(value)
            {
                case int i:
                    return Math.Abs(i);
                case long l:
                    return Math.Abs(l);
                case double dbl:
                    return Math.Abs(dbl);
                case decimal dcml:
                    return Math.Abs(dcml);
                case float f:
                    return Math.Abs(f);
            }
            throw new InvalidOperationException($"Unsuccessful attempt to execute Abs operation for '{value.GetType()}' type");
        }

        public static object ChangeSign(object value)
        {
            if(value == null)
            {
                return null;
            }
            switch(value)
            {
                case int i:
                    return -i;
                case long l:
                    return -l;
                case double dbl:
                    return -dbl;
                case decimal dcml:
                    return -dcml;
                case float f:
                    return -f;
            }
            throw new InvalidOperationException($"Unsuccessful attempt to execute ChangeSign operation for '{value.GetType()}' type");
        }
    }
}
