namespace Common
{
    public class DataSetFileReader
    {
        private const string dataSetFolderName = "DataSets";
        private readonly string sourceFolderNameUC;
        private readonly int year;

        public DataSetFileReader(string sourceFolderName, int year)
        {
            sourceFolderNameUC = sourceFolderName.ToUpper();
            this.year = year;
        }

        public string ReadDataSetFile(int day)
        {
            var solutionRootPath = GetSolutionRootPath();
            var dataSetFolder = GetDataSetFolder(solutionRootPath);
            var yearFolder = GetDataSetYearFolder(dataSetFolder);
            var dataSetFile = GetDataSetFile(yearFolder, day);
            var fileContent = ReadFile(dataSetFile);
            return fileContent;
        }

        private DirectoryInfo GetSolutionRootPath()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory.Parent != null)
            {
                if (directory.Name.ToUpper() == sourceFolderNameUC)
                    return directory;
                directory = directory.Parent;
            }

            throw new Exception("Solution root folder not found");
        }

        private DirectoryInfo GetDataSetFolder(DirectoryInfo solutionRootFolder)
        {
            try
            {
                var dataSetFolderNameUC = dataSetFolderName.ToUpper();
                return solutionRootFolder.GetDirectories().First(d => d.Name.ToUpper() == dataSetFolderNameUC);
            }
            catch (Exception ex)
            {
                throw new Exception($"DataSets folder not found at location {solutionRootFolder}", ex);
            }
        }

        private DirectoryInfo GetDataSetYearFolder(DirectoryInfo dataSetFolder)
        {
            try
            {
                var yearFolderName = year.ToString();
                return dataSetFolder.GetDirectories().First(d => d.Name == yearFolderName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Year folder {year} not found at location {dataSetFolder}");
            }
        }

        private FileInfo GetDataSetFile(DirectoryInfo yearFolder, int day)
        {
            string dayFileNameBegin = day < 10 ? $"Day0{day}" : $"Day{day}";
            return yearFolder.GetFiles().First(f => f.Name.StartsWith(dayFileNameBegin));
        }

        private static string ReadFile(FileInfo file)
        {
            try
            {
                return File.ReadAllText(file.FullName);
            }
            catch (Exception ex)
            {
                throw new Exception($"DataSet file not found: {file.FullName}", ex);
            }
        }
    }
}
