using Common;

namespace AdventOfCode2023UnitTests
{
    internal static class TestDataWriter
    {
        internal static void ReadDataSet(char[,] dataSet, string fileName)
        {
            TestDataRepository.WriteDataSet(dataSet, fileName);
        }
    }
}
