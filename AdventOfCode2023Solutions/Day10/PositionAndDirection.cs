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

        public char Direction { get; set; }

        public void SetDirectionNorth() { Direction = 'N'; IsDirectionNorth = true; }
        public void SetDirectionSouth() { Direction = 'S'; IsDirectionSouth = true; }
        public void SetDirectionEast() { Direction = 'E'; IsDirectionEast = true; }
        public void SetDirectionWest() { Direction = 'W'; IsDirectionWest = true; }

        public bool IsSamePositionAs(PositionAndDirection position)
        {
            return X == position.X && Y == position.Y;
        }

        public void Move()
        {
            X = IsDirectionEast ? ++X : IsDirectionWest ? --X : X;
            Y = IsDirectionSouth ? ++Y : IsDirectionNorth ? --Y : Y;
        }
    }
}
