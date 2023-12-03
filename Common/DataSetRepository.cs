namespace Common
{
    public class DataSetRepository(int year)
    {
        private const string DATASET_SOURCE_FOLDER_NAME = "AdventsOfCode";
        private readonly DataSetFileReader solutionFileReader = new(DATASET_SOURCE_FOLDER_NAME, year);

        public string[] GetDataSet(int day)
        {
            var fileContent = solutionFileReader.ReadDataSetFile(day);
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
