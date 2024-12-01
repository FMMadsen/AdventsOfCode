namespace AdventOfCode2023Solutions.Day03
{
    internal class Engine
    {
        internal string[] EngineLines { get; private set; }
        internal char[,] EngineMap { get; private set; }
        internal int Rows { get; private set; }
        internal int Columns { get; private set; }
        internal IList<EnginePart> EngineParts { get; private set; }
        internal IList<EngineSymbol> EngineSymbols { get; private set; }

        public Engine(string[] engineLines)
        {
            EngineSymbols = new List<EngineSymbol>();
            EngineLines = engineLines;
            Columns = engineLines[0].Length;
            Rows = engineLines.Length;
            EngineMap = CreateEngineMap(engineLines);
            EngineParts = IdentifyEngineParts();
        }

        internal void AddSymbol(char symbol, int row, int column, EnginePart enginePartReference)
        {
            var existingSymbol = EngineSymbols.FirstOrDefault(s => s.Row == row && s.Column == column);
            if (existingSymbol != null)
            {
                existingSymbol.AddPartsReference(enginePartReference);
            }
            else
            {
                var newEngineSymbol = new EngineSymbol(symbol, row, column, enginePartReference);
                EngineSymbols.Add(newEngineSymbol);
            }
        }

        private char[,] CreateEngineMap(string[] engineLines)
        {
            var engineMap = new char[Rows, Columns];

            for (int r = 0; r < Rows; r++)
            {
                var rowContent = engineLines[r].ToArray();
                for (int c = 0; c < Columns; c++)
                {
                    engineMap[r, c] = rowContent[c];
                }
            }
            return engineMap;
        }

        private IList<EnginePart> IdentifyEngineParts()
        {
            var engineParts = new List<EnginePart>();

            int numberIndexBegin;
            int numberIndexEnd;

            for (int r = 0; r < Rows; r++)
            {
                numberIndexBegin = -1;

                for (int c = 0; c < Columns; c++)
                {
                    var isDigit = char.IsDigit(EngineMap[r, c]);
                    var isLastColumn = c == Columns - 1;

                    if (isDigit && !isLastColumn)
                    {
                        if (numberIndexBegin == -1)
                        {
                            numberIndexBegin = c;
                        }
                    }
                    else if (isDigit && isLastColumn)
                    {
                        if (numberIndexBegin != -1)
                        {
                            numberIndexEnd = c;
                            engineParts.Add(CreateEnginePart(r, numberIndexBegin, numberIndexEnd));
                            numberIndexBegin = -1;
                        }
                        else
                        {
                            numberIndexBegin = c;
                            numberIndexEnd = c;
                            engineParts.Add(CreateEnginePart(r, numberIndexBegin, numberIndexEnd));
                            numberIndexBegin = -1;
                        }
                    }
                    else
                    {
                        if (numberIndexBegin != -1)
                        {
                            numberIndexEnd = c - 1;
                            engineParts.Add(CreateEnginePart(r, numberIndexBegin, numberIndexEnd));
                            numberIndexBegin = -1;
                        }
                    }
                }
            }

            return engineParts;
        }

        private EnginePart CreateEnginePart(int row, int columnBegin, int columnEnd)
        {
            var partNumberString = EngineLines[row].Substring(columnBegin, columnEnd - columnBegin + 1);
            var enginePart = new EnginePart(partNumberString, this, row, columnBegin, columnEnd);
            enginePart.IsMissingPart = enginePart.IsAdjecantToSymbol;
            return enginePart;
        }
    }
}
