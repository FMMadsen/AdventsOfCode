namespace AdventsOfCode2022
{
    internal interface ISolution
    {
        string DataSetFile { get; }
        string SolvePart1(string[] datasetLines, bool doPrintOut);
        string SolvePart2(string[] datasetLines, bool doPrintOut);
    }
}
