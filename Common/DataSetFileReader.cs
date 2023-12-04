namespace Common
{
    internal static class DataSetFileReader
    {
        private const string DATASET_SOURCE_DIR_NAME_UC = "DATASETS";

        internal static string ReadDataSetFile(string solutionFolderName, string projectFolderNameForYear, string fileName)
        {
            var solutionFolderNameUC = solutionFolderName.ToUpper();

            var solutionRootDir = GetSolutionRootDir(solutionFolderNameUC);
            var projectDir = GetProjectDir(solutionRootDir, projectFolderNameForYear);
            var dataSetFolder = GetDataSetDirectory(projectDir);
            var dataSetFile = GetDataSetFile(dataSetFolder, fileName);
            var fileContent = ReadFile(dataSetFile);
            return fileContent;
        }

        private static DirectoryInfo GetSolutionRootDir(string sourceFolderNameUC)
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory.Parent != null)
            {
                if (directory.Name.ToUpper() == sourceFolderNameUC)
                    return directory;
                directory = directory.Parent;
            }

            throw new Exception("Solution root directory not found");
        }

        private static DirectoryInfo GetProjectDir(DirectoryInfo solutionRootDir, string projectFolderNameForYear)
        {
            try
            {
                var projectFolderNameForYearUC = projectFolderNameForYear.ToUpper();
                return solutionRootDir.GetDirectories().First(d => d.Name.ToUpper() == projectFolderNameForYearUC);
            }
            catch (Exception ex)
            {
                throw new Exception($"Project directory not found at location {solutionRootDir}", ex);
            }
        }

        private static DirectoryInfo GetDataSetDirectory(DirectoryInfo projectDir)
        {
            try
            {
                return projectDir.GetDirectories().First(d => d.Name.ToUpper() == DATASET_SOURCE_DIR_NAME_UC);
            }
            catch (Exception ex)
            {
                throw new Exception($"DataSets folder not found at location {projectDir}", ex);
            }
        }

        private static FileInfo GetDataSetFile(DirectoryInfo dataSetFolder, string fileName)
        {
            try
            {
                var fileNameUC = fileName.ToUpper();
                return dataSetFolder.GetFiles().First(f => f.Name.ToUpper() == fileNameUC);
            }
            catch (Exception ex)
            {
                throw new Exception($"DataSet file '{fileName}' not found at location {dataSetFolder}", ex);
            }
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
