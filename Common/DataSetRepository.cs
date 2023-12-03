namespace Common
{
    public class DataSetRepository
    {
        private SolutionFileReader solutionFileReader;
        private string DataSetFolderName = "DataSets";

        public DataSetRepository(string sourceFolderName, int year)
        {
            //solutionFileReader = new(sourceFolderName, year, DataSetFolderName);
            //DataSetFolderName = dataSetFolderName;
        }

        public string[] GetDataSet(int day)
        {
            var fileContent = solutionFileReader.ReadDataSetFile(day);
            var lines = SplitLinesIntoArray(fileContent);
            return lines;
        }

        private string[] SplitLinesIntoArray(string content)
        {
            string[] lines = content.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return lines;
        }
    }
}
