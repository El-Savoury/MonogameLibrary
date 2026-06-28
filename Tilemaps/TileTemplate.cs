using MonogameLibrary.Collisions;
using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Stores info we can use to define different tile types.
    /// </summary>
    /// 
    /// <remarks>
    /// Implementation of flyweight pattern for tiles. A tile template contains data relevant to all
    /// tiles, so each tile in our map references a template (e.g GrassTileTemplate, WaterTileTemplate) 
    /// rather than storing the same data in every tile instance.
    /// </remarks>
    public struct TileTemplate
    {
        public int TilesetID { get; }
        public Rectangle Bounds { get; }
        public CardinalDir Rotation { get; }
        public TileCollision Collision { get; }
        public List<Animation> Animations { get; }


        public TileTemplate(int tilesetID)
        {
            TilesetID = tilesetID;
            Collision = TileCollision.None;
            Rotation = CardinalDir.Up;
        }


        public TileTemplate(int tilesetID, TileCollision collision, CardinalDir rotation = CardinalDir.Up)
        {
            TilesetID = tilesetID;
            Collision = collision;
            Rotation = rotation;
        }
    }
}
