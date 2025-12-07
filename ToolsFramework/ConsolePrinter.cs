namespace ToolsFramework
{
    public static class ConsolePrinter
    {
        public static void Print<T>(T[] values)
        {
            Console.WriteLine(string.Join(" ", values));
        }

        /// <summary>
        /// Writes all elements of <paramref name="items"/> to the console separated by <paramref name="spacer"/>,
        /// printing each element in a different color by cycling the provided <paramref name="palette"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements to print.</typeparam>
        /// <param name="items">The sequence of items to print.</param>
        /// <param name="spacer">The separator printed between elements (e.g., " ", ", ", " | ").</param>
        /// <param name="palette">
        /// The list of colors to cycle through; if null or empty, a default palette is used.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="items"/> is null.</exception>
        public static void WriteColored<T>(
            IEnumerable<T> items,
            string spacer = " ",
            ConsoleColor[]? palette = null)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));
            spacer ??= " ";

            var list = items as IList<T> ?? items.ToList();
            if (list.Count == 0)
            {
                // nothing to print—do nothing (or Console.WriteLine(); if you prefer a newline)
                return;
            }

            var colors = (palette is null || palette.Count() == 0)
                ? DefaultPalette
                : palette;

            var originalColor = Console.ForegroundColor;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    // choose color by cycling the palette
                    Console.ForegroundColor = colors[i % colors.Count()];

                    string text = list[i]?.ToString() ?? "null";
                    Console.Write(text);

                    // spacer between elements (not after the last one)
                    if (i < list.Count - 1)
                    {
                        Console.ForegroundColor = originalColor; // spacers in default color
                        Console.Write(spacer);
                    }
                }
                Console.WriteLine(); // end the line
            }
            finally
            {
                Console.ForegroundColor = originalColor; // always restore
            }
        }

        /// <summary>
        /// A compact default palette of readable colors
        /// </summary>
        private static readonly ConsoleColor[] DefaultPalette =
        [
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Magenta,
            ConsoleColor.Blue,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkBlue
        ];
    }
}