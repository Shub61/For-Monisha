using Expressions.Abstractions;
namespace Expressions.Parsers
{
    public interface IParserConfiguration
    {
        string GetToken(ExpressionKind kind);
        ExpressionKind? GetKind(string value);
        int GetPriority(ExpressionKind kind);
        bool IsFuntion(ExpressionKind kind);
    }
}