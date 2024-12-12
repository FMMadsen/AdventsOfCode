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

        private Vector2I DirectionValue = new();
        protected Vector2I Direction 
        {
            get
            {
                return DirectionValue;
            }
            set
            {
                DirectionValue = value;
            }
        }
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
        protected IEnumerable<Transform> VisitedPossible = Enumerable.Empty<Transform>();
        protected IEnumerable<Vector2I> PossibleObstacles = Enumerable.Empty<Vector2I>();
        protected IEnumerable<Transform> Visited = Enumerable.Empty<Transform>();
        protected Vector2I[] Obstacles = Array.Empty<Vector2I>();
        protected Vector2I? ConsideredObstacle = null;

        public string SolvePart1(string[] datasetLines)
        {
            Map = datasetLines;
            LoadMap();

            GuardIsMoving = true;

            while (GuardIsMoving)
            {
                MoveStep();
            }

            Console.WriteLine("Steps: " + Visited.Count());

            return VisitedLocations.Count().ToString();
        }

        protected void MoveStep(bool detect = false)
        {
            Vector2I nextStep = Location + Direction;
            char nextChar;

            if (detect)
            {
                if (!Console.IsOutputRedirected) { Console.SetCursorPosition(0, Console.CursorTop - 1); }
                Console.WriteLine("Step: " + Visited.Count().ToString() + " ");
            }

            try
            {
                nextChar = CharOF(nextStep);
            }
            catch (IndexOutOfRangeException e)
            {
                GuardIsMoving = false;
                return;
            }

            if (IsObstacle(nextChar))
            {
                Direction = TurnRight(Direction);
            }
            else
            {
                if (detect)
                {
                    VisitedPossible = Enumerable.Empty<Transform>();
                    ConsideredObstacle = nextStep;
                    Transform? resultNext = null;
                    bool possibleObstacle = DetectPossibleObstacle(new Transform(Location, Direction), out resultNext);

                    while (null != resultNext)
                    {
                        possibleObstacle = DetectPossibleObstacle(new Transform(resultNext.Location, resultNext.Direction), out resultNext);
                    }

                    if (possibleObstacle)
                    {
                        StorePossibleObstacle((Vector2I)ConsideredObstacle);
                    }

                    ConsideredObstacle = null;
                }

                Location = nextStep;
            }

            StoreTransform(new Transform(Location, Direction));
        }

        protected static Vector2I TurnRight(Vector2I direction)
        {
            int temp = direction.Y;
            int y = direction.X;
            int x = -temp;
            return new Vector2I(x, y);
        }

        protected void LoadMap()
        {
            VisitedLocations = Enumerable.Empty<Vector2I>();
            VisitedPossible = Enumerable.Empty<Transform>();
            PossibleObstacles = Enumerable.Empty<Vector2I>();
            Visited = Enumerable.Empty<Transform>();
            Obstacles = Array.Empty<Vector2I>();

            IEnumerable<Vector2I> tempObst = Enumerable.Empty<Vector2I>();

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

                    if (IsObstacle(symbol))
                    {
                        tempObst = tempObst.Append(new Vector2I(x, y));
                    }
                }
            }
            Obstacles = tempObst.ToArray();
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
        protected static bool IsObstacle(char symbol)
        {
            return Obstacle == symbol;
        }

        public string SolvePart2(string[] datasetLines)
        {
            Map = datasetLines;
            LoadMap();

            GuardIsMoving = true;
            Console.WriteLine("Step: " + (-1).ToString());
            while (GuardIsMoving)
            {
                MoveStep(true);
            }

            return PossibleObstacles.Count().ToString();
        }

        private bool DetectPossibleObstacle(Transform aTransform, out Transform? frontNextObstacle)
        {
            Transform turned = new Transform(aTransform.Location, TurnRight(aTransform.Direction) );
            bool found = NextObstacle(turned, out Vector2I nextObstacle);
            frontNextObstacle = null;

            if (found)
            {
                if (!FindVisitedPossibleBetween(turned, nextObstacle))
                {
                    frontNextObstacle = new Transform(nextObstacle - turned.Direction, turned.Direction);
                    VisitedPossible = VisitedPossible.Append(frontNextObstacle);
                }
            }

            return found;
        }

        protected bool FindVisitedBetween(Transform start, Vector2I nextObstacle)
        {
            Vector2I ObstacleDirection;
            foreach (Transform visited in Visited)
            {
                ObstacleDirection = nextObstacle - visited.Location;
                if (start.Direction == visited.Direction)
                {
                    if (ObstacleDirection == OneDirectioned(ObstacleDirection, start.Direction))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool FindVisitedPossibleBetween(Transform start, Vector2I nextObstacle)
        {
            Vector2I ObstacleDirection;
            foreach (Transform visited in VisitedPossible)
            {
                ObstacleDirection = nextObstacle - visited.Location;
                if (start.Direction == visited.Direction)
                {
                    if (ObstacleDirection == OneDirectioned(ObstacleDirection, start.Direction))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool NextObstacle(Transform aTransform, out Vector2I nextObstacle)
        {
            if (Obstacles.Length < 1) { throw new Exception("obstacles require at least one item"); }

            nextObstacle = Vector2I.Off;
            Vector2I temp;
            
            if (null != ConsideredObstacle)
            {
                temp = ClosestTo(aTransform, nextObstacle, (Vector2I)ConsideredObstacle);

                if (Vector2I.Off != temp)
                {
                    nextObstacle = temp;
                }
            }
            
            foreach (Vector2I obstacle in Obstacles)
            {
                temp = ClosestTo(aTransform, nextObstacle, obstacle);

                if (Vector2I.Off != temp)
                {
                    nextObstacle = temp;
                }
            }

            return Vector2I.Off != nextObstacle;
        }

        protected static Vector2I ClosestTo(Transform aTransform, Vector2I a, Vector2I b)
        {
            Vector2I result = Vector2I.Off;

            if (Vector2I.Off != a && Vector2I.Off != b)
            {
                bool aOnLine = IsOnLine(aTransform, a);
                bool bOnLine = IsOnLine(aTransform, b);
                int aDistance = -1;
                int bDistance = -1;

                if (aOnLine && bOnLine)
                {
                    if (0 < aTransform.Direction.X)
                    {
                        aDistance = a.X - aTransform.Location.X;
                        bDistance = b.X - aTransform.Location.X;
                    }
                    else if (aTransform.Direction.X < 0)
                    {
                        aDistance = aTransform.Location.X - a.X;
                        bDistance = aTransform.Location.X - b.X;
                    }
                    else if (0 < aTransform.Direction.Y)
                    {
                        aDistance = a.Y - aTransform.Location.Y;
                        bDistance = b.Y - aTransform.Location.Y;
                    }
                    else if (aTransform.Direction.Y < 0)
                    {
                        aDistance = aTransform.Location.Y - a.Y;
                        bDistance = aTransform.Location.Y - b.Y;
                    }
                }
                

                if (0 < aDistance && 0 < bDistance)
                {
                    if (aDistance < bDistance)
                    {
                        result = a;
                    }
                    else
                    {
                        result = b;
                    }
                }
                else if (0 < aDistance)
                {
                    result = a;
                }
                else if (0 < bDistance)
                {
                    result = b;
                }
            }
            else if (Vector2I.Off != a)
            {
                bool aOnLine = IsOnLine(aTransform, a);
                int aDistance = -1;

                if (aOnLine)
                {
                    if (0 < aTransform.Direction.X)
                    {
                        aDistance = a.X - aTransform.Location.X;
                    }
                    else if (aTransform.Direction.X < 0)
                    {
                        aDistance = aTransform.Location.X - a.X;
                    }
                    else if (0 < aTransform.Direction.Y)
                    {
                        aDistance = a.Y - aTransform.Location.Y;
                    }
                    else if (aTransform.Direction.Y < 0)
                    {
                        aDistance = aTransform.Location.Y - a.Y;
                    }
                }
                

                if (0 < aDistance)
                {
                    result = a;
                }
            }
            else if (Vector2I.Off != b)
            {
                bool bOnLine = IsOnLine(aTransform, b);
                int bDistance = -1;

                if (bOnLine)
                {
                    if (0 < aTransform.Direction.X)
                    {
                        bDistance = b.X - aTransform.Location.X;
                    }
                    else if (aTransform.Direction.X < 0)
                    {
                        bDistance = aTransform.Location.X - b.X;
                    }
                    else if (0 < aTransform.Direction.Y)
                    {
                        bDistance = b.Y - aTransform.Location.Y;
                    }
                    else if (aTransform.Direction.Y < 0)
                    {
                        bDistance = aTransform.Location.Y - b.Y;
                    }
                }
                

                if (0 < bDistance)
                {
                    result = b;
                }
            }

            return result;
            
        }

        protected static bool IsOnLine(Transform aTransform, Vector2I dot)
        {
            Vector2I delta = dot - aTransform.Location;
            Vector2I oneDirection = OneDirectioned(delta, aTransform.Direction);

            if (oneDirection == delta)
            {
                return true;
            }

            return false;
        }

        protected static bool IsOnCorrectSide(Transform aTransform, Vector2I dot)
        {

            if (0 == aTransform.Direction.X)
            {
                if (0 < aTransform.Direction.Y && aTransform.Location.Y < dot.Y )
                {
                    return true;
                }
                else if (aTransform.Direction.Y < 0 && dot.Y < aTransform.Location.Y)
                {
                    return true;
                }
            }
            else if (0 == aTransform.Direction.Y)
            {
                if (0 < aTransform.Direction.X && aTransform.Location.X < dot.X)
                {
                    return true;
                }
                else if (aTransform.Direction.X < 0 && dot.X < aTransform.Location.X)
                {
                    return true;
                }
            }

            return false;
        }

        protected static int ToPositive(int x)
        {
            if (x < 0)
            {
                return x * -1;
            }
            return x;
        }

        protected static Vector2I OneDirectioned(Vector2I deltaDirection, Vector2I direction)
        {
            int dx = direction.X;
            int dy = direction.Y;

            if (direction.X < 0 && deltaDirection.X < 0)
            {
                dx = -dx;
            }

            if (direction.Y < 0 && deltaDirection.Y < 0)
            {
                dy = -dy;
            }

            return new Vector2I(deltaDirection.X * dx, deltaDirection.Y * dy);
        }

        protected static Vector2I LowestCommon(Vector2I numbers)
        {
            {
                int num1, num2;

                if (numbers.Y < numbers.X)
                {
                    num1 = numbers.X;
                    num2 = numbers.Y;
                }
                else
                {
                    num1 = numbers.Y;
                    num2 = numbers.X;
                }

                for (int i = 2; i <= num2; i++)
                {
                    if (i % num2 == 0 && i % num1 == 0)
                    {
                        return new Vector2I(numbers.X / i, numbers.Y / i);
                    }
                }

                return numbers;
            }
        }

        protected void StoreTransform(Transform aTransform)
        {

            Visited = Visited.Append(aTransform);
        }

        protected void StorePossibleObstacle(Vector2I index)
        {
            if (!PossibleObstacles.Contains(index))
            {
                PossibleObstacles = PossibleObstacles.Append(index);
            }
        }
    }
}
