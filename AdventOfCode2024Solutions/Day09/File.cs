namespace AdventOfCode2024Solutions.Day09
{
    public class File : DiskBlock
    {
        public int Id { get; }
        public ConsoleColor Color { get; }

        public File(int size, int id)
        {
            base.Size = size;
            this.Id = id;

            var colorNumber = ToolsFramework.Tools.ModulusConverNumberIntoRange(id, 1, 14);
            Color = (ConsoleColor)colorNumber;
        }
    }
}
