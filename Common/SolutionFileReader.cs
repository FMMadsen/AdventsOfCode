namespace Common
{
    public class SolutionFileReader
    {
        private readonly string sourceFolderNameUC;
        private readonly string dataSetFolderNameUC;
        private readonly int year;

        public SolutionFileReader(string sourceFolderName, int year, string dataSetFolderName)
        {
            dataSetFolderNameUC = dataSetFolderName.ToUpper();
            sourceFolderNameUC = sourceFolderName.ToUpper();
            this.year = year;
        }

        public string ReadDataSetFile(int day)
        {
            var solutionRootPath = GetSolutionRootPath();
            var dataSetFileRelativePath = ConstructDataSetRelativePath(day);
            var fileFullPath = Path.Combine(solutionRootPath + dataSetFileRelativePath);
            var fileContent = ReadFile(fileFullPath);
            return fileContent;
        }

        private string ConstructDataSetRelativePath(int day)
        {
            return @"Datasets\2022\Day01ElvesInventoryList.txt";
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
            solutionRootFolder.GetDirectories().First(d => d.Name.ToUpper() == "DATASETS");

            throw new NotImplementedException();
            //if()

            //if (solutionRootFolder.GetDirectories().F)
        }

        //public static string[] ReadFileIntoLineArrayFromCurrentFolder(string filename)
        //{
        //    var fileContent = ReadFileFromCurrentFolder(filename);
        //    var lines = SplitLinesIntoArray(fileContent);
        //    return lines;
        //}

        //public static string ReadFileFromCurrentFolder(string filename)
        //{
        //    var contentRootPath = GetContentRootPath();
        //    var fileFullPath = contentRootPath + filename;

        //    var fileContent = ReadFile(fileFullPath);
        //    return fileContent;
        //}

        //private static string[] SplitLinesIntoArray(string content)
        //{
        //    string[] lines = content.Split(
        //        new string[] { Environment.NewLine },
        //        StringSplitOptions.None
        //    );
        //    return lines;
        //}

        //private static string GetContentRootPath()
        //{
        //    var appRootDirectory = AppContext.BaseDirectory;

        //    if (appRootDirectory.Contains("bin"))
        //    {
        //        appRootDirectory = appRootDirectory.Substring(0, appRootDirectory.IndexOf("bin"));
        //    }

        //    return appRootDirectory;
        //}

        private static string ReadFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var filecontent = File.ReadAllText(fileFullPath);
                return filecontent;
            }
            else
            {
                throw new Exception("File not found: " + fileFullPath);
            }
        }
    }
}
