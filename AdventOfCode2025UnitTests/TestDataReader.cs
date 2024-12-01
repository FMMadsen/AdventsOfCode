using Common;

namespace AdventOfCode2025UnitTests
{
    internal static class TestDataReader
    {
        internal static string[] ReadDataSet(string fileName)
        {
            return TestDataRepository.ReadDataSet(fileName);
        }
    }
}
