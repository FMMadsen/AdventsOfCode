namespace _2022
{
    public class MyFileReader
    {
        public static string ReadFileFromCurrentFolder(string filename)
        {
            var contentRootPath = GetContentRootPath();
            var fileFullPath = contentRootPath + filename;

            var fileContent = ReadFile(fileFullPath);
            return fileContent;
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
