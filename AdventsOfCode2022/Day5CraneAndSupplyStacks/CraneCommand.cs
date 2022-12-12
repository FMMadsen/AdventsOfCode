namespace AdventsOfCode2022.Day5CraneAndSupplyStacks
{
    internal class CraneCommand
    {
        internal int MoveFrom = 0;
        internal int MoveTo = 0;
        internal int Repeats = 0;

        internal CraneCommand(int from, int to, int repeats)
        {
            MoveFrom = from;
            MoveTo = to;
            Repeats = repeats;
        }
    }
}
