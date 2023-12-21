namespace AdventOfCode2023Solutions.Day10
{
    public class PositionAndDirection
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDirectionNorth { get; set; } = false;
        public bool IsDirectionSouth { get; set; } = false;
        public bool IsDirectionEast { get; set; } = false;
        public bool IsDirectionWest { get; set; } = false;

        public void SetDirectionNorth() { IsDirectionNorth = true; }
        public void SetDirectionSouth() { IsDirectionSouth = true; }
        public void SetDirectionEast() { IsDirectionEast = true; }
        public void SetDirectionWest() { IsDirectionWest = true; }

        public PositionAndDirection(int y, int x)
        {
            Y = y;
            X = x;
        }

        public PositionAndDirection(Char[,] map, int y, int x, char pipeChar, int directionNumber)
        {
            Y = y;
            X = x;
            if (pipeChar == 'S')
                DetermineStartDirections(map, directionNumber);
        }

        public PositionAndDirection(Char[,] map, PositionAndDirection previousPosition)
        {
            int currentX = previousPosition.X;
            int currentY = previousPosition.Y;

            X = previousPosition.IsDirectionEast ? ++currentX : previousPosition.IsDirectionWest ? --currentX : currentX;
            Y = previousPosition.IsDirectionSouth ? ++currentY : previousPosition.IsDirectionNorth ? --currentY : currentY;

            char newPipeChar = map[Y, X];

            switch (newPipeChar)
            {
                case '|':
                    if (previousPosition.IsDirectionNorth) IsDirectionNorth = true;
                    if (previousPosition.IsDirectionSouth) IsDirectionSouth = true;
                    break;
                case '-':
                    if (previousPosition.IsDirectionEast) IsDirectionEast = true;
                    if (previousPosition.IsDirectionWest) IsDirectionWest = true;
                    break;
                case 'L':
                    if (previousPosition.IsDirectionSouth) IsDirectionEast = true;
                    if (previousPosition.IsDirectionWest) IsDirectionNorth = true;
                    break;
                case 'J':
                    if (previousPosition.IsDirectionEast) IsDirectionNorth = true;
                    if (previousPosition.IsDirectionSouth) IsDirectionWest = true;
                    break;
                case '7':
                    if (previousPosition.IsDirectionNorth) IsDirectionWest = true;
                    if (previousPosition.IsDirectionEast) IsDirectionSouth = true;
                    break;
                case 'F':
                    if (previousPosition.IsDirectionNorth) IsDirectionEast = true;
                    if (previousPosition.IsDirectionWest) IsDirectionSouth = true;
                    break;
            }
        }

        public bool IsSamePositionAs(PositionAndDirection position)
        {
            return X == position.X && Y == position.Y;
        }

        private void DetermineStartDirections(Char[,] map, int directionNumber)
        {
            var noOfYPipes = map.GetLength(0);
            var noOfXPipes = map.GetLength(1);

            char? northPipe = (Y > 0) ? map[Y - 1, X] : null;
            char? southPipe = (Y < noOfYPipes - 1) ? map[Y + 1, X] : null;
            char? eastPipe = (X < noOfXPipes - 1) ? map[Y, X + 1] : null;
            char? westPipe = (X > 0) ? map[Y, X - 1] : null;

            bool hasNorthExit = (northPipe != null) && (northPipe == 'F' || northPipe == '7' || northPipe == '|');
            bool hasSouthExit = (southPipe != null) && (southPipe == 'L' || southPipe == 'J' || southPipe == '|');
            bool hasEastExit = (eastPipe != null) && (eastPipe == 'J' || eastPipe == '7' || eastPipe == '-');
            bool hasWestExit = (westPipe != null) && (westPipe == 'F' || westPipe == 'L' || westPipe == '-');

            int exitCount = 0;
            if (hasNorthExit)
            {
                exitCount++;
                if (directionNumber == exitCount)
                    IsDirectionNorth = true;
            }
            if (hasSouthExit)
            {
                exitCount++;
                if (directionNumber == exitCount)
                    IsDirectionSouth = true;
            }
            if (hasEastExit)
            {
                exitCount++;
                if (directionNumber == exitCount)
                    IsDirectionEast = true;
            }
            if (hasWestExit)
            {
                exitCount++;
                if (directionNumber == exitCount)
                    IsDirectionWest = true;
            }

            if (exitCount == 0)
                throw new Exception("Didnt find any exit from Start location");

            if (exitCount == 1)
                throw new Exception("Only found 1 exit from start location");

            if (exitCount > 2)
                throw new Exception("Found more than 2 exits from start location");
        }
    }
}
