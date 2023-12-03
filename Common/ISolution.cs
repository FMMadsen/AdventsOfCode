namespace Common
{
    public interface IAOCSolution
    {
        string PuzzleName { get; }
        string SolvePart1();
        string SolvePart2();
    }
}
