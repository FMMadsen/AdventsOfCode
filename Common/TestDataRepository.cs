using System.Text;

namespace Common
{
    public class TestDataRepository
    {
        public static string[] ReadDataSet(string fileName)
        {
            var testDataSetFilePath = GetTestDataRootPath() + fileName;
            return File.ReadAllLines(testDataSetFilePath);
        }

        public static void WriteDataSet(char[,] dataSet, string fileName)
        {
            var filePath = GetOutputDataRootPath() + fileName;

            using (var file = File.Open(filePath, FileMode.CreateNew))
            using (var stream = new StreamWriter(file))
            {
                var sb = new StringBuilder();
                for (int y = 0; y <= dataSet.GetUpperBound(0); y++)
                {
                    sb.Clear();
                    for (int x = 0; x <= dataSet.GetUpperBound(1); x++)
                        sb.Append((dataSet[y, x]));
                    stream.WriteLine(sb.ToString());
                }
            }
        }

        private static string GetTestDataRootPath()
        {
            return GetProjectRootDirectory() + @"TestDataSets\";
        }

        private static string GetOutputDataRootPath()
        {
            return GetProjectRootDirectory() + @"OutputData\";
        }

        private static string GetProjectRootDirectory()
        {
            var executableDirectory = AppContext.BaseDirectory;
            var projectRootDirectory = executableDirectory;

            if (executableDirectory.Contains("bin"))
                projectRootDirectory = executableDirectory.Substring(0, executableDirectory.IndexOf("bin"));

            return projectRootDirectory;
        }
    }
}
