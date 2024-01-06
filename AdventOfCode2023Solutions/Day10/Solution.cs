using AdventOfCode2023Solutions.Day08;
using Common;
using System.Collections.Generic;
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
    }

    public class Pipe
    {
        public PipeVector2 Location;
        public Dictionary<PipeVector2, Pipe?> Connections = new Dictionary<PipeVector2, Pipe?>();
    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: ";

        public string[] DatasetLines { get; }

        private Pipe StartPipe { get; }
        public List<Pipe> PipeLine { get; }

        public Solution(string[] datasetLines)
        {
            DatasetLines = datasetLines;
            StartPipe = GetStartPipe();
            PipeLine = new List<Pipe>() { StartPipe };
        }

        public string SolvePart1()
        {
            Pipe nextPipe = GetNextPipe(StartPipe);
            
            while (StartPipe != nextPipe)
            {
                PipeLine.Add(nextPipe);
                nextPipe = GetNextPipe(PipeLine[PipeLine.Count - 1]);
                
            }
            
            int furthestDistance = (int)(PipeLine.Count * 0.5);


            return furthestDistance.ToString();
        }

        public string SolvePart2()
        {
            return "To be implemented";
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
                    if (null != north) 
                    {
                        north.Connections[PipeConnections.South] = start;
                        start.Connections[PipeConnections.North] = north;
                    }

                    Pipe? south = GetPipeFrom(start.Location + PipeConnections.South);
                    if (null != south)
                    {
                        south.Connections[PipeConnections.North] = start;
                        start.Connections[PipeConnections.South] = south;
                    }

                    Pipe? east = GetPipeFrom(start.Location + PipeConnections.East);
                    if (null != east)
                    {
                        east.Connections[PipeConnections.West] = start;
                        start.Connections[PipeConnections.East] = east;
                    }

                    Pipe? west = GetPipeFrom(start.Location + PipeConnections.West);
                    if (null != west)
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
                    if (StartPipe.Location == currentPipe.Location + keyValuePair.Key) { return StartPipe; }
                    nextPipe = GetPipeFrom(currentPipe.Location + keyValuePair.Key);
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
                    Connections = PipeConnections.ConnectionsFromDirection(PipeConnections.PipeDirectionFromChar[DatasetLines[location.Y][location.X]])
                };
            }
            else { return null; }
        }
    }
}
