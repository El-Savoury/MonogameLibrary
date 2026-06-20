using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores info we can use to define different tile types
    /// </summary>
    public struct TileTemplate
    {
        public int TileType { get; }
        public int TilesetID { get; }
        public Rectangle Bounds { get; }
        public CardinalDir Rotation { get; }
        public TileCollision Collision { get; }
        public List<Animation> Animations { get; }


        public TileTemplate(int tileType, int tilesetID, TileCollision collision)
        {
            TileType = tileType;
            TilesetID = tilesetID;
            Collision = collision;
        }
    }
}
