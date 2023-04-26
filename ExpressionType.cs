namespace Expressions
{
    [Flags]
    public enum ExpressionType
    {
        Bool = 0b1,
        String = 0b10,
        DateTime = 0b100,
        Numeric = 0b1000,

        Any = Bool | String | DateTime | Numeric,
    }
}