namespace AdventOfCode2024Solutions.Day16.GenericMapping
{
    public class GenericMap
    {
        public char[,] SourceMapTiles { get; private set; }
        public GenericMapTile[,] MapTiles { get; private set; }
        public Dictionary<char, List<GenericMapTile>> MapTileIndex { get; private set; }
        public List<GenericMapTile> MapTileList { get; private set; }
        public int NoOfYTiles { get; private set; }
        public int NoOfXTiles { get; private set; }

        private GenericMapTileFactory _mapTileFactory;

        public GenericMap(string[] mapLines, GenericMapTileFactory mapTileFactory)
        {
            _mapTileFactory = mapTileFactory;
            NoOfYTiles = mapLines.Length;
            NoOfXTiles = mapLines[0].Length;
            SourceMapTiles = new Char[NoOfYTiles, NoOfXTiles];
            MapTiles = new GenericMapTile[NoOfYTiles, NoOfXTiles];
            MapTileIndex = new Dictionary<char, List<GenericMapTile>>();
            MapTileList = new List<GenericMapTile>();

            for (int y = 0; y < NoOfYTiles; y++)
            {
                for (int x = 0; x < NoOfXTiles; x++)
                {
                    var currentTileSource = mapLines[y][x];
                    AddMapTile(x, y, currentTileSource);
                }
            }
        }

        private void AddMapTile(int x, int y, char source)
        {
            var currentTile = _mapTileFactory.Create(x, y, source);
            MapTileList.Add(currentTile);
            AddToMapTileIndex(currentTile);
            MapTiles[y, x] = currentTile;
            SourceMapTiles[y, x] = source;
            if (y > 0)
            {
                var northTile = MapTiles[y - 1, x];
                currentTile.North = northTile;
                northTile.South = currentTile;
            }
            if (x > 0)
            {
                var westTile = MapTiles[y, x - 1];
                currentTile.West = westTile;
                westTile.East = currentTile;
            }
        }

        private void AddToMapTileIndex(GenericMapTile mapTile)
        {
            List<GenericMapTile>? mapTileList = null;

            if (MapTileIndex.ContainsKey(mapTile.Source))
            {
                mapTileList = MapTileIndex[mapTile.Source];
                if (mapTileList != null)
                {
                    mapTileList.Add(mapTile);
                }
            }

            if (mapTileList == null)
            {
                mapTileList = new List<GenericMapTile> { mapTile };
                MapTileIndex.Add(mapTile.Source, mapTileList);
            }
        }
    }
}
