using System.Net.Sockets;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores information relating to each tile type for fast lookup, rather than every tile instance holding it's own data.
    /// </summary>
    public class TileTypeRegistry
    {
        private Dictionary<ushort, TileInfo> _tileTypes = [];

        public void Add(ushort ID, int tilesetID)
        {
            TileInfo info = new TileInfo(tilesetID);
            _tileTypes.Add(ID, info);
        }


        public TileInfo GetInfo(ushort ID)
        {
            return _tileTypes[ID];
        }
    }
}
