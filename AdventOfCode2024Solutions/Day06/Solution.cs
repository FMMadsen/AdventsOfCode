using AdventOfCode2024Solutions.Day04;
using Common;

namespace AdventOfCode2024Solutions.Day06
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 6: ";

        protected string[] Map = Array.Empty<string>();

        protected static readonly char Up = '^';
        protected static readonly char Right = '>';
        protected static readonly char Down = 'v';
        protected static readonly char Left = '<';
        protected static readonly char Obstacle = '#';

        protected Vector2I Direction = new();
        private Vector2I LocationValue = new();
        protected Vector2I Location
        {
            get
            {
                return LocationValue; 
            }
            set
            {
                LocationValue = value;
                StoreLocation(value);
            }
        }
        protected bool GuardIsMoving = false;
        protected IEnumerable<Vector2I> VisitedLocations = Enumerable.Empty<Vector2I>();
        protected IEnumerable<Vector2I> VisitedDirections = Enumerable.Empty<Vector2I>();


        public string SolvePart1(string[] datasetLines)
        {
            Map = datasetLines;
            SetGuardTransform();

            GuardIsMoving = true;

            while (GuardIsMoving)
            {
                MoveStep();
            }

            return VisitedLocations.Count().ToString();
        }

        protected void MoveStep()
        {
            Vector2I nextStep = Location + Direction;
            char nextChar;

            try
            {
                nextChar = CharOF(Location + Direction);
            }
            catch (IndexOutOfRangeException e)
            {
                GuardIsMoving = false;
                return;
            }

            if (IsObstacle(nextChar))
            {
                // turn right
                int temp = Direction.Y;
                int y = Direction.X;
                int x = -temp;
                Direction = new Vector2I(x, y);
            }
            else
            {
                Location = nextStep;
            }
        }

        protected void SetGuardTransform()
        {
            for (int y = 0; y < Map.Length; y++)
            {
                string line = Map[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char symbol = line[x];
                    if (IsUp(symbol) || IsRight(symbol) || IsDown(symbol) || IsLeft(symbol))
                    {
                        Location = new Vector2I(x, y);
                    }
                }
            }
        }

        protected void StoreLocation(Vector2I index)
        {
            if (!VisitedLocations.Contains(index))
            {
                VisitedLocations = VisitedLocations.Append(index);
            }
        }

        protected char CharOF(Vector2I index)
        {
            return Map[index.Y][index.X];
        }

        protected bool IsUp(char symbol)
        {
            bool itis;

            if (itis = Up == symbol)
            {
                Direction = Vector2I.NN;
            }
            
            return itis;
        }
        protected bool IsRight(char symbol)
        {
            bool itis;

            if (itis = Right == symbol)
            {
                Direction = Vector2I.EE;
            }

            return itis;
        }
        protected bool IsDown(char symbol)
        {
            bool itis;

            if (itis = Down == symbol)
            {
                Direction = Vector2I.SS;
            }

            return itis;
        }
        protected bool IsLeft(char symbol)
        {
            bool itis;

            if (itis = Left == symbol)
            {
                Direction = Vector2I.WW;
            }

            return itis;
        }
        protected bool IsObstacle(char symbol)
        {
            return Obstacle == symbol;
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
