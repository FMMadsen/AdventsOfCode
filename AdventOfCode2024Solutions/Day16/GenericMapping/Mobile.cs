namespace AdventOfCode2024Solutions.Day16.GenericMapping
{
    internal class Mobile(GenericMapTile location)
    {
        public bool Move(GenericDirection direction)
        {
            var newTile = location.GetTileStraightAhead(direction);

            if (newTile != null)
                location = newTile;

            return newTile != null;
        }
    }
}
