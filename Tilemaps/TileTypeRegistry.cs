namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores information relating to each tile type for fast lookup, rather than every tilemap tile instance holding it's own data.
    /// </summary>
    public class TileTypeRegistry
    {
        private Dictionary<Enum, TileInfo> _tileTypes = [];


        /// <summary>
        /// Register a new tile type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public void Add(Enum type, TileInfo info)
        {
            _tileTypes.Add(type, info);
        }


        /// <summary>
        /// Get the information relevant to a tile type
        /// </summary>
        /// <param name="tileType">Tile type to get info about</param>
        /// <returns>Tile info for specified tile type</returns>
        public TileInfo GetTileInfo(Enum tileType)
        {
            return _tileTypes[tileType];
        }
    }
}
