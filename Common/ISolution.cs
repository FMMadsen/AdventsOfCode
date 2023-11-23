namespace Common
{
    public interface IAOCSolution
    {
        //int Day { get; }
        string PuzzleName { get; }
        //string DataSetFile { get; }

        string SolvePart1();
        string SolvePart2();

        //string SolvePart1(string[] datasetLines, bool doPrintOut); 
        //string SolvePart2(string[] datasetLines, bool doPrintOut);
    }
}
