using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A single tile instance in a tilemap
    /// </summary>
    public struct Tile
    {
        public int TilesetID { get; }
        public CardinalDir Rotation { get; set; }


        public Tile(int tilesetID, CardinalDir rotation = CardinalDir.Up)
        {
            TilesetID = tilesetID;
            Rotation = rotation;
        }
    }
}
