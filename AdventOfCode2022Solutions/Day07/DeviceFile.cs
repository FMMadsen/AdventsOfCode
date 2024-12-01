namespace AdventsOfCode2022.Day07FileDirectorySizes
{
    internal class DeviceFile
    {
        public DeviceFile(string name, int size, DeviceDirectory? parent)
        {
            Name = name;
            Size = size;
            Parent = parent;
            Path = Parent == null ? "" : $"{Parent.Path}\\{Name}";
        }

        public int Size { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public DeviceDirectory? Parent { get; private set; }

        public void Print()
        {
            Console.WriteLine($"{Path} ({Size:N0})");
        }
    }
}
