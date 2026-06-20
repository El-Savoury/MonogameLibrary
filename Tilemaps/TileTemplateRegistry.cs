namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Implementation of flyweight pattern for tiles. A tile template 
    /// represents one tile type (e.g GrassTile) and each tile instance in our map
    /// references a template rather storing the same info in every tile.
    /// </summary>
    public class TileTemplateRegistry
    {
        private Dictionary<int, TileTemplate> _templates = [];

        public void Register(int tilesetID, TileCollision collision)
        {
            TileTemplate info = new TileTemplate(tilesetID, collision);
            _templates.Add(tilesetID, info);
        }
    }
}
