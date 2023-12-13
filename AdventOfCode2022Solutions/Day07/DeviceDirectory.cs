namespace AdventsOfCode2022.Day07FileDirectorySizes
{
    internal class DeviceDirectory
    {
        public DeviceDirectory(DeviceDirectory? parent, string name)
        {
            Parent = parent;
            Name = name;
            Path = Parent == null ? "" : $"{Parent.Path}\\{Name}";
            Files = new List<DeviceFile>();
            Directories = new List<DeviceDirectory>();
        }

        public DeviceDirectory? Parent { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public int FilesSizeSum { get; private set; }
        public int DirectorySize { get; private set; }
        public IList<DeviceFile> Files { get; private set; }
        public IList<DeviceDirectory> Directories { get; private set; }

        public void Add(DeviceDirectory directory)
        {
            Directories.Add(directory);
        }

        public void Add(DeviceFile file)
        {
            Files.Add(file);
            FilesSizeSum += file.Size;
        }

        internal DeviceDirectory? GetChildDirectory(string directoryName)
        {
            if (directoryName != null && directoryName.Length > 0)
            {
                return Directories.FirstOrDefault(d => d.Name.Equals(directoryName));
            }
            return null;
        }

        public void CalculateSize()
        {
            DirectorySize += FilesSizeSum;

            foreach (DeviceDirectory directory in Directories)
            {
                directory.CalculateSize();
                DirectorySize += directory.DirectorySize;
            }
        }

        public void Print()
        {
            if (Parent == null)
                Console.Write($"root (");
            else
                Console.Write($"{Path} (dir)(");

            var previousColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write($"{DirectorySize:N0}");
            Console.BackgroundColor = previousColor;
            Console.Write(")");
            Console.WriteLine();

            foreach (DeviceFile file in Files)
            {
                file.Print();
            }

            foreach (DeviceDirectory directory in Directories)
            {
                directory.Print();
            }
        }

        public void SearchDirectories(int filterMazSize, IList<DeviceDirectory> resultList)
        {
            foreach (DeviceDirectory directory in Directories)
            {
                if (directory.DirectorySize <= filterMazSize)
                    resultList.Add(directory);

                directory.SearchDirectories(filterMazSize, resultList);
            }
        }

        public DeviceDirectory SearchSmallestDirectory(int minSize, DeviceDirectory referenceDirectory)
        {
            var smallestDirectoryFoundSoFar = referenceDirectory;

            if (this.DirectorySize >= minSize && this.DirectorySize < referenceDirectory.DirectorySize)
            {
                smallestDirectoryFoundSoFar = this;
            }

            foreach (DeviceDirectory directory in Directories)
            {
                smallestDirectoryFoundSoFar = directory.SearchSmallestDirectory(minSize, smallestDirectoryFoundSoFar);
            }

            return smallestDirectoryFoundSoFar;
        }
    }
}