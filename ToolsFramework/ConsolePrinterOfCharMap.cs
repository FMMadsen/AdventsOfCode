namespace ToolsFramework
{
    public static class ConsolePrinterOfCharMap
    {
        private static Dictionary<char, ConsoleColor>? _mapElementColors = null;
        private static int nextColorSelectorIncrementer = 1;

        private static int GetNextColorSelectorIncrementer()
        {
            return NumberTools.ModulusConverNumberIntoRange(nextColorSelectorIncrementer++, 1, 14);
        }

        public static void PrintMapToConsole(char[,]? map, char emptyTile = ' ', bool setDifferentMapElementColors = true, bool printMapCompressed = false)
        {
            ArgumentNullException.ThrowIfNull(map);

            int cols = map.GetLength(0);
            int rows = map.GetLength(1);

            var currentForegroundColor = Console.ForegroundColor;
            var currentBackgroundColor = Console.BackgroundColor;

            if (setDifferentMapElementColors)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                _mapElementColors = [];
            }

            var mapSpacing = printMapCompressed ? "" : " ";

            Console.WriteLine();

            Console.Write("    ");
            for (int x = 0; x < cols; x++)
                if (x < 10)
                    Console.Write("{0}", x + mapSpacing);
                else
                    Console.Write(x);
            Console.WriteLine();

            for (int y = 0; y < rows; y++)
            {
                if (setDifferentMapElementColors)
                    Console.ForegroundColor = ConsoleColor.White;

                Console.Write(" {0,2} ", y);

                for (int x = 0; x < cols; x++)
                {
                    var currentTile = map[x, y];
                    currentTile = currentTile > 0 ? currentTile : emptyTile;

                    if (_mapElementColors != null)
                    {
                        if (_mapElementColors.TryGetValue(currentTile, out var color))
                            Console.ForegroundColor = color;
                        else
                        {
                            Console.ForegroundColor = (ConsoleColor)GetNextColorSelectorIncrementer();
                            _mapElementColors.Add(currentTile, Console.ForegroundColor);
                        }
                    }
                    Console.Write(currentTile + mapSpacing);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ForegroundColor = currentForegroundColor;
            Console.BackgroundColor = currentBackgroundColor;
        }

        public static void PrintMapToConsole(object[,] map, bool printMapCompressed = false)
        {
            int cols = map.GetLength(0);
            int rows = map.GetLength(1);

            var mapSpacing = printMapCompressed ? "" : " ";

            Console.WriteLine();

            Console.Write("    ");
            for (int x = 0; x < cols; x++)
                if (x < 10)
                    Console.Write("{0}", x + mapSpacing);
                else
                    Console.Write(x);
            Console.WriteLine();

            for (int y = 0; y < rows; y++)
            {
                Console.Write(" {0,2} ", y);

                for (int x = 0; x < cols; x++)
                {
                    var currentTile = map[x, y];
                    Console.Write(currentTile + mapSpacing);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
