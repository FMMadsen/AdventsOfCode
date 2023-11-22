namespace AdventsOfCode2022
{
    internal class DataSetRepository
    {
        internal string[] GetTestDataset(int day)
        {
            var datasetFileName = (day < 10) ? @$"Datasets\Day0{day}.txt" : @$"Datasets\Day{day}.txt";
            var datasetLines = GetDatasetByFileName(datasetFileName);
            return datasetLines;
        }

        internal string[] GetDataset(int day)
        {
            var datasetFileName = (day < 10) ? @$"Datasets\Day0{day}.txt" : @$"Datasets\Day{day}.txt";
            var datasetLines = GetDatasetByFileName(datasetFileName);
            return datasetLines;
        }

        internal string[] GetDatasetByFileName(string datasetFileName)
        {
            var datasetFile = @$"Datasets\{datasetFileName}";
            var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(datasetFile);
            return datasetLines;
        }

        internal string[] GetDatasetByFilePath(string datasetFile)
        {
            var datasetLines = MyFileReader.ReadFileIntoLineArrayFromCurrentFolder(datasetFile);
            return datasetLines;
        }
    }
}
