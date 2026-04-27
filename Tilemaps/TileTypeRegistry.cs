using System.Net.Sockets;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores information relating to each tile type for fast lookup, rather than every tile instance holding it's own data.
    /// </summary>
    public class TileTypeRegistry
    {
        private Dictionary<ushort, TileInfo> _tileTypes = [];

        public void Add(ushort type, int tilesetIndex, TileCollision collision)
        {
            TileInfo info = new TileInfo(tilesetIndex, collision);
            _tileTypes.Add(type, info);
        }


        public TileInfo GetInfo(ushort type)
        {
            return _tileTypes[type];
        }
    }
}
