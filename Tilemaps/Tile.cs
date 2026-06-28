using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A single tile instance in a tilemap.
    /// </summary>
    public struct Tile
    {
        public int TilesetID { get; }
        public CardinalDir Rotation { get; set; }


        public Tile(int tilesetID, CardinalDir rotation)
        {
            TilesetID = tilesetID;
            Rotation = rotation;
        }

        public static Tile FromTemplate(TileTemplate template)
        {
            return new Tile(template.TilesetID, template.Rotation);
        }
    }
}
