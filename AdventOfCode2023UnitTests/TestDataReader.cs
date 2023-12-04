using Common;

namespace AdventOfCode2023UnitTests
{
    internal static class TestDataReader
    {
        internal static string[] ReadDataSet(string fileName)
        {
            var projectRootDirectory = GetTestDataRootPath();
            var testFilePath = projectRootDirectory + fileName;
            var testData = File.ReadAllText(testFilePath);
            var testDataLines = DataSetRepo.SplitLinesIntoArray(testData);
            return testDataLines;
        }

        internal static string GetTestDataRootPath()
        {
            var executableDirectory = AppContext.BaseDirectory;
            var projectRootDirectory = executableDirectory;

            if (executableDirectory.Contains("bin"))
                projectRootDirectory = executableDirectory.Substring(0, executableDirectory.IndexOf("bin"));

            var datasetRootPath = projectRootDirectory + @"TestDataSets\";

            return datasetRootPath;
        }
    }
}
