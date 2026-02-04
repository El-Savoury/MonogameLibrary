namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores information relating to each tile type for fast lookup, rather than every tilemap tile instance holding it's own data.
    /// </summary>
    public class TileTypeRegistry
    {
        private Dictionary<ushort, TileInfo> _tileTypes = [];


        /// <summary>
        /// Register a new tile type
        /// </summary>
        /// <param name="tileType"></param>
        /// <param name="tileInfo"></param>
        public void Add(Enum tileType, int tilesetID)
        {
            TileInfo info = new TileInfo(tilesetID);
            _tileTypes.Add(Convert.ToUInt16(tileType), info);
        }


        /// <summary>
        /// Get the information relevant to a tile type
        /// </summary>
        /// <param name="tileType">Tile type to get info about</param>
        /// <returns>Tile info for specified tile type</returns>
        public TileInfo GetInfo(ushort tileType)
        {
            return _tileTypes[tileType];
        }
    }
}
