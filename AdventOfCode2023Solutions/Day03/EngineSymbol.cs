namespace AdventOfCode2023Solutions.Day03
{
    internal class EngineSymbol
    {
        internal char Symbol { get; set; }
        internal int Row { get; set; }
        internal int Column { get; set; }

        private IList<EnginePart> enginePartRefs = new List<EnginePart>();

        internal EngineSymbol(char symbol, int row, int column, EnginePart partReference)
        {
            Symbol = symbol;
            Row = row;
            Column = column;
            AddPartsReference(partReference);
        }

        internal void AddPartsReference(EnginePart partReference)
        {
            enginePartRefs.Add(partReference);
        }

        internal bool IsGeer()
        {
            return Symbol == '*' && enginePartRefs.Count == 2;
        }

        internal IList<EnginePart> EnginePartsReference => enginePartRefs;
    }
}
