using Common;
using System.Collections.Frozen;


namespace AdventOfCode2023Solutions.Day10
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: Pipe Maze";

        private string[] _DatasetLines = [];
        public string[] DatasetLines { get { return _DatasetLines; } }

        private Pipe StartPipe { get; set; } = new Pipe();
        public List<Pipe> Pipeline { get; } = new List<Pipe>();

        public string SolvePart1(string[] datasetLines)
        {
            _DatasetLines = datasetLines;
            StartPipe = GetStartPipe();
            Pipeline.Add ( StartPipe );

            Pipe nextPipe = GetNextPipe(StartPipe);
            
            while (StartPipe != nextPipe)
            {
                Pipeline.Add(nextPipe);
                nextPipe = GetNextPipe(Pipeline[Pipeline.Count - 1]);
                
            }
            
            int furthestDistance = (int)(Pipeline.Count * 0.5);


            return furthestDistance.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            if (2 > Pipeline.Count) { SolvePart1(datasetLines); }

            FrozenDictionary<PipeVector2,Pipe> pipeRing = Pipeline.ToFrozenDictionary(a=>a.Location, a=>a);
            List<PipeVector2> inside = [];

            PipeVector2 searchStartPipeLocation = SearchRingStartPipe(pipeRing, out PipeVector2 searchDirection);

            AddInsideAlongPipeline(searchStartPipeLocation, searchDirection, pipeRing, inside);

            AddFloatingInside(inside, pipeRing);

            return inside.Count.ToString();
        }

        private Pipe GetStartPipe()
        {
            Pipe? start = null;
            int startX;
            
            for (int i = 0; i < DatasetLines.Length; i++)
            {
                startX = DatasetLines[i].IndexOf('S');
                if (-1 < startX)
                {
                    start = new Pipe() { Location = new PipeVector2() { X = startX, Y = i } };

                    Pipe? north = GetPipeFrom(start.Location + PipeConnections.North);
                    if (null != north && PipeDirection.South.HasFlag(north.Direction)) 
                    {
                        north.Connections[PipeConnections.South] = start;
                        start.Connections[PipeConnections.North] = north;
                    }

                    Pipe? south = GetPipeFrom(start.Location + PipeConnections.South);
                    if (null != south && PipeDirection.North.HasFlag(south.Direction))
                    {
                        south.Connections[PipeConnections.North] = start;
                        start.Connections[PipeConnections.South] = south;
                    }

                    Pipe? east = GetPipeFrom(start.Location + PipeConnections.East);
                    if (null != east && PipeDirection.West.HasFlag(east.Direction))
                    {
                        east.Connections[PipeConnections.West] = start;
                        start.Connections[PipeConnections.East] = east;
                    }

                    Pipe? west = GetPipeFrom(start.Location + PipeConnections.West);
                    if (null != west && PipeDirection.East.HasFlag(west.Direction))
                    {
                        west.Connections[PipeConnections.East] = start;
                        start.Connections[PipeConnections.West] = west;
                    }

                    start.Connections[start.Connections.First().Key] = null;

                    break;
                }
            }

            if (null == start) { throw new Exception("No StartPipe."); } else { return start; }
        }

        private Pipe GetNextPipe(Pipe currentPipe)
        {
            Pipe? nextPipe = null;

            foreach (KeyValuePair<PipeVector2,Pipe?> keyValuePair in currentPipe.Connections)
            {
                if(null == keyValuePair.Value)
                {
                    if (StartPipe.Location == currentPipe.Location + keyValuePair.Key) 
                    {
                        nextPipe = StartPipe;
                    }
                    else
                    {
                        nextPipe = GetPipeFrom(currentPipe.Location + keyValuePair.Key);
                    }

                    currentPipe.Connections[keyValuePair.Key] = nextPipe;

                    if (null != nextPipe)
                    {
                        nextPipe.Connections[-keyValuePair.Key] = currentPipe;
                    }  
                }
            }

            if (null == nextPipe)
            {
                throw new Exception("Unable to find next Pipe");
            }

            return nextPipe;
        }

        private Pipe? GetPipeFrom(PipeVector2 location)
        {
            if (-1 < location.Y
                && -1 < location.X
                && location.Y < DatasetLines.Length
                && location.X < DatasetLines[location.Y].Length
                && PipeConnections.PipeDirectionFromChar.ContainsKey(DatasetLines[location.Y][location.X]) 
                )
            {
                return new Pipe()
                {
                    Location = location,
                    Direction = PipeConnections.PipeDirectionFromChar[DatasetLines[location.Y][location.X]],
                    Connections = PipeConnections.ConnectionsFromDirection(PipeConnections.PipeDirectionFromChar[DatasetLines[location.Y][location.X]])
                };
            }
            else { return null; }
        }

        private PipeVector2 SearchForPipeline(FrozenDictionary<PipeVector2, Pipe> pipeRing, out PipeVector2 searchLocation)
        {
            PipeVector2 lastLocation = new PipeVector2() { X = -1, Y = -1 };
            
            for (int iY = 0; iY < DatasetLines.Length; iY++)
            {
                lastLocation = new PipeVector2() { X = -1, Y = iY };

                for (int iX = 0; iX < DatasetLines[iY].Length; iX++)
                {
                    PipeVector2 iLocation = new PipeVector2() { X = iX, Y = iY };

                    if (pipeRing.ContainsKey(iLocation))
                    {
                        searchLocation = lastLocation;
                        return iLocation;
                    }

                    lastLocation = iLocation;
                }

            }

            throw new Exception("No PipeLine found");
        }

        private PipeVector2 SearchRingStartPipe(FrozenDictionary<PipeVector2, Pipe> pipeRing, out PipeVector2 searchDirection)
        {
            PipeVector2 pipelineLocation = SearchForPipeline(pipeRing, out PipeVector2 searchLocation);

            PipeVector2 circlePipeDirection = (pipelineLocation - searchLocation).Right90();

            while (!pipeRing.ContainsKey(searchLocation) || !pipeRing[searchLocation].Connections.ContainsKey(pipelineLocation - searchLocation))
            {
                searchLocation = searchLocation + circlePipeDirection;
                circlePipeDirection = circlePipeDirection.Left90();
                searchLocation = searchLocation + circlePipeDirection;
            }
            
            searchDirection = pipelineLocation - searchLocation;
            return searchLocation;
            
        }

        private void AddInsideAlongPipeline(PipeVector2 currentPipeLocation, PipeVector2 searchDirection, FrozenDictionary<PipeVector2, Pipe> pipeRing, List<PipeVector2> inside)
        {
            for (int iRing = 0; iRing <= pipeRing.Count; iRing++)
            {
                // search right of current
                AddInside(currentPipeLocation + searchDirection.Right90(), pipeRing, inside);
                
                // search right of next
                if (null != pipeRing[currentPipeLocation].Connections[searchDirection]) 
                {
                    currentPipeLocation = pipeRing[currentPipeLocation].Connections[searchDirection].Location;
                }
                else { throw new Exception("Pipeline broken"); }
                AddInside(currentPipeLocation + searchDirection.Right90(), pipeRing, inside);

                // turn
                foreach (PipeVector2 direction in pipeRing[currentPipeLocation].Connections.Keys) 
                {
                    if (direction != searchDirection.Back180())
                    {
                        searchDirection = direction;
                        break;
                    }
                }
            }
        }

        private void AddFloatingInside(List<PipeVector2> inside, FrozenDictionary<PipeVector2, Pipe> pipeRing)
        {
            PipeVector2[] searchLocations = [PipeConnections.North, PipeConnections.South, PipeConnections.East, PipeConnections.West];
            for (int i = 0; i < inside.Count; i++)
            {
                foreach (PipeVector2 searchLocation in searchLocations)
                {
                    AddInside(inside[i] + searchLocation, pipeRing, inside);
                }
            }
        }

        private void AddInside(PipeVector2 searchLocation, FrozenDictionary<PipeVector2, Pipe> pipeRing, List<PipeVector2> inside)
        {
            if (!pipeRing.ContainsKey(searchLocation) && !inside.Contains(searchLocation))
            {
                inside.Add(searchLocation);
            }
        }

    }
}
