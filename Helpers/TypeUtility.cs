namespace Expressions.Helpers
{
    public static class TypeUtility
    {
        private static List<Type> RankedNumericTypes = new List<Type>
        {
            typeof(int),
            typeof(long),
            typeof(double),
            typeof(float),
            typeof(decimal),
        };

        private static readonly HashSet<Type> SupportedNumericTypes;
        private static readonly HashSet<Type> SupportedNumericNullableTypes;
        private static readonly HashSet<Type> SupportedNonNumericTypes;
        private static readonly HashSet<Type> SupportedTypes;
        private static readonly HashSet<Type> SupportedNullableTypes;
        private static readonly HashSet<Type> AllSupportedTypes;

        static TypeUtility()
        {
            SupportedNumericTypes = new HashSet<Type>(RankedNumericTypes);
            SupportedNumericNullableTypes = new HashSet<Type>(SupportedNumericTypes.Select(t=> typeof(Nullable<>).MakeGenericType(t)));
            SupportedNonNumericTypes = new HashSet<Type> {typeof(DateTime), typeof(bool), typeof(string)};
            SupportedTypes = new HashSet<Type>(SupportedNumericTypes.Concat(new List<Type>{typeof(DateTime), typeof(bool)}));
            SupportedNullableTypes = new HashSet<Type>(SupportedTypes.Select(t=> typeof(Nullable<>).MakeGenericType(t)));
            AllSupportedTypes = new HashSet<Type>
            (
                SupportedNumericTypes.Concat(SupportedNumericNullableTypes)
                .Concat(SupportedTypes)
                .Concat(SupportedNullableTypes)
                .Concat(new List<Type> {typeof(string)})
            );
        }
        
        public static IReadOnlyCollection<Type> NumericTypes => RankedNumericTypes;

        public static Type FindNumericCommonType(Type right, Type left)
        {
            if(!IsNumericType(right) && !IsNumericType(left))
            {
                throw new ArgumentException($"Both operands are not of numeric type. Right operand: {right}, left operand: {left}");
            }
            if(ReferenceEquals(right, left))
            {
                return GetCurrentOrBaseType(right);
            }
            if(IsNumericType(right) && IsNumericType(left))
            {
                var numericRight = GetCurrentOrBaseType(right);
                var numericLeft = GetCurrentOrBaseType(left);
                var indexRight = RankedNumericTypes.IndexOf(numericRight);
                var indexLeft = RankedNumericTypes.IndexOf(numericLeft);
                return indexRight >= indexLeft ? numericRight : numericLeft;
            }
            if(IsNumericType(right))
            {
                return GetCurrentOrBaseType(right);
            }
            return GetCurrentOrBaseType(left);
        }

        public static bool IsNumericType (Type type)
        {
            return SupportedNumericTypes.Contains(type) || SupportedNumericNullableTypes.Contains(type);
        }

        public static bool IsNumericNullableType (Type type)
        {
            return SupportedNumericNullableTypes.Contains(type);
        }

        public static bool IsNumericOrNumericNullableType(Type type)
        {
            return IsNumericType(type) || IsNumericNullableType(type);
        }

        public static bool IsSupportedType(Type type)
        {
           return AllSupportedTypes.Contains(type); 
        }
        
        public static bool IsNonNumericType(Type type)
        {
           return SupportedNonNumericTypes.Contains(type); 
        }

        public static Type GetCurrentOrBaseType(Type type)
        {
            return SupportedNullableTypes.Contains(type) ? type.GenericTypeArguments.FirstOrDefault(): type;
        }
    }
}