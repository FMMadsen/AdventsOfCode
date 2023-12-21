namespace AdventOfCode2023Solutions.Day10
{
    public class Pipe
    {
        public long ID { get; private set; }

        public long N { get; private set; }
        public long S { get; private set; }
        public long E { get; private set; }
        public long W { get; private set; }


        public bool CanGoNorth { get; private set; } = false;
        public bool CanGoSouth { get; private set; } = false;
        public bool CanGoEast { get; private set; } = false;
        public bool CanGoWest { get; private set; } = false;
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsStartLocation { get; private set; } = false;

        public int Connection1 = 0;
        public int Connection2 = 0;


        public Pipe(char pipe, int x, int y, int noOfXs, int noOfYs)
        {
            ID = (y * noOfXs) + x;
            X = x;
            Y = y;

            switch (pipe)
            {
                case '|':
                    CanGoNorth = true;
                    CanGoSouth = true;
                    break;
                case '-':
                    CanGoWest = true;
                    CanGoEast = true;
                    break;
                case 'L':
                    CanGoNorth = true;
                    CanGoEast = true;
                    break;
                case 'J':
                    CanGoNorth = true;
                    CanGoWest = true;
                    break;
                case '7':
                    CanGoWest = true;
                    CanGoSouth = true;
                    break;
                case 'F':
                    CanGoEast = true;
                    CanGoSouth = true;
                    break;
                case 'S':
                    IsStartLocation = true;
                    CanGoNorth = true;
                    CanGoSouth = true;
                    CanGoEast = true;
                    CanGoWest = true;
                    break;
                default:
                    break;
            }

            //var connection1 = 

            switch (pipe)
            {
                case '|':
                    CanGoNorth = true;
                    CanGoSouth = true;
                    break;
                case '-':
                    CanGoWest = true;
                    CanGoEast = true;
                    break;
                case 'L':
                    CanGoNorth = true;
                    CanGoEast = true;
                    break;
                case 'J':
                    CanGoNorth = true;
                    CanGoWest = true;
                    break;
                case '7':
                    CanGoWest = true;
                    CanGoSouth = true;
                    break;
                case 'F':
                    CanGoEast = true;
                    CanGoSouth = true;
                    break;
                case 'S':
                    IsStartLocation = true;
                    CanGoNorth = true;
                    CanGoSouth = true;
                    CanGoEast = true;
                    CanGoWest = true;
                    break;
                default:
                    break;
            }
        }
    }
}
