using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day10
{
    public class LimitedValue<T>
    {
        private ConstantExpression MinimumExpressionValue;
        private ConstantExpression MaximumExpressionValue;
        private T MinimumValue
        {
            get
            {
                return (T)MinimumExpressionValue.Value;
            }
            set
            {
                MinimumExpressionValue = Expression.Constant(value);
            }
        }
        private T MaximumValue
        {
            get
            {
                return (T)MaximumExpressionValue.Value;
            }
            set
            {
                MaximumExpressionValue = Expression.Constant(value);
            }
        }
        private T TheValue;

        public T Minimum { get { return MinimumValue; } }
        public T Maximum {  get { return MaximumValue; } }
        public T Value { 
            get
            {
                return TheValue;
            } 
            set
            {
                try
                {
                    if (Expression.Lambda<Func<bool>>(Expression.NotEqual(MaximumExpressionValue, MinimumExpressionValue)).Compile()())
                    {
                        ConstantExpression temp = Expression.Constant(value);

                        if (Expression.Lambda<Func<bool>>(Expression.LessThan(temp, MinimumExpressionValue)).Compile()())
                        {
                            TheValue = MinimumValue;
                        }
                        else if (Expression.Lambda<Func<bool>>(Expression.LessThan(MaximumExpressionValue, temp)).Compile()())
                        {
                            TheValue = MaximumValue;
                        }
                        else 
                        {  TheValue = value; }
                    }
                    else
                    { TheValue = value; }
                }
                catch 
                { TheValue = value; }
            } 
        }

        public LimitedValue(T aValue = default, T minimum = default, T maximum = default)
        {
            MinimumValue = minimum;
            MaximumValue = maximum;
            TheValue = aValue;
        }


    }
}
