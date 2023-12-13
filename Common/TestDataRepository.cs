namespace Common
{
    public class TestDataRepository
    {
        public static string[] ReadDataSet(string fileName)
        {
            var projectRootDirectory = GetTestDataRootPath();
            var testFilePath = projectRootDirectory + fileName;
            var testDataLines = File.ReadAllLines(testFilePath);
            return testDataLines;
        }

        private static string GetTestDataRootPath()
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
