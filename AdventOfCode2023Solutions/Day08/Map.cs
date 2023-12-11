namespace AdventOfCode2023Solutions.Day08
{
    public class Map
    {
        public Dictionary<string, MapPiece> MapPieces { get; private set; } = new Dictionary<string, MapPiece>();
        public char[] MoveInstructions { get; private set; }

        public int NextDirectionPointer { get; private set; } = 0;
        public long Part2_MoveCounter { get; private set; } = 0;
        public MapPiece[] Part2_EntryLocations { get; private set; }
        public MapPiece[] Part2_ExitLocations { get; private set; }

        public MapPiece[] Part2_CurrentLocations { get; private set; }

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

        public void Part2_DetermineEntryLocations(string entryLocationLabelEnd)
        {
            Part2_EntryLocations = MapPieces.Where(p => p.Value.Label.EndsWith(entryLocationLabelEnd)).Select(k => k.Value).ToArray();
        }

        public void Part2_DetermineExitLocations(string exitLocationLabelEnd)
        {
            Part2_ExitLocations = MapPieces.Where(p => p.Value.Label.EndsWith(exitLocationLabelEnd)).Select(k => k.Value).ToArray();
        }

        public void Part2_Move()
        {
            var numberOfParallelLocations = Part2_EntryLocations.Length;
            Part2_CurrentLocations = new MapPiece[numberOfParallelLocations];
            Array.Copy(Part2_EntryLocations, Part2_CurrentLocations, numberOfParallelLocations);

            Part2_MoveCounter++;

            var nextDirection = MoveInstructions[NextDirectionPointer++];

            Move(Part2_CurrentLocations, nextDirection);


        }

        public long CountMovesPart2(string exitLocationLabelEnd)
        {
            var currentLocations = new MapPiece[Part2_EntryLocations.Length];
            Array.Copy(Part2_EntryLocations, currentLocations, Part2_EntryLocations.Length);
            var parallelCount = MapPieces.Count;

            long moveCounter = 0;

            for (int i = 0; i < MoveInstructions.Length; i++)
            {
                moveCounter++;
                var moveInstruction = MoveInstructions[i];

                if (Move(currentLocations, moveInstruction, exitLocationLabelEnd))
                    break;

                if (i == MoveInstructions.Length - 1)
                    i = -1;
            }

            return moveCounter;
        }

        private bool Move(MapPiece[] currentLocations, char direction, string exitLocationLabelEnd)
        {
            long countDestinations = 0;

            for (int i = 0; i < currentLocations.Length; i++)
            {
                Move(currentLocations, direction);

                if(currentLocations[i].Label.EndsWith(exitLocationLabelEnd))
                    countDestinations++;
            }

            return countDestinations == currentLocations.Length;
        }

        private void Move(MapPiece[] currentLocations, char direction)
        {
            for (int i = 0; i < currentLocations.Length; i++)
            {
                if (direction == 'L')
                    currentLocations[i] = currentLocations[i].LeftMapPiece;

                if (direction == 'R')
                    currentLocations[i] = currentLocations[i].RightMapPiece;
            }
        }
    }
}
