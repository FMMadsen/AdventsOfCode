namespace AdventOfCode2023Solutions.Day11
{
    public class Universe
    {
        public List<Galaxy> GalaxyList { get; set; } = [];
        public Dictionary<long, Galaxy> GalaxyDictionary { get; set; } = [];
        public List<GalaxyRoute> GalaxyRoutes { get; set; } = [];


        public Universe(string[] observationRows)
        {
            var idCounter = 1;
            for (int y = 0; y < observationRows.Length; y++)
            {
                for (int x = 0; x < observationRows[0].Length; x++)
                {
                    if (observationRows[y][x] == '#')
                    {
                        var newGalaxy = new Galaxy(y, x, idCounter++);
                        GalaxyList.Add(newGalaxy);
                        GalaxyDictionary.Add(newGalaxy.ID, newGalaxy);
                    }
                }
            }
        }

        public void ExpandUniverse(long expandingRowsAndColumns)
        {
            var columnsUsed = GetColumnsWithGalaxies(GalaxyList);
            var rowsUsed = GetRowsWithGalaxies(GalaxyList);

            var columnsNotUsed = Enumerable.Range(0, columnsUsed.Max()).Where(c => !columnsUsed.Contains(c)).ToArray();
            var rowsNotUsed = Enumerable.Range(0, rowsUsed.Max()).Where(r => !rowsUsed.Contains(r)).ToArray();
            var expandingSize = expandingRowsAndColumns;

            foreach (var galaxy in GalaxyList)
            {
                galaxy.ExpandLocation(columnsNotUsed, rowsNotUsed, expandingSize);
            }
        }

        public static int[] GetColumnsWithGalaxies(List<Galaxy> galaxyList)
        {
            return galaxyList.Select(g => g.X).Distinct().Order().ToArray() ?? [];
        }

        public static int[] GetRowsWithGalaxies(List<Galaxy> galaxyList)
        {
            return galaxyList.Select(g => g.Y).Distinct().Order().ToArray() ?? [];
        }

        public void InitializeGalaxyRoutes()
        {
            var permutations = PermutationSupporter.BuildPermutatinos(2, GalaxyList.Count).ToArray();

            for (int i = 0; i < permutations.Length; i++)
            {
                var routePermutation = permutations[i];
                var gA = GalaxyDictionary[routePermutation[0]];
                var gB = GalaxyDictionary[routePermutation[1]];
                var route = new GalaxyRoute(gA, gB);
                GalaxyRoutes.Add(route);
            }
        }
    }
}