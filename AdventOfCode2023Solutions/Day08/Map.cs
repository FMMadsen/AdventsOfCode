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

        public MapPiece[] Part2_EntryLocations { get; private set; }
        public MapPiece[] Part2_ExitLocations { get; private set; }
        public MapPiece?[] Part2_CurrentLocations { get; private set; }
        public int Part2_NumberOfParralelLocations { get; private set; }
        public string Part2_EntryLocationLabelEnd { get; private set; }
        public string Part2_ExitLocationLabelEnd { get; private set; }

        public int Part2_NextDirectionPointer { get; private set; } = 0;
        public int Part2_PreviousDirectionPointer { get; private set; } = 0;


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



        public long Part2_CountMovesToExit(MapPiece entryLocation)
        {
            MapPiece currentLocation = entryLocation;
            
            long moveCounter = 0;
            int directionPointer = 0;

            bool exitLocationFound = false;
            while (!exitLocationFound)
            {
                moveCounter++;
                currentLocation = Move(currentLocation, MoveInstructions[directionPointer]);
                
                if(currentLocation.Label.EndsWith(Part2_ExitLocationLabelEnd))
                    exitLocationFound = true;

                directionPointer++;
                if(directionPointer == MoveInstructions.Length)
                    directionPointer = 0;
            }
            return moveCounter;
        }

        public static MapPiece Move(MapPiece currentLocation, char direction)
        {
            if (direction == 'L')
                return currentLocation.LeftMapPiece;

            else
                return currentLocation.RightMapPiece;
        }

       

        public void Part2_Move()
        {
            MoveCounter++;
            var nextDirection = MoveInstructions[Part2_NextDirectionPointer];
            MoveAllLocations(Part2_CurrentLocations, nextDirection);

            Part2_PreviousDirectionPointer = Part2_NextDirectionPointer++;
            if (Part2_NextDirectionPointer == MoveInstructions.Length)
                Part2_NextDirectionPointer = 0;
        }

        public int CountExitEndings()
        {
            return Part2_CurrentLocations.Count(l => l.Label.EndsWith('Z'));
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
                MoveAllLocations(currentLocations, direction);

                if (currentLocations[i].Label.EndsWith(exitLocationLabelEnd))
                    countDestinations++;
            }

            return countDestinations == currentLocations.Length;
        }

        private static void MoveAllLocations(MapPiece[] currentLocations, char direction)
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
