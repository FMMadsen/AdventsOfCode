using ToolsFramework.Map;

namespace AdventOfCode2025Solutions.Day07
{
    internal class Location(int x, int y, char source) : GenericMapTile(x, y, source)
    {
        internal Location? Left => base.West as Location;
        internal Location? Right => base.East as Location;
        internal Location? Down => base.South as Location;
        internal long TimerValue { get; set; } = 0;
    }
}