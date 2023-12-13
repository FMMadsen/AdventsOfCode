namespace AdventOfCode2023Solutions.Day08
{
    public class Map
    {
        public Dictionary<string, MapPiece> MapPieces { get; private set; } = new Dictionary<string, MapPiece>();
        public char[] MoveInstructions { get; private set; }
        public string MoveInstructionsString { get; private set; }
        public int NumberOfMoveInstructions { get; private set; }

        public int NextMoveInstructionPointer { get; private set; } = 0;
        public long MoveCounter { get; private set; } = 0;

        public MapPiece[] Part2_EntryLocations { get; private set; } = [];
        public MapPiece[] Part2_ExitLocations { get; private set; } = [];
        public MapPiece?[] Part2_CurrentLocations { get; private set; } = [];
        public int Part2_NumberOfParralelLocations { get; private set; } = 0;
        public string Part2_EntryLocationLabelEnd { get; private set; } = string.Empty;
        public string Part2_ExitLocationLabelEnd { get; private set; } = string.Empty;

        public Map(string[] initializeStrings)
        {
            MoveInstructionsString = initializeStrings[0].Trim();
            MoveInstructions = MoveInstructionsString.ToArray();
            NumberOfMoveInstructions = MoveInstructions.Length;

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

        public void Part2_InitializeLocationVariables(string entryLocationLabelEnd, string exitLocationLabelEnd)
        {
            Part2_EntryLocationLabelEnd = entryLocationLabelEnd;
            Part2_ExitLocationLabelEnd = exitLocationLabelEnd;

            Part2_EntryLocations = MapPieces.Where(p => p.Value.Label.EndsWith(Part2_EntryLocationLabelEnd)).Select(k => k.Value).ToArray();
            Part2_ExitLocations = MapPieces.Where(p => p.Value.Label.EndsWith(Part2_ExitLocationLabelEnd)).Select(k => k.Value).ToArray();
            Part2_NumberOfParralelLocations = Part2_EntryLocations.Length;

            Part2_CurrentLocations = new MapPiece[Part2_NumberOfParralelLocations];
            Array.Copy(Part2_EntryLocations, Part2_CurrentLocations, Part2_NumberOfParralelLocations);
        }

        public List<long>[] CountMovesToFirstXExits(int exitEncounterIterations)
        {
            var movesToExitStatistics = new List<long>[Part2_NumberOfParralelLocations];
            for (int i = 0; i < movesToExitStatistics.Length; i++)
                movesToExitStatistics[i] = new List<long>();

            var neededStatistics = exitEncounterIterations;
            var foundAllNeededStatistics = false;

            while (!foundAllNeededStatistics)
            {
                MoveAllLocations();

                for (int i = 0; i < Part2_NumberOfParralelLocations; i++)
                {
                    if (Part2_CurrentLocations[i]?.Label?.EndsWith(Part2_ExitLocationLabelEnd) ?? false)
                    {
                        movesToExitStatistics[i].Add(MoveCounter);
                        foundAllNeededStatistics = movesToExitStatistics.Count(s => s.Count >= neededStatistics) == Part2_NumberOfParralelLocations;
                    }
                }
            }

            return movesToExitStatistics;
        }

        private void MoveAllLocations()
        {
            for (int i = 0; i < Part2_NumberOfParralelLocations; i++)
            {
                if (MoveInstructions[NextMoveInstructionPointer] == 'L')
                    Part2_CurrentLocations[i] = Part2_CurrentLocations[i]?.LeftMapPiece;

                else
                    Part2_CurrentLocations[i] = Part2_CurrentLocations[i]?.RightMapPiece;
            }

            MoveCounter++;

            NextMoveInstructionPointer++;
            if (NextMoveInstructionPointer == NumberOfMoveInstructions)
                NextMoveInstructionPointer = 0;
        }
    }
}
