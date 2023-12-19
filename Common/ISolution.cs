namespace Common
{
    public interface IAOCSolution
    {
        string PuzzleName { get; }
        string SolvePart1(string[] datasetLines);
        string SolvePart2(string[] datasetLines);
    }
}
