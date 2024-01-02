namespace AdventOfCode2023Solutions.Day05
{
    internal class SeedRange(long from, long range)
    {
        internal long From { get; set; } = from;
        internal long To { get; set; } = from + range - 1;
    }
}
