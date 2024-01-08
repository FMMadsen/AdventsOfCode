using AdventOfCode2023Solutions.Day08;
using Common;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace AdventOfCode2023Solutions.Day10
{
    public static class PipeConnections
    {
        public static PipeVector2 North = new PipeVector2() { X=0,Y=-1};
        public static PipeVector2 East = new PipeVector2() { X = 1, Y = 0 };
        public static PipeVector2 South = new PipeVector2() { X = 0, Y = 1 };
        public static PipeVector2 West = new PipeVector2() { X = -1, Y = 0 };

        public static Dictionary<char, PipeDirection> PipeDirectionFromChar = new Dictionary<char, PipeDirection>()
        {
            { '-', PipeDirection.Horizontal },
            { '|', PipeDirection.Vertical },
            { 'L', PipeDirection.NE },
            { 'J', PipeDirection.NW },
            {'F', PipeDirection.SE },
            {'7', PipeDirection.SW }
        };

        public static Dictionary<string, PipeDirection> PipeDirectionFromString = new Dictionary<string, PipeDirection>()
        {
            { "EW", PipeDirection.Horizontal },
            { "NS", PipeDirection.Vertical },
            { "NE", PipeDirection.NE },
            { "NW", PipeDirection.NW },
            { "SE", PipeDirection.SE },
            { "SW", PipeDirection.SW }
        };

        public static Dictionary<PipeVector2, Pipe?> ConnectionsFromDirection(PipeDirection direction)
        {
            Dictionary<PipeVector2, Pipe?> connections = new Dictionary<PipeVector2, Pipe?>();
            
            if (PipeDirection.North.HasFlag(direction)) 
            {
                connections.Add(new PipeVector2() { X = 0, Y = -1}, null);
            }
            if (PipeDirection.South.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = 0, Y = 1 }, null);
            }
            if (PipeDirection.East.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = 1, Y = 0 }, null);
            }
            if (PipeDirection.West.HasFlag(direction))
            {
                connections.Add(new PipeVector2() { X = -1, Y = 0 }, null);
            }

            return connections;
        }
    }

    [Flags]
    public enum PipeDirection
    {
        NonPipe = 0,
        Horizontal = 1,
        Vertical = 2,
        NE = 4,
        NW = 8,
        SE = 16,
        SW = 32,
        North = Vertical | NE | NW,
        South = Vertical | SE | SW,
        East = Horizontal | NE | SE,
        West = Horizontal | NW | SW
    }

    public struct PipeVector2
    {
        public int X, Y;

        public static PipeVector2 operator +(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y};
        }
        public static PipeVector2 operator -(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X - vector2.X, Y = vector1.Y - vector2.Y };
        }
        public static PipeVector2 operator -(PipeVector2 vector1)
        {
            return new PipeVector2() { X = - vector1.X, Y = - vector1.Y };
        }
        public static PipeVector2 operator *(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X * vector2.X, Y = vector1.Y * vector2.Y };
        }
        public static PipeVector2 operator /(PipeVector2 vector1, PipeVector2 vector2)
        {
            return new PipeVector2() { X = vector1.X / vector2.X, Y = vector1.Y / vector2.Y };
        }
        public static bool operator ==(PipeVector2 vector1, PipeVector2 vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }
        public static bool operator !=(PipeVector2 vector1, PipeVector2 vector2)
        {
            return vector1.X != vector2.X || vector1.Y != vector2.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is PipeVector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        
        public PipeVector2 Right90()
        {
            return new PipeVector2 { X = -Y, Y = X };
        }
        public PipeVector2 Left90()
        {
            return new PipeVector2 { X = Y, Y = -X };
        }
        public PipeVector2 Back180()
        {
            return new PipeVector2 { X = -X, Y = -Y };
        }
    }

    public class Pipe
    {
        public PipeVector2 Location { get; set; }
        public PipeDirection Direction {  get; set; }
        public Dictionary<PipeVector2, Pipe?> Connections { get; set; } = new Dictionary<PipeVector2, Pipe?>();
    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: ";

        public string[] DatasetLines { get; }

        private Pipe StartPipe { get; }
        public List<Pipe> Pipeline { get; }

        public Solution(string[] datasetLines)
        {
            DatasetLines = datasetLines;
            StartPipe = GetStartPipe();
            Pipeline = new List<Pipe>() { StartPipe };
        }

        public string SolvePart1()
        {
            Pipe nextPipe = GetNextPipe(StartPipe);
            
            while (StartPipe != nextPipe)
            {
                Pipeline.Add(nextPipe);
                nextPipe = GetNextPipe(Pipeline[Pipeline.Count - 1]);
                
            }
            
            int furthestDistance = (int)(Pipeline.Count * 0.5);


            return furthestDistance.ToString();
        }

        public string SolvePart2()
        {
            if (2 > Pipeline.Count) { SolvePart1(); }

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
