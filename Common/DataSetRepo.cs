namespace Common
{
    public class DataSetRepo(string sourceFolderNameForYear)
    {
        private const string SOLUTION_SOURCE_FOLDER_NAME = "AdventsOfCode";

        public string[] ReadDataSet(string dataSetFileName)
        {
            var fileContent = DataSetFileReader.ReadDataSetFile(SOLUTION_SOURCE_FOLDER_NAME, sourceFolderNameForYear, dataSetFileName);
            var lines = SplitLinesIntoArray(fileContent);
            return lines;
        }

        public static string[] SplitLinesIntoArray(string content)
        {
            string[] lines = content.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return lines;
        }
    }
}
