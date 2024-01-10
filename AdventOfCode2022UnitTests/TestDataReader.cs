using Common;

namespace AdventOfCode2022UnitTests
{
    internal static class TestDataReader
    {
        internal static string[] ReadDataSet(string fileName)
        {
            return TestDataRepository.ReadDataSet(fileName);
        }
    }
}
