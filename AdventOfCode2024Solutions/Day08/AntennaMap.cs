using ToolsFramework;
using ToolsFramework.Geometry;

namespace AdventOfCode2024Solutions.Day08
{
    internal class AntennaMap
    {
        private readonly Dictionary<char, List<Coordinate>> antennas = [];
        private readonly long numberOfXs = 0;
        private readonly long numberOfYs = 0;

        private readonly char[,]? antinodeMap;

        internal int AntinodeCount => antinodeMap?.Length ?? 0;

        public AntennaMap(string[] mapLines)
        {
            numberOfYs = mapLines.Length;
            numberOfXs = mapLines[0].Length;

            if (Solution.PrintFinalMapToConsole || Solution.PrintInitialMapToConsole || Solution.PrintMapToConsoleEveryStep)
                antinodeMap = new char[numberOfXs, numberOfYs];

            for (int y = 0; y < mapLines.Length; y++)
            {
                for (int x = 0; x < mapLines[y].Length; x++)
                {
                    var mapSpace = mapLines[y][x];

                    AddToAntinodeMap(mapSpace, x, y);

                    if (mapSpace == '.')
                        continue;

                    var antennaCoordinate = Coordinate.FromCoordinateStringXY<Coordinate>(x, y);

                    if (antennas.TryGetValue(mapSpace, out var aList))
                        aList.Add(antennaCoordinate);
                    else
                        antennas.Add(mapSpace, new List<Coordinate> { antennaCoordinate });
                }
            }

            if (Solution.PrintInitialMapToConsole)
                ConsolePrinterOfCharMap.PrintMapToConsole(antinodeMap);
        }

        internal int IdentifyAndCountAntinodes(bool extendedResonance = false)
        {
            var antinodes = new HashSet<long>();

            foreach (var frequency in antennas.Keys)
            {
                IdentifyAntinodes(antennas[frequency].ToArray(), antinodes, extendedResonance);
                if (Solution.PrintMapToConsoleEveryStep)
                    ConsolePrinterOfCharMap.PrintMapToConsole(antinodeMap);
            }

            if (Solution.PrintFinalMapToConsole)
                ConsolePrinterOfCharMap.PrintMapToConsole(antinodeMap);

            return antinodes.Count;
        }

        private void IdentifyAntinodes(Coordinate[] coordinates, HashSet<long> antinodes, bool extendedResonance)
        {
            var numberOfCoordinates = coordinates.Length;
            for (int a = 0; a < numberOfCoordinates - 1; a++)
                for (int b = a + 1; b < numberOfCoordinates; b++)
                {
                    CalculateAntinodes(coordinates[a], coordinates[b], antinodes, extendedResonance);
                    if (Solution.PrintMapToConsoleEveryStep)
                        ConsolePrinterOfCharMap.PrintMapToConsole(antinodeMap);
                }
        }

        private void CalculateAntinodes(Coordinate a, Coordinate b, HashSet<long> antinodes, bool extendedResonance)
        {
            if (extendedResonance)
            {
                AddToAntinodeList(a, antinodes);
                AddToAntinodeList(b, antinodes);
                AddToAntinodeMap('#', a);
                AddToAntinodeMap('#', b);
            }

            CalculateAntinodeExtendRight(a, b, antinodes, extendedResonance);
            CalculateAntinodeExtendLeft(a, b, antinodes, extendedResonance);
        }

        private void CalculateAntinodeExtendRight(Coordinate a, Coordinate b, HashSet<long> antinodes, bool extendedResonance)
        {
            var antinodeX = b.X + (b.X - a.X);
            var antinodeY = b.Y + (b.Y - a.Y);
            var newCoordinate = Coordinate.FromCoordinateStringXY<Coordinate>(antinodeX, antinodeY);
            var isWithinMap = IsCoordinateInMap(newCoordinate);

            if (isWithinMap)
            {
                AddToAntinodeMap('#', newCoordinate);
                antinodes.Add(CreateCoordinateKey(newCoordinate));
                if (extendedResonance)
                    CalculateAntinodeExtendRight(b, newCoordinate, antinodes, extendedResonance);
            }
        }

        private void CalculateAntinodeExtendLeft(Coordinate a, Coordinate b, HashSet<long> antinodes, bool extendedResonance)
        {
            var antinodeX = a.X - (b.X - a.X);
            var antinodeY = a.Y - (b.Y - a.Y);
            var newCoordinate = Coordinate.FromCoordinateStringXY<Coordinate>(antinodeX, antinodeY);
            var isWithinMap = IsCoordinateInMap(newCoordinate);

            if (isWithinMap)
            {
                AddToAntinodeMap('#', newCoordinate);
                antinodes.Add(CreateCoordinateKey(newCoordinate));
                if (extendedResonance)
                    CalculateAntinodeExtendLeft(newCoordinate, a, antinodes, extendedResonance);
            }
        }

        private void AddToAntinodeList(Coordinate coordinate, HashSet<long> antinodes)
        {
            antinodes.Add(CreateCoordinateKey(coordinate));
        }

        private static long CreateCoordinateKey(Coordinate coordinate)
        {
            return coordinate.X * 100 + coordinate.Y;
        }

        private int CreateCoordinateKey(int x, int y)
        {
            return x * 100 + y;
        }

        private void AddToAntinodeMap(char mapTile, Coordinate coordinate)
        {
            if (antinodeMap == null)
                return;

            if (IsCoordinateInMap(coordinate))
                antinodeMap[coordinate.Y, coordinate.X] = mapTile;
        }

        private void AddToAntinodeMap(char mapTile, int x, int y)
        {
            if (antinodeMap == null)
                return;

            if (IsCoordinateInMap(x, y))
                antinodeMap[y, x] = mapTile;
        }

        private bool IsCoordinateInMap(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X < numberOfXs && coordinate.Y >= 0 && coordinate.Y < numberOfYs;
        }

        private bool IsCoordinateInMap(int x, int y)
        {
            return x >= 0 && x < numberOfXs && y >= 0 && y < numberOfYs;
        }
    }
}
