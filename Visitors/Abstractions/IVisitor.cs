using Expressions.Implementations;

namespace Expressions
{
    public interface IVisitor
    {
        void Visit(MemberExpression expression);
        void Visit(ConstExpression expression);
        void Visit(AreEqualExpression expression);
        void Visit(AreNotEqualExpression expression);
        void Visit(LessThanExpression expression);
        void Visit(LessThanOrEqualExpression expression);
        void Visit(GreaterThanExpression expression);
        void Visit(GreaterThanOrEqualExpression expression);
        void Visit(OrExpression expression);
        void Visit(AndExpression expression);
        void Visit(NotExpression expression);
        void Visit(AbsExpression expression);
        void Visit(SubtractExpression expression);
        void Visit(AddExpression expression);

        void Visit(DivideExpression expression);
        void Visit(MultiplyExpression expression);        
        void Visit(ModuloExpression expression);

        void Visit(LikeExpression expression);
        void Visit(PositiveSignExpression expression);
        void Visit(NegativeSignExpression expression);




    }
}