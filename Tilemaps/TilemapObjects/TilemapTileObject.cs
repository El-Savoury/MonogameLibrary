
using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps.TilemapObjects
{
    abstract class TilemapTileObject : TilemapObject
    {
        protected Tile _tile;

        public TilemapTileObject(Tilemap tilemap, int xIndex, int yIndex, Tile tile) : base(tilemap, xIndex, yIndex)
        {
            _tile = tile;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
