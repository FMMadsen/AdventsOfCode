using System.Globalization;

namespace ToolsFramework
{
    public static class ConsolePrinterGridRowColumn
    {
        /// <summary>
        /// Prints a 2D array (grid) of generic values to the console with aligned columns.
        /// Columns are spaced by a single space. Each column's width is the maximum width
        /// of the string representation of its cells.
        /// </summary>
        /// <typeparam name="T">Element type of the grid.</typeparam>
        /// <param name="grid">The 2D array to print.</param>
        /// <param name="format">
        /// Optional format string used for values that implement IFormattable (e.g., numbers, dates).
        /// Ignored for other types.
        /// </param>
        /// <param name="provider">
        /// Optional culture/provider for formatting (defaults to <see cref="CultureInfo.InvariantCulture"/>).
        /// </param>
        /// <param name="leftAlign">
        /// If true, columns are left-aligned; otherwise right-aligned.
        /// </param>
        public static void Print<T>(T[,] grid, string? format = null, IFormatProvider? provider = null, bool leftAlign = false)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));

            provider ??= CultureInfo.InvariantCulture;

            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            if (rows == 0 || cols == 0)
            {
                Console.WriteLine("(empty grid)");
                return;
            }

            // Convert all cells to strings once and measure column widths.
            var strings = new string[rows, cols];
            var widths = new int[cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string s = ToStringValue(grid[r, c], format, provider);
                    strings[r, c] = s;
                    if (s.Length > widths[c]) widths[c] = s.Length;
                }
            }

            // Print row by row with padding per column.
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string s = strings[r, c];
                    int width = widths[c];

                    // pad to column width
                    string padded = leftAlign ? s.PadRight(width) : s.PadLeft(width);

                    // single space between columns (except after last column)
                    if (c < cols - 1) Console.Write(padded + " ");
                    else Console.Write(padded);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Converts a value to string with optional formatting if it implements IFormattable.
        /// Returns "null" for null references.
        /// </summary>
        private static string ToStringValue<T>(T value, string? format, IFormatProvider provider)
        {
            if (value is null) return "null";

            // Use IFormattable if available (numbers, DateTime, etc.)
            if (value is IFormattable f)
                return f.ToString(format, provider) ?? string.Empty;

            // Fall back to ToString()
            return value.ToString() ?? string.Empty;
        }
    }
}