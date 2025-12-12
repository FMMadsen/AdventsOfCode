namespace ToolsFramework.Map
{
    public class GenericMap
    {
        /// <summary>
        /// Map of the source char's
        /// </summary>
        public char[,] SourceMapTiles { get; private init; }

        /// <summary>
        /// Map of the tiles
        /// </summary>
        public GenericMapTile[,] MapTiles { get; private init; }

        /// <summary>
        /// Index of map tiles with a given source char
        /// </summary>
        public Dictionary<char, List<GenericMapTile>> MapTileIndex { get; private init; }

        /// <summary>
        /// Complete collection of MapTiles as a List
        /// </summary>
        public List<GenericMapTile> MapTileList { get; private init; }

        /// <summary>
        /// Number of Y (columns) tiles
        /// </summary>
        public int NoOfYTiles { get; private init; }

        /// <summary>
        /// Number of X (rows) tiles
        /// </summary>
        public int NoOfXTiles { get; private init; }

        private readonly GenericMapTileFactory _mapTileFactory;

        /// <summary>
        /// All elements in the input must be of same length.
        /// Length of first element will be based for all rows in grid
        /// </summary>
        /// <param name="mapLines"></param>
        public GenericMap(string[] mapLines, GenericMapTileFactory? _factory = null)
        {
            _mapTileFactory = _factory ?? new GenericMapTileFactory();
            NoOfYTiles = mapLines.Length;
            NoOfXTiles = mapLines[0].Length;
            SourceMapTiles = new char[NoOfXTiles, NoOfYTiles];
            MapTiles = new GenericMapTile[NoOfXTiles, NoOfYTiles];
            MapTileIndex = [];
            MapTileList = [];

            for (int y = 0; y < NoOfYTiles; y++)
            {
                for (int x = 0; x < NoOfXTiles; x++)
                {
                    var currentTileSource = mapLines[y][x];
                    AddMapTile(x, y, currentTileSource);
                }
            }
        }

        /// <summary>
        /// Building the Map, adding Map Tiles one by one
        /// This approach implemented here, are assuming map tiles
        /// are added always from top left most and in right side
        /// direction and then down.
        /// Meaning also, from NorthWest corner and towards East and then South.
        /// </summary>
        /// <param name="x">the X coordinate (row)</param>
        /// <param name="y">the Y coordinate (column)</param>
        /// <param name="source">the Source char</param>
        private void AddMapTile(int x, int y, char source)
        {
            var currentTile = _mapTileFactory.Create(x, y, source);
            MapTileList.Add(currentTile);
            AddToMapTileIndex(currentTile);
            MapTiles[x, y] = currentTile;
            SourceMapTiles[x, y] = source;
            if (y > 0)
            {
                var northTile = MapTiles[x, y - 1];
                currentTile.North = northTile;
                northTile.South = currentTile;

                if (x > 0)
                {
                    var northWestTile = MapTiles[x - 1, y - 1];
                    currentTile.NorthWest = northWestTile;
                    northWestTile.SouthEast = currentTile;
                }
                if (x < NoOfXTiles - 1)
                {
                    var northEastTile = MapTiles[x + 1, y - 1];
                    currentTile.NorthEast = northEastTile;
                    northEastTile.SouthWest = currentTile;
                }
            }
            if (x > 0)
            {
                var westTile = MapTiles[x - 1, y];
                currentTile.West = westTile;
                westTile.East = currentTile;
            }
        }

        /// <summary>
        /// While building the map, every map tile will be added to the index of MapTiles
        /// </summary>
        /// <param name="mapTile">Map tile to add</param>
        private void AddToMapTileIndex(GenericMapTile mapTile)
        {
            List<GenericMapTile>? mapTileList = null;

            if (MapTileIndex.TryGetValue(mapTile.Source, out List<GenericMapTile>? value))
            {
                mapTileList = value;
                mapTileList?.Add(mapTile);
            }

            if (mapTileList == null)
            {
                mapTileList = [mapTile];
                MapTileIndex.Add(mapTile.Source, mapTileList);
            }
        }

        /// <summary>
        /// Get the map tile for a given zero based index
        /// </summary>
        /// <param name="y">row/y index, zero based</param>
        /// <param name="x">column/x index, zero based</param>
        /// <returns>cell value, long number</returns>
        /// <exception cref="ArgumentOutOfRangeException">If any parameter are out of range in worksheet</exception>
        public GenericMapTile this[int x, int y]
        {
            get
            {
                CheckBoundaries(x, y);
                return MapTiles[x, y];
            }
            set
            {
                CheckBoundaries(x, y);
                MapTiles[x, y] = value;
            }
        }

        /// <summary>
        /// Check boundaries. Throw exception if out of range.
        /// </summary>
        /// <param name="column">column index</param>
        /// <param name="row">row index</param>
        /// <exception cref="ArgumentOutOfRangeException">If any parameter are out of range in worksheet</exception>
        private void CheckBoundaries(int x, int y)
        {
            if (y >= NoOfYTiles)
                throw new ArgumentOutOfRangeException(nameof(y), $"Max column indexer is {NoOfYTiles - 1}");

            if (x >= NoOfXTiles)
                throw new ArgumentOutOfRangeException(nameof(x), $"Max row indexer is {NoOfXTiles - 1}");
        }
    }
}