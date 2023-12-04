namespace AdventOfCode2023Solutions.Day03
{
    internal class EnginePart
    {
        private static int partIdCounter = 0;
        internal int PartId { get; private set; }
        internal int PartNumber { get; private set; }
        internal bool IsMissingPart { get; set; }
        internal bool IsAdjecantToSymbol { get; private set; }
        internal int LocationRow { get; private set; }
        internal int LocationColumnBegin { get; private set; }
        internal int LocationColumnEnd { get; private set; }

        private readonly Engine engineRef;

        public EnginePart(string partNumberString, Engine engineReference, int locationRow, int locationColumnBegin, int locationColumnEnd)
        {
            PartId = partIdCounter++;
            engineRef = engineReference;
            PartNumber = int.Parse(partNumberString);
            LocationRow = locationRow;
            LocationColumnBegin = locationColumnBegin;
            LocationColumnEnd = locationColumnEnd;
            IsAdjecantToSymbol = DetermineIfAdjecentToSymbol();
        }

        private bool DetermineIfAdjecentToSymbol()
        {
            return IsSymbolAdjacentRowAbove() || IsSymbolAdjacentSameRowLeft() || IsSymbolAdjacentSameRowRight() || IsSymbolAdjacentRowBelow();
        }

        private bool IsSymbolAdjacentSameRowLeft()
        {
            if (LocationColumnBegin == 0) return false;
            var row = LocationRow;
            var col = LocationColumnBegin - 1;
            return IsSymbol(row, col);
        }

        private bool IsSymbolAdjacentSameRowRight()
        {
            if (LocationColumnEnd + 1 == engineRef.Rows) return false;
            var row = LocationRow;
            var col = LocationColumnEnd + 1;
            return IsSymbol(row, col);
        }

        private bool IsSymbolAdjacentRowAbove()
        {
            if (LocationRow == 0) return false;

            int row = LocationRow - 1;
            int columnBegin = LocationColumnBegin == 0 ? 0 : LocationColumnBegin - 1;
            int columnEnd = LocationColumnEnd == engineRef.Columns - 1 ? LocationColumnEnd : LocationColumnEnd + 1;

            return IsSymbolInRange(row, columnBegin, columnEnd);
        }

        private bool IsSymbolAdjacentRowBelow()
        {
            if (LocationRow == engineRef.Rows - 1) return false;

            int row = LocationRow + 1;
            int columnBegin = LocationColumnBegin == 0 ? 0 : LocationColumnBegin - 1;
            int columnEnd = LocationColumnEnd == engineRef.Columns - 1 ? LocationColumnEnd : LocationColumnEnd + 1;

            return IsSymbolInRange(row, columnBegin, columnEnd);
        }

        private bool IsSymbolInRange(int row, int columnBegin, int columnEnd)
        {
            for (int column = columnBegin; column <= columnEnd; column++)
            {
                if (IsSymbol(row, column))
                    return true;
            }
            return false;
        }

        private bool IsSymbol(int row, int column)
        {
            var charToCheck = engineRef.EngineMap[row, column];
            var isSymbol = IsSymbol(charToCheck);
            
            if (isSymbol)
            {
                engineRef.AddSymbol(charToCheck, row, column, this);
            }

            return isSymbol;
        }

        private bool IsSymbol(char c)
        {
            if (char.IsDigit(c)) return false;
            if (c == '.') return false;
            if (c == ' ') return false;
            return true;
        }
    }
}
