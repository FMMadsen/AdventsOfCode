namespace ToolsFramework.Map
{
    public class GenericMapTileFactory
    {
        public virtual GenericMapTile Create(int x, int y, char source)
        {
            return new GenericMapTile(x, y, source);
        }
    }
}