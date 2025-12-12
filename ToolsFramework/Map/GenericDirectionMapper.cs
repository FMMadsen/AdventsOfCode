namespace ToolsFramework.Map
{
    public static class GenericDirectionMapper
    {
        public static GenericDirection GetCounterClockwise(GenericDirection currentDirection)
        {
            return currentDirection switch
            {
                GenericDirection.North => GenericDirection.NorthWest,
                GenericDirection.NorthEast => GenericDirection.North,
                GenericDirection.East => GenericDirection.NorthEast,
                GenericDirection.SouthEast => GenericDirection.East,
                GenericDirection.South => GenericDirection.SouthEast,
                GenericDirection.SouthWest => GenericDirection.South,
                GenericDirection.West => GenericDirection.SouthWest,
                GenericDirection.NorthWest => GenericDirection.West,
                _ => throw new Exception("Invalid ")
            };
        }

        public static GenericDirection GetClockwiseDirection(GenericDirection currentDirection)
        {
            return currentDirection switch
            {
                GenericDirection.North => GenericDirection.NorthEast,
                GenericDirection.NorthEast => GenericDirection.East,
                GenericDirection.East => GenericDirection.SouthEast,
                GenericDirection.SouthEast => GenericDirection.South,
                GenericDirection.South => GenericDirection.SouthWest,
                GenericDirection.SouthWest => GenericDirection.West,
                GenericDirection.West => GenericDirection.NorthWest,
                GenericDirection.NorthWest => GenericDirection.North,
                _ => throw new Exception("Invalid ")
            };
        }

        public static GenericDirection GetLeftDirection(GenericDirection currentDirection)
        {
            return currentDirection switch
            {
                GenericDirection.North => GenericDirection.West,
                GenericDirection.NorthEast => GenericDirection.NorthWest,
                GenericDirection.East => GenericDirection.North,
                GenericDirection.SouthEast => GenericDirection.NorthEast,
                GenericDirection.South => GenericDirection.East,
                GenericDirection.SouthWest => GenericDirection.SouthEast,
                GenericDirection.West => GenericDirection.South,
                GenericDirection.NorthWest => GenericDirection.SouthWest,
                _ => throw new Exception("Invalid ")
            };
        }

        public static GenericDirection GetRightDirection(GenericDirection currentDirection)
        {
            return currentDirection switch
            {
                GenericDirection.North => GenericDirection.East,
                GenericDirection.NorthEast => GenericDirection.SouthEast,
                GenericDirection.East => GenericDirection.South,
                GenericDirection.SouthEast => GenericDirection.SouthWest,
                GenericDirection.South => GenericDirection.West,
                GenericDirection.SouthWest => GenericDirection.NorthWest,
                GenericDirection.West => GenericDirection.North,
                GenericDirection.NorthWest => GenericDirection.NorthEast,
                _ => throw new Exception("Invalid ")
            };
        }
    }
}