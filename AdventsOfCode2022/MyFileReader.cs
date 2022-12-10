namespace AdventsOfCode2022
{
    public class MyFileReader
    {
        public static string[] ReadFileIntoLineArrayFromCurrentFolder(string filename)
        {
            var fileContent = ReadFileFromCurrentFolder(filename);
            var lines = SplitLinesIntoArray(fileContent);
            return lines;
        }

        public static string ReadFileFromCurrentFolder(string filename)
        {
            var contentRootPath = GetContentRootPath();
            var fileFullPath = contentRootPath + filename;

            var fileContent = ReadFile(fileFullPath);
            return fileContent;
        }

        private static string[] SplitLinesIntoArray(string content)
        {
            string[] lines = content.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return lines;
        }

        private static string GetContentRootPath()
        {
            var appRootDirectory = AppContext.BaseDirectory;

            if (appRootDirectory.Contains("bin"))
            {
                appRootDirectory = appRootDirectory.Substring(0, appRootDirectory.IndexOf("bin"));
            }

            return appRootDirectory;
        }

        private static string ReadFile(string fileFullPath)
        {
            if (System.IO.File.Exists(fileFullPath))
            {
                var filecontent = System.IO.File.ReadAllText(fileFullPath);
                return filecontent;
            }
            else
            {
                throw new Exception("File not found: " + fileFullPath);
            }
        }
    }
}
