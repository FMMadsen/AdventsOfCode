using System.Globalization;

namespace ToolsFramework
{
    public static class ConsolePrinterGridColumnRow
    {
        /// <summary>
        /// Prints a 2D array (grid) of generic values to the console with aligned columns.
        /// This overload assumes the grid is indexed as [column, row], i.e., grid[col, row].
        /// Columns are spaced by a single space. Each column's width is the maximum width
        /// of the string representation of its cells in that column.
        /// </summary>
        /// <typeparam name="T">Element type of the grid.</typeparam>
        /// <param name="grid">The 2D array to print, indexed as [column, row].</param>
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
        public static void Print<T>(T[,] grid, string? format = null, IFormatProvider? provider = null, bool leftAlign = true)
        {
            ArgumentNullException.ThrowIfNull(grid);

            provider ??= CultureInfo.InvariantCulture;

            // IMPORTANT: first dimension is columns, second is rows
            int cols = grid.GetLength(0);
            int rows = grid.GetLength(1);

            if (rows == 0 || cols == 0)
            {
                Console.WriteLine("(empty grid)");
                return;
            }

            // Convert all cells to strings once and measure column widths.
            // We'll store strings in [row, col] order for straightforward printing.
            var strings = new string[rows, cols];
            var widths = new int[cols];

            for (int c = 0; c < cols; c++)
            {
                int maxWidth = 0;
                for (int r = 0; r < rows; r++)
                {
                    string s = ToStringValue(grid[c, r], format, provider);
                    strings[r, c] = s;
                    if (s.Length > maxWidth) maxWidth = s.Length;
                }
                widths[c] = maxWidth;
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

            if (value is IFormattable f)
                return f.ToString(format, provider) ?? string.Empty;

            return value.ToString() ?? string.Empty;
        }
    }
}