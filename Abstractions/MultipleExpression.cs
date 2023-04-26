using Expressions.Helpers;
using System.Collections.Generic;

namespace Expressions.Abstractions
{
    public abstract class MultipleExpression : Expression, IMultipleExpression
    {
        protected MultipleExpression(ExpressionType type, param IExpression[] operands)
        : this(type, IReadOnlyCollection < IExpression > operands) { }
        
        protected MultipleExpression(ExpressionType type, IReadOnlyCollection<IExpression> operands) 
                        : base(type) 
        {
            if(operands == null)
            {
                throw new ArgumentNullException(nameof(operands));                
            }
            if(operands.Count <= 1)
            {
                throw new ArgumentException($"Not supported number of arguments: {operands.Count}. Must be greaterthan or eqaul to 2");
            }
            Operands = operands;
        }   
        
        public IReadOnlyCollection<IExpression> Operands {get;}        

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(obj, this))
            {
                return true;
            }

            return obj is MultipleExpression expression && ReferenceEquals(GetType(), expression.GetType())
                   && base.Equals(obj)                    
                   && Operands.SequenceEqual(expression.Operands);
        }

        public override int GetHashCode()
        {
            unchecked
            {   
                var hash = base.GetHashCode();
                foreach (var operand in Operands)
                {
                    hash = hash * ObjectHelper.HashCodeSeed + operand.GetHashCode();
                }            
                return hash;
            }            
        }

        public override string? ToString()
        {
            var separator = $" {GetExpressionName()} ";
            return $"({string.Join(separator, Operands.Select(o=> o.ToString()))})";
        }

        protected abstract string GetExpressionName();
    }
}