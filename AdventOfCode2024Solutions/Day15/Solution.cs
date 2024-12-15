using AdventOfCode2024Solutions.Day04;
using Common;
using System.Linq;

namespace AdventOfCode2024Solutions.Day15
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 15: ";

        protected WarehouseMap CurrentMap {  get; set; } = new WarehouseMap();

        public string SolvePart1(string[] datasetLines)
        {
            CurrentMap = LoadMap(datasetLines, out int firstEmpty);

            Robot currentRobot = (Robot)CurrentMap.Children.Where(a=> a is Robot).First();

            currentRobot.StepList = LoadRobotSteps(datasetLines, firstEmpty + 1);

            foreach (Vector2I step in currentRobot.StepList)
            {
                Move(currentRobot, step);
            }

            long result = 0;

            foreach (WarehouseObject item in CurrentMap.Children)
            {
                if (item is Box)
                {
                    result += item.Location.X + (100 * item.Location.Y);
                }
            }

            return result.ToString();
        }

        public void Move(WarehouseObject item, Vector2I direction) 
        {
            // pickup all directly next objects in direction. if hit none pushable return.
            WarehouseObject[] toMove = Array.Empty<WarehouseObject>();
            IEnumerable<WarehouseObject> boxes = Enumerable.Empty<WarehouseObject>();
            IEnumerable<WarehouseObject> boxesTemp = Enumerable.Empty<WarehouseObject>();

            //Vector2I? nextItem = Vector2I.Off;
            //Vector2I searchLocation = item.Location;
            bool canMove = true;
            bool found = true;

            boxes = boxes.Append(item);

            while (found)
            {
                found = false;

                foreach (WarehouseObject mapItem in CurrentMap.Children)
                {
                    bool itemHit = false;

                    if (Vector2I.NN == direction || Vector2I.SS == direction)
                    {
                        itemHit = boxes.Any(a =>
                           a.Location + direction == mapItem.Location
                        || a.Location + direction == mapItem.Location + mapItem.ExtraSize
                        || a.Location + a.ExtraSize + direction == mapItem.Location
                        || a.Location + a.ExtraSize + direction == mapItem.Location + mapItem.ExtraSize);
                    }
                    else if (Vector2I.EE == direction)
                    {
                        itemHit = boxes.Any(a => a.Location + a.ExtraSize + direction == mapItem.Location);
                    }
                    else if (Vector2I.WW == direction)
                    {
                        itemHit = boxes.Any(a => a.Location + direction == mapItem.Location + mapItem.ExtraSize);
                    }

                    if (itemHit)
                    {
                        if (mapItem.Pushable)
                        {
                            boxesTemp = boxesTemp.Append(mapItem);
                            found = true;
                        }
                        else
                        {
                            found = true;
                            canMove = false;
                            break;
                        }
                    }
                }

                if (!canMove)
                {
                    break;
                }

                toMove = toMove.Union(boxes).ToArray();
                boxes = boxesTemp;
                boxesTemp = Enumerable.Empty<WarehouseObject>();
            }

            if (canMove)
            {
                foreach (WarehouseObject box in toMove)
                {
                    box.Location += direction;
                }
            }

        }

        protected static WarehouseMap LoadMap(string[] datasetLines, out int emptyLine)
        {
            WarehouseMap warehouse = new();
            string currentLine;
            emptyLine = -1;

            for (int y = 0; y < datasetLines.Length; y++) 
            {
                currentLine = datasetLines[y];

                if (currentLine.Length == 0 && currentLine.All(char.IsWhiteSpace)) { emptyLine = y; break; }

                for (int x = 0; x < currentLine.Length; x++)
                {
                    switch (currentLine[x])
                    {
                        case '#':
                            warehouse.Children = warehouse.Children.Append(new Wall() { Location = new Vector2I(x, y) }).ToArray();
                            break;
                        case '@':
                            warehouse.Children = warehouse.Children.Append(new Robot() { Location = new Vector2I(x, y) }).ToArray();
                            break;
                        case 'O':
                            warehouse.Children = warehouse.Children.Append(new Box() { Location = new Vector2I(x, y) }).ToArray();
                            break;
                    }
                }
                
            }

            return warehouse;
        }

        protected static Vector2I[] LoadRobotSteps(string[] datasetLines, int beginAt)
        {
            Vector2I[] directions;
            {
                int count = 0;

                for (int i = beginAt; i < datasetLines.Length; i++)
                {
                    count += datasetLines[i].Length;
                }

                directions = new Vector2I[count];
            }

            int directionIndex = 0;

            for (int linesIndex = beginAt; linesIndex < datasetLines.Length; linesIndex++)
            {
                string currentLine = datasetLines[linesIndex];

                for (int lineIndex = 0; lineIndex < currentLine.Length; lineIndex++) 
                {
                    directions[directionIndex] = CharToDirection(currentLine[lineIndex]);
                    directionIndex++;
                }
            }

            if (directionIndex != directions.Length)
            {
                throw new Exception("Something went wrong loading Robot directions.\nPerhaps an empty char in input");
            }

            return directions;
        }

        public static Vector2I CharToDirection(char direction)
        {
            switch (direction)
            {
                case '^':
                    return Vector2I.NN;
                    break;
                case '>':
                    return Vector2I.EE;
                    break;
                case 'v':
                    return Vector2I.SS;
                    break;
                case '<':
                    return Vector2I.WW;
                    break;

                default:
                    return Vector2I.Zero;
                    break;
            }
        }

        public string SolvePart2(string[] datasetLines)
        {
            CurrentMap = LoadMap2(datasetLines, out int firstEmpty);

            Robot currentRobot = (Robot)CurrentMap.Children.Where(a => a is Robot).First();

            currentRobot.StepList = LoadRobotSteps(datasetLines, firstEmpty + 1);

            foreach (Vector2I step in currentRobot.StepList)
            {
                Move(currentRobot, step);
            }

            long result = 0;

            foreach (WarehouseObject item in CurrentMap.Children)
            {
                if (item is Box)
                {
                    result += item.Location.X + (100 * item.Location.Y);
                }
            }

            return result.ToString();
        }

        protected static WarehouseMap LoadMap2(string[] datasetLines, out int emptyLine)
        {
            WarehouseMap warehouse = new();
            string currentLine;
            emptyLine = -1;

            for (int y = 0; y < datasetLines.Length; y++)
            {
                currentLine = datasetLines[y];

                if (currentLine.Length == 0 && currentLine.All(char.IsWhiteSpace)) { emptyLine = y; break; }

                for (int x = 0; x < currentLine.Length; x++)
                {
                    switch (currentLine[x])
                    {
                        case '#':
                            warehouse.Children = warehouse.Children.Append(new Wall() { Location = new Vector2I(x * 2, y), ExtraSize=Vector2I.EE }).ToArray();
                            break;
                        case '@':
                            warehouse.Children = warehouse.Children.Append(new Robot() { Location = new Vector2I(x * 2, y) }).ToArray();
                            break;
                        case 'O':
                            warehouse.Children = warehouse.Children.Append(new Box() { Location = new Vector2I(x * 2, y), ExtraSize = Vector2I.EE }).ToArray();
                            break;
                    }
                }

            }

            return warehouse;
        }
    }
}
