using Common;
using ToolsFramework;

namespace AdventOfCode2024Solutions.Day08
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 8: Resonant Collinearity";
        private bool printInitialMapToConsole => true;
        private bool printFinalMapToConsole => true;
        private bool printMapToConsoleEveryStep => false;

        private Dictionary<char, List<Coordinate>> antennas = [];
        private int numberOfXs = 0;
        private int numberOfYs = 0;

        private char[,]? antinodeMap;

        public string SolvePart1(string[] datasetLines)
        {
            numberOfYs = datasetLines.Length;
            numberOfXs = datasetLines[0].Length;

            if (printFinalMapToConsole || printInitialMapToConsole || printMapToConsoleEveryStep)
                antinodeMap = new char[numberOfXs, numberOfYs];

            for (int y = 0; y < datasetLines.Length; y++)
            {
                for (int x = 0; x < datasetLines[y].Length; x++)
                {
                    var mapSpace = datasetLines[y][x];

                    AddToAntinodeMap(mapSpace, x, y);

                    if (mapSpace == '.')
                        continue;

                    var antennaCoordinate = new Coordinate(x, y);

                    if (antennas.TryGetValue(mapSpace, out var aList))
                        aList.Add(antennaCoordinate);
                    else
                        antennas.Add(mapSpace, new List<Coordinate> { antennaCoordinate });
                }
            }

            if (printInitialMapToConsole)
                ConsolePrinting.PrintMapToConsole(antinodeMap);

            var antinodes = new HashSet<int>();

            foreach (var frequency in antennas.Keys)
            {
                IdentifyAntinodes(antennas[frequency].ToArray(), antinodes);
                if (printMapToConsoleEveryStep)
                    ConsolePrinting.PrintMapToConsole(antinodeMap);

            }

            if (printFinalMapToConsole)
                ConsolePrinting.PrintMapToConsole(antinodeMap);

            return antinodes.Count.ToString();
        }

        private void IdentifyAntinodes(Coordinate[] coordinates, HashSet<int> antinodes)
        {
            var numberOfCoordinates = coordinates.Length;
            for (int a = 0; a < numberOfCoordinates - 1; a++)
                for (int b = a + 1; b < numberOfCoordinates; b++)
                {
                    CalculateAntinodes(coordinates[a], coordinates[b], antinodes);
                    if (printMapToConsoleEveryStep)
                        ConsolePrinting.PrintMapToConsole(antinodeMap);
                }
        }

        private void CalculateAntinodes(Coordinate a, Coordinate b, HashSet<int> antinodes)
        {
            var antinodeLeftX = a.X - (b.X - a.X);
            var antinodeRightX = b.X + (b.X - a.X);
            var antinodeLeftY = a.Y - (b.Y - a.Y);
            var antinodeRightY = b.Y + (b.Y - a.Y);

            if (IsCoordinateInMap(antinodeLeftX, antinodeLeftY))
            {
                AddToAntinodeMap('#', antinodeLeftX, antinodeLeftY);
                var antinodeKey1 = CreateCoordinateKey(antinodeLeftX, antinodeLeftY);
                antinodes.Add(antinodeKey1);
            }

            if (IsCoordinateInMap(antinodeRightX, antinodeRightY))
            {
                AddToAntinodeMap('#', antinodeRightX, antinodeRightY);
                var antinodeKey2 = CreateCoordinateKey(antinodeRightX, antinodeRightY);
                antinodes.Add(antinodeKey2);
            }
        }

        private bool IsCoordinateInMap(int x, int y)
        {
            return x >= 0 && x < numberOfXs && y >= 0 && y < numberOfYs;
        }

        private int CreateCoordinateKey(int x, int y)
        {
            return x * 100 + y;
        }

        private void AddToAntinodeMap(char mapTile, int x, int y)
        {
            if (antinodeMap == null)
                return;

            if (IsCoordinateInMap(x, y))
                antinodeMap[y, x] = mapTile;
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
