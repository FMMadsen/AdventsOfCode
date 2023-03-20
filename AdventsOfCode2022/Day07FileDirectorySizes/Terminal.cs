namespace AdventsOfCode2022.Day07FileDirectorySizes
{
    internal class Terminal
    {
        public Terminal()
        {
            RootDirectory = new DeviceDirectory(parent: null, "/");
            CurrentDirectory = RootDirectory;
        }

        public DeviceDirectory RootDirectory { get; set; }

        public DeviceDirectory CurrentDirectory { get; set; }

        public void ChangeDirectory(string command)
        {
            if(command.ToLower().StartsWith("cd"))
            {
                string directoryName = command[3..].Trim();
                
                if(directoryName.Equals(".."))
                {
                    if(CurrentDirectory.Parent != null)
                        CurrentDirectory = CurrentDirectory.Parent;
                }
                else
                {
                    var childDirectory = CurrentDirectory.GetChildDirectory(directoryName);
                    if (childDirectory != null)
                        CurrentDirectory = childDirectory;
                }
            }
        }

        public void CreateChildItems(string childItemLine)
        {
            if (childItemLine.ToLower().StartsWith("dir"))
            {
                string directoryName = childItemLine[4..].Trim();
                CreateNewDirectory(directoryName);
            }
            else
            {
                CreateNewFile(childItemLine);
            }
        }

        public void CreateNewDirectory(string directoryName)
        {
            DeviceDirectory newDirectory = new DeviceDirectory(CurrentDirectory, directoryName);
            CurrentDirectory.Add(newDirectory);
        }

        public void CreateNewFile(string fileSizeNameString)
        {
            string[] fileSizeName = fileSizeNameString.Split(' ');
            int fileSize = int.Parse(fileSizeName[0]);
            string fileName = fileSizeName[1];
            DeviceFile file = new DeviceFile(fileName, fileSize, CurrentDirectory);
            CurrentDirectory.Add(file);
        }

        public void CalculateFileAndDirectorySizes()
        {
            RootDirectory.CalculateSize();
        }

        public void PrintFilesystem()
        {
            Console.WriteLine("---- DEVICE FILES ----");
            RootDirectory.Print();
            Console.WriteLine("----------------------");
        }

        public IList<DeviceDirectory> SearchDirectory(int maxSize)
        {
            var result = new List<DeviceDirectory>();
            RootDirectory.SearchDirectories(maxSize, result);
            return result;
        }
    }
}
