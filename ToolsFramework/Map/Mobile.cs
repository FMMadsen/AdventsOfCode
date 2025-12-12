namespace ToolsFramework.Map
{
    public class Mobile(GenericMapTile location)
    {
        public bool Move(GenericDirection direction)
        {
            var newTile = location.GetTile(direction);

            if (newTile != null)
                location = newTile;

            return newTile != null;
        }
    }
}