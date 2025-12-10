using System.Globalization;
using System.Numerics;
using System.Text;

namespace ToolsFramework
{
    public class NumberTools
    {
        public static int[] StringToIntArray(string stringOfNumbers)
        {
            return stringOfNumbers.Select(x => int.Parse(x.ToString())).ToArray();
        }

        public static int[] StringOfNumbersToIntArray(string numberString)
        {
            var str = numberString.Trim();

            int[] intArray = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                intArray[i] = int.Parse(str.Substring(i, 1));
            }
            return intArray;
        }

        public static long CountSumOfEvery2nd(long[] array)
        {
            return array.Where((element, index) => index % 2 == 0).Sum(x => x);
        }

        public static long CountSumOfEvery2nd(int[] array)
        {
            return array.Where((element, index) => index % 2 == 0).Sum(x => x);
        }

        public static long CountSumOfNumberArray(long[] array)
        {
            return array.Sum();
        }

        public static long CountSumOfNumberArray(int[] array)
        {
            return array.Sum();
        }

        public static int RandomInt(int from, int to)
        {
            return new Random().Next(from, to);
        }

        public static int ModulusConverNumberIntoRange(int number, int from, int to)
        {
            return (number % to) + from;
        }

        public static long StringBuilderToLong(StringBuilder numberBuilder)
        {
            var numberStringRaw = numberBuilder.ToString();
            var numberString = numberStringRaw.Trim();
            if (numberString.Length == 0)
                return 0;
            var number = long.Parse(numberString);
            return number;
        }

        public static T MathOperateOnArray<T>(T[] values, MathOperationTypes opType) where T : INumber<T>
        {
            if (values.Length == 0) return T.Zero;

            return opType switch
            {
                MathOperationTypes.Multiply => MathMultiplyArray(values),
                MathOperationTypes.Addition => MathSummarizeArray(values),
                MathOperationTypes.Subtract => MathSubtractArrayByLeft(values),
                _ => throw new ArgumentException("Operation must be '+', '-', or '*'.", nameof(opType))
            };
        }

        public static T MathSummarizeArray<T>(T[] values) where T : INumber<T>
        {
            if (values.Length == 0) return T.Zero;

            T result = T.Zero;
            for (int i = 0; i < values.Length; i++)
                result += values[i];
            return result;
        }

        public static T MathSubtractArrayByLeft<T>(T[] values) where T : INumber<T>
        {
            if (values.Length == 0) return T.Zero;

            T result = values[0];
            for (int i = 1; i < values.Length; i++)
                result -= values[i];
            return result;
        }

        public static T MathMultiplyArray<T>(T[] values) where T : INumber<T>
        {
            if (values.Length == 0) return T.Zero;

            T result = values[0];
            for (int i = 1; i < values.Length; i++)
                result *= values[i];
            return result;
        }
    }

    public enum MathOperationTypes
    {
        None,
        Subtract,
        Addition,
        Multiply,
        Division,
    }


    public static class OperationExtensions
    {
        /// <summary>
        /// Converts a char ('+', '-', '*', '/') to an Operation enum.
        /// Throws ArgumentException for unsupported chars.
        /// </summary>
        public static MathOperationTypes FromChar(char op)
        {
            return op switch
            {
                '+' => MathOperationTypes.Addition,
                '-' => MathOperationTypes.Subtract,
                '*' => MathOperationTypes.Multiply,
                '/' => MathOperationTypes.Division,
                _ => throw new ArgumentException(
                           $"Unsupported operation character: '{op}'. Allowed: + - * /",
                           nameof(op))
            };
        }

        /// <summary>
        /// Tries to convert a char ('+', '-', '*', '/') to an Operation enum.
        /// Returns true if successful.
        /// </summary>
        public static bool TryFromChar(char op, out MathOperationTypes operation)
        {
            switch (op)
            {
                case '+':
                    operation = MathOperationTypes.Addition; return true;
                case '-':
                    operation = MathOperationTypes.Subtract; return true;
                case '*':
                    operation = MathOperationTypes.Multiply; return true;
                case '/':
                    operation = MathOperationTypes.Division; return true;
                default:
                    operation = MathOperationTypes.None; return false;
            }
        }

        /// <summary>
        /// Converts an Operation enum to its char representation.
        /// </summary>
        public static char ToChar(this MathOperationTypes operation)
        {
            return operation switch
            {
                MathOperationTypes.Addition => '+',
                MathOperationTypes.Subtract => '-',
                MathOperationTypes.Multiply => '*',
                MathOperationTypes.Division => '/',
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Unknown operation.")
            };
        }
    }

}
