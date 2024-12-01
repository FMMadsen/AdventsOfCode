namespace AdventOfCode2023Solutions.Day03
{
    internal class Gear
    {
        private EngineSymbol engineSymbolRef;
        private EnginePart enginePartReference1;
        private EnginePart enginePartReference2;

        public Gear(EngineSymbol engineSymbolReference)
        {
            engineSymbolRef = engineSymbolReference;
            enginePartReference1 = engineSymbolRef.EnginePartsReference[0];
            enginePartReference2 = engineSymbolRef.EnginePartsReference[1];
        }

        internal int GearRatio => enginePartReference1.PartNumber * enginePartReference2.PartNumber;
    }
}
