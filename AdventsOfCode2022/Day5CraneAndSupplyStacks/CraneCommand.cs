namespace AdventsOfCode2022.Day5CraneAndSupplyStacks
{
    internal class CraneCommand
    {
        internal int MoveFrom = 0;
        internal int MoveTo = 0;

        internal CraneCommand(int from, int to)
        {
            MoveFrom = from;
            MoveTo = to;
        }
    }
}
