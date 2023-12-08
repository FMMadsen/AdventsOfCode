namespace AdventOfCode2023Solutions.Day08
{
    public class Map
    {
        public Dictionary<string, MapPiece> MapPieces { get; private set; } = new Dictionary<string, MapPiece>();
        public char[] MoveInstructions { get; private set; }

        public Map(string[] initializeStrings)
        {
            MoveInstructions = initializeStrings[0].Trim().ToArray();

            for (int i = 2; i < initializeStrings.Length; i++)
            {
                var mapPiece = new MapPiece(initializeStrings[i]);
                MapPieces.Add(mapPiece.Label, mapPiece);
            }

            foreach (var mapPiece in MapPieces)
            {
                mapPiece.Value.InitializeNetwork(MapPieces);
            }
        }

        public long CountMovesPart1(string startLocation, string destinationLocation)
        {
            var currentLocation = MapPieces[startLocation];
            var destination = MapPieces[destinationLocation];
            long moveCounter = 0;

            for (int i = 0; i < MoveInstructions.Length; i++)
            {
                moveCounter++;
                var moveInstruction = MoveInstructions[i];

                if (moveInstruction == 'L')
                    currentLocation = currentLocation?.LeftMapPiece;

                if (moveInstruction == 'R')
                    currentLocation = currentLocation?.RightMapPiece;

                if (currentLocation == null)
                    throw new Exception("Walked off the map");

                if (currentLocation == destination)
                    break;

                if (i == MoveInstructions.Length - 1)
                    i = -1;
            }

            return moveCounter;
        }

        public long CountMovesPart2(string startLocationEnd, string endLocationEnd)
        {
            var currentLocations = MapPieces.Where(p => p.Value.Label.EndsWith(startLocationEnd)).Select(k => k.Value).ToArray();
            var parallelCount = MapPieces.Count;

            long moveCounter = 0;

            for (int i = 0; i < MoveInstructions.Length; i++)
            {
                moveCounter++;
                var moveInstruction = MoveInstructions[i];

                if (Move(currentLocations, moveInstruction, endLocationEnd))
                    break;

                if (i == MoveInstructions.Length - 1)
                    i = -1;
            }

            return moveCounter;
        }

        private bool Move(MapPiece[] currentLocations, char direction, string endLocationEnd)
        {
            long countDestinations = 0;

            for (int i = 0; i < currentLocations.Length; i++)
            {
                if (direction == 'L')
                    currentLocations[i] = currentLocations[i].LeftMapPiece;

                if (direction == 'R')
                    currentLocations[i] = currentLocations[i].RightMapPiece;

                if(currentLocations[i].Label.EndsWith(endLocationEnd))
                    countDestinations++;
            }

            return countDestinations == currentLocations.Length;
        }
    }
}
